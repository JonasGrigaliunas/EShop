using EShop.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EShop.Readers
{
    public class RoleReader : Reader<IdentityRole>
    {
        public RoleReader(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}