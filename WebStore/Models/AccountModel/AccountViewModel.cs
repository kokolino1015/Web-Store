using System.ComponentModel.DataAnnotations;
using WebStore.Data.Entities;

namespace WebStore.Models.AccountModel
{
    public class AccountViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
       
        public string? FirstName { get; set; }

        
        public string? LastName { get; set; }

        public Role? Role { get; set; }
        public Cart? Cart { get; set; }
    }
}
