using Application.API.User.Model.Dto;
using Microsoft.EntityFrameworkCore;

namespace Application.API.User.Model
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<UserDto> User { get; set; }
    }
}
