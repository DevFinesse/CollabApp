using Microsoft.AspNetCore.Identity;

namespace CollabApp.Domain.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {
            Id = Guid.CreateVersion7().ToString();
        }

        public bool IsDeleted { get; set; }
    }
}
