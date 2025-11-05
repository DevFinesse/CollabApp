using CollabApp.Contracts.Repository;
using CollabApp.Contracts.Services;
using CollabApp.Domain.Entities;
using CollabApp.Shared.Abstractions;
using CollabApp.Shared.Common.ErrorHandler;
using CollabApp.Shared.Dtos.User;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CollabApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IRoleRepository _roleRepository;
        private readonly IRoleService _roleService;

        public UserService(
            UserManager<User> userManager,
            IRoleRepository roleRepository,
            IRoleService roleService)
        {
            _userManager = userManager;
            _roleRepository = roleRepository;
            _roleService = roleService;
        }

        public async Task<Result<UserResponse>> GetAsync(string userId)
        {
            if(await _userManager.FindByIdAsync(userId) is not { } user)
                return Result.Failure<UserResponse>(UserErrors.UserNotFound);

            var roles = await _userManager.GetRolesAsync(user);
            var response = new UserResponse(
                               Id: user.Id,
                               FirstName: user.FirstName,
                               LastName: user.LastName,
                               Email: user.Email ?? string.Empty,
                               Status: user.Status,
                               IsDisabled: user.IsDisabled,
                               Roles: roles
                           );
            return Result.Success(response);
        }

        public async Task<Result<UserResponse>> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
        {
            var emailExists = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
            if (emailExists)
                return Result.Failure<UserResponse>(UserErrors.DuplicatedEmail);

            var allowedRoles = await _roleService.GetAllAsync(cancellationToken: cancellationToken);

            if (request.Roles.Except(allowedRoles.Select(x => x.Name)).Any())
                return Result.Failure<UserResponse>(UserErrors.InvalidRoles);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Email = request.Email
            };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, request.Roles);
                var roles = await _userManager.GetRolesAsync(user);
                
                // Manual mapping to debug the issue
                var response = new UserResponse(
                    Id: user.Id,
                    FirstName: user.FirstName,
                    LastName: user.LastName,
                    Email: user.Email ?? string.Empty,
                    Status: user.Status,
                    IsDisabled: user.IsDisabled,
                    Roles: roles
                );
                
                return Result.Success(response);
            }
            else
            {
                var error = result.Errors.First();
                return Result.Failure<UserResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
            }
        }

        public async Task<Result> UpdateAsync(string userId, UpdateUserRequest request, CancellationToken cancellationToken = default)
        {
            if(await _userManager.FindByIdAsync(userId) is not { } user)
                return Result.Failure(UserErrors.UserNotFound);

            var emailExists = await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != userId, cancellationToken);
            if (emailExists)
                return Result.Failure(UserErrors.DuplicatedEmail);

            var allowRoles = await _roleService.GetAllAsync(cancellationToken: cancellationToken);

            if (request.Roles.Except(allowRoles.Select(x => x.Name)).Any())
                return Result.Failure(UserErrors.InvalidRoles);

            request.Adapt(user);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await _roleRepository.DeleteRolesOfUserAsync(user.Id, cancellationToken);
                await _userManager.AddToRolesAsync(user, request.Roles);
                return Result.Success();
            }
            else
            {
                var error = result.Errors.First();
                return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
            }
        }

        public async Task<Result<UserProfileResponse>> GetProfileInfoAsync(string userId)
        {
            try
            {
                var user = await _userManager.Users
                    .Where(x => x.Id == userId)
                    .ProjectToType<UserProfileResponse>()
                    .FirstOrDefaultAsync();

                if (user == null)
                    return Result.Failure<UserProfileResponse>(UserErrors.UserNotFound);

                return Result.Success(user);
            }
            catch (Exception)
            {
                return Result.Failure<UserProfileResponse>(UserErrors.UserNotFound);
            }
        }

        public async Task<Result> ToggleStatusAsync(string id)
        {
            if (await _userManager.FindByIdAsync(id) is not { } user)
                return Result.Failure(UserErrors.UserNotFound);

            user.IsDisabled = !user.IsDisabled;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
                return Result.Success();

            var error = result.Errors.First();
            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }

        public async Task<Result> UnlockAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return Result.Failure(UserErrors.UserNotFound);

            var result = await _userManager.SetLockoutEndDateAsync(user, null);

            if (result.Succeeded)
                return Result.Success();

            var error = result.Errors.First();
            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }

        public async Task<Result> UpdateProfileAsync(string userId, UpdateProfileRequest request)
        {
            try
            {
                await _userManager.Users
                    .Where(x => x.Id == userId)
                    .ExecuteUpdateAsync(setter =>
                        setter.SetProperty(x => x.FirstName, request.FirstName)
                            .SetProperty(x => x.LastName, request.LastName));

                return Result.Success();
            }
            catch (Exception)
            {
                return Result.Failure(UserErrors.UserNotFound);
            }
        }

        public async Task<Result> ChangePasswordAsync(string userId, ChangePasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return Result.Failure(UserErrors.UserNotFound);

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if (result.Succeeded)
                return Result.Success();

            var error = result.Errors.First();
            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }
    }
}
