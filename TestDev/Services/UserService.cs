using Microsoft.EntityFrameworkCore;
using TestDev.Helpers;
using TestDev.Model;

namespace TestDev.Services
{
    public interface IUserService
    {
        public Task<List<User>> GetAll();
    }

    public class UserService : IUserService
    {
        private readonly DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAll() => await _context.Users.ToListAsync();
    }

}

