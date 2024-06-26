﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [ForeignKey("Cart")]
        public int CartId { get; set; }
        public Role? Role { get; set; }
        public Cart? Cart { get; set; }
    }
}