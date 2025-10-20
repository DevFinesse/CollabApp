using Microsoft.AspNetCore.Identity;

namespace CollabApp.Domain.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {
            // Let Identity handle Id generation
        }

        public bool IsDeleted { get; set; }
    }
}
