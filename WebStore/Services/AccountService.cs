using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebStore.Data;
using WebStore.Data.Entities;
using WebStore.Data.Entities.Account;
using WebStore.Models;
using WebStore.Models.AccountModel;
using WebStore.Models.ProductModel;

namespace WebStore.Services
{
    public class AccountService
    {
        private readonly ApplicationDbContext context;

        public AccountService(ApplicationDbContext _context)
        {
            this.context = _context;
        }
        public AccountViewModel GetUser(string username)
        {
            
            return context.Users.Where(x => x.UserName == username)
                .Select(x => new AccountViewModel
                {
                    Id = x.Id,
                    Username = x.UserName,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Role = x.Role,
                    Cart = x.Cart
                })
                .FirstOrDefault();
        }
        public async Task<IEnumerable<Payment>> GetLast10Orders(int CartId)
        {
            return await context.Payments
                .OrderByDescending(x => x.Id)
                .Take(10)
                .Where(x => x.Cart.Id == CartId)
                //.Include(x => x.Items)
                .Select(x => new Payment()
            {
                Id = x.Id,
                Amount = x.Amount,
                AmountOfItems = x.AmountOfItems,
                Items = x.Items 
            }).ToListAsync();
        }
        public Payment GetOrder(int id)
        {
            return context.Payments
                .Where(x => x.Id == id)
                .Select(x => new Payment()
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    AmountOfItems = x.AmountOfItems,
                    Items = x.Items
                }).FirstOrDefault();
        }
    }
}
