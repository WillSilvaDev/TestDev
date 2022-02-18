using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using TestDev.Helpers;
using TestDev.Model;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using static TestDev.Helpers.DataContext;

namespace TestDev.Services
{

    public interface IUserRequestService
    {
        //public Task<User> Create(User user);
        //public Task<User> GetById(int id);
        //public Task Update(User userIn, int id);
        //public Task Delete(int id);
        //public Task<List<User>> GetAll();

        public Task<User> GetUserAsync(string id);
        
        //public Task<User> Get();        
    }
    public class UserRequestService : IUserRequestService
    {
        
            private readonly IMemoryCache _memoryCache;


        //public void OnGet()
        //{
        //    var CurrentDateTime = DateTime.Now;

        //    if (!_memoryCache.TryGetValue(CacheKeys.Entry, out DateTime cacheValue))
        //    {
        //        cacheValue = CurrentDateTime;

        //        var cacheEntryOptions = new MemoryCacheEntryOptions()
        //            .SetSlidingExpiration(TimeSpan.FromSeconds(3));

        //        _memoryCache.Set(CacheKeys.Entry, cacheValue, cacheEntryOptions);
        //    }

        //  var CacheCurrentDateTime = cacheValue;
        //}


        //private readonly DataContext _context;
        //private readonly HttpClient _httpClient;
        //private readonly IConfiguration _configuration;
        //static IMemoryCache _memoryCache;
        


        static HttpClient client = new HttpClient();

        //public UserRequestService(HttpClient httpClient, IConfiguration configuration, IMemoryCache memoryCache)
        //{
        //    _httpClient = httpClient;
        //    _configuration = configuration;
        //    _memoryCache = memoryCache;
        //}


        public static TEntity CreateMemoryCache<TEntity>
               (IMemoryCache memoryCache, string ulrClient, TEntity result) => memoryCache.GetOrCreate(ulrClient, e =>
               {
                   e.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                   return result;
               });

        //public async Task<User> GetUserAsync(string id)
        //{

        //    User user = null;
        //    client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/posts/" + id);
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(
        //        new MediaTypeWithQualityHeaderValue("application/json"));
        //    HttpResponseMessage response = await client.GetAsync(id);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        user = await response.Content.ReadAsAsync<User>();
        //        response = await client.PostAsJsonAsync(
        //       "https://localhost:7046", user);
        //        response.EnsureSuccessStatusCode();
        //    }
        //    return user;
        //}

        public async Task<User> GetUserAsync(string id)
        {
            string Url = "https://jsonplaceholder.typicode.com/posts/" + id;
            User user = null;
            client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/posts/" + id);
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync(id);

            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadAsAsync<User>();                                                  
            }
            return user;
        }

        static async Task<Uri> CreateUserAsync(User user)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "https://jsonplaceholder.typicode.com/posts/55", user);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
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
