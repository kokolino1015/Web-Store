using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Models.RoleModel
{
    public class SetRoleViewModel
    {
        [Required]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "User Name is Required")]
        
        public string? RoleName { get; set; }
        //public IList<string>? UserRoles { get; set; }

        public IEnumerable<IdentityRole>? AllRoles { get; set; }
        //public IList<string>? AllRoles { get; set; }
    }
}
