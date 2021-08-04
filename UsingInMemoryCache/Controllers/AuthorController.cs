using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using UsingInMemoryCache.Data;
using UsingInMemoryCache.Models;

namespace UsingInMemoryCache.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorController : ControllerBase
    {

        private readonly CourseLibraryDbContext _context;
        private readonly IMemoryCache _memoryCache;

        public AuthorController(CourseLibraryDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cacheOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5)
            };

            //means that data will remove from the cache only if it is not accessed in the last 5 minutes
            var slidingCacheOptions = new MemoryCacheEntryOptions()
            {
                SlidingExpiration = TimeSpan.FromMinutes(5)
            };

            var combinedCacheOptions = new MemoryCacheEntryOptions()
            {
                SlidingExpiration = TimeSpan.FromMinutes(5),
                AbsoluteExpiration = DateTime.Now.AddMinutes(60),
                Priority = CacheItemPriority.High
            };

            var cacheKey = "GET_ALL_AUTHORS";
            if (_memoryCache.TryGetValue(cacheKey, out List<Author> authorsFromCache))
            {
                return Ok(authorsFromCache);
            }
            var authorsFromDb = await _context.Authors.ToListAsync();
            _memoryCache.Set(cacheKey, authorsFromDb, cacheOptions);
            return Ok(authorsFromDb);
        }
    }
}