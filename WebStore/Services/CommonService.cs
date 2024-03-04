using System.Security.Claims;
using WebStore.Data.Entities.Account;
using WebStore.Data.Entities;
using WebStore.Data;

namespace WebStore.Services
{
    public class CommonService
    {
        private readonly ApplicationDbContext context;
        public CommonService(ApplicationDbContext _context)
        {
            this.context = _context;
        }
        public ApplicationUser FindUser(ClaimsPrincipal user)
        {
            string userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return context.Users.Where(x => x.Id == userId)
                .Select(x => new ApplicationUser{
                    Id = x.Id,
                    Email = x.Email,
                    UserName = x.UserName,
                    Cart = x.Cart
            })
                .FirstOrDefault();
        }
        //public Role FindRole(ClaimsPrincipal user)
        //{
        //    string userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    var role = context.Users.Where(x => x.Id == userId).Select(x => new ApplicationUser()
        //    {
        //        Role = x.Role,
        //    }).FirstOrDefault();
        //    if (role != null)
        //    {
        //        return context.Roles.Where(x => x.Id == role.Role.Id).Select(x => new Role()
        //        {
        //            Id = x.Id,
        //            Name = x.Name,
        //        }).FirstOrDefault();
        //    }
        //    return null;

        //}
    }
}
