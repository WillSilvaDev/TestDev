using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TestDev.Helpers;
using TestDev.Model;
using System.Net.Http.Headers;

namespace TestDev.Services
{
    public interface IUserRequestService
    {
        public Task<User> Create(User user);
        public Task<User> GetUserAsync(string id);           
    }
    public class UserRequestService : IUserRequestService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;
        static private HttpClient client = new HttpClient();

        public UserRequestService(IMemoryCache memoryCache, IConfiguration configuration, DataContext context)
        {
            _memoryCache = memoryCache;
            _configuration = configuration;
            _context = context;
        }

        /// <summary>
        /// requested a user by id Via HttpGet save in cache memory
        /// and calls the method create new user in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>                     
        public async Task<User> GetUserAsync(string id)
        {
            string url = "https://jsonplaceholder.typicode.com/posts/" + id;
            User user = null;
            client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/posts/" + id);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync(id);            

            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadAsAsync<User>();                
                await Create(user);
            }
            return user;
        }

        /// <summary>
        /// Create a new user on database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> Create(User user)
        {
            User userDb = await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == user.Id);
            if (userDb is not null)
                throw new Exception($"User {user.Id} already exist.");
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }




























        //public async Task<User> Get()
        //{
        //    try
        //    {
        //        if (_memoryCache.TryGetValue("userData", out User user))
        //            return user;

        //        var response = await _httpClient.GetAsync(_configuration["https://jsonplaceholder.typicode.com/posts/1"]);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var contentRead = await response.Content.ReadAsStringAsync();
        //            if (!string.IsNullOrEmpty(contentRead))
        //                return MemoryCacheHelper.CreateMemoryCache(_memoryCache, "userData", JsonSerializer.Deserialize<User>(contentRead));
        //        }
        //        return new User();
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("User not found.");
        //    }
        //}



        //static async Task<Uri> CreateUserAsync(User user)
        //{
        //    HttpResponseMessage response = await client.PostAsJsonAsync(
        //        "https://localhost:7046", user);
        //    response.EnsureSuccessStatusCode();

        //    // return URI of the created resource.
        //    return response.Headers.Location;
        //}

    }
}
