using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Data;
using WebStore.Data.Entities;

namespace WebStore.Data.Entities.Account
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(20)]
        public string? FirstName { get; set; }

        [StringLength(20)]
        public string? LastName { get; set; }

        public Role? Role { get; set; }
        public Cart? Cart { get; set; }
    }
}