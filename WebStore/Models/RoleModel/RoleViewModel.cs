using System.ComponentModel.DataAnnotations;

namespace WebStore.Models.RoleModel
{
    public class RoleViewModel
    {
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
