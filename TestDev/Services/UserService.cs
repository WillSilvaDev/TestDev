using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using TestDev.Helpers;
using TestDev.Model;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;

namespace TestDev.Services
{
    public interface IUserService
    {
        public Task<User> Create(User user);
        public Task<User> GetById(int id);
        public Task Update(User userIn, int id);
        public Task Delete(int id);
        public Task<List<User>> GetAll();

        public Task<User> GetUserAsync(string id);
        //public Task<User> GetByApi();
    }

    public class UserService : IUserService
    {

        private readonly DataContext _context;

        static HttpClient client = new HttpClient();

        public UserService(DataContext context)
        {
            _context = context;
        }
        public async Task<User> Create(User user)
        {
            User userDb = await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.UserId == user.UserId);
            if (userDb is not null)
                throw new Exception($"User name {user.UserId} already exist.");
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task Delete(int id)
        {
            User userDb = await _context.Users
                .SingleOrDefaultAsync(u => u.Id == id);
            if (userDb is null)
                throw new Exception($"User {id} not found.");
            _context.Users.Remove(userDb);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAll() => await _context.Users.ToListAsync();


        public async Task<User> GetById(int id)
        {
            User userDb = await _context.Users
                .SingleOrDefaultAsync(u => u.Id == id);

            if (userDb is null)
                throw new Exception($"User {id} not found.");

            return userDb;
        }

        public async Task Update(User userIn, int id)
        {
            if (userIn.Id != id)
                throw new Exception("Router id differs User id.");

            User userDb = await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == id);

            if (userDb is null)
                throw new Exception($"User {id} not found.");

            _context.Entry(userIn).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserAsync(string id)
        {
            
            User user = null;
            client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/posts/" + id);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync(id); 
            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadAsAsync<User>();
                //var contentRead = await response.Content.ReadAsStringAsync();
                //if (!string.IsNullOrEmpty(contentRead))
                //     Create(JsonSerializer.Deserialize<User>(contentRead));
            }
            return user;
        }

        static async Task<Uri> CreateUserAsync(User user)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "TestDev", user);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }


    }

}

