using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MewtwoCounter.Pages
{
    public class IndexModel : PageModel
    {
        private const string MewTwoCookieName = nameof(MewTwoCookieName);

        private readonly CounterContext db;

        [BindProperty]
        public int Count { get; set; }
        [BindProperty]
        public Counter Last { get; set; }

        public IndexModel(CounterContext db)
        {
            this.db = db;
        }

        public void OnGet()
        {
            Guid counterKey = GetCounterKey();
            if (counterKey == default(Guid) && 
                (!Request.Cookies.TryGetValue(MewTwoCookieName, out string key) || !Guid.TryParse(key, out counterKey)))
            {
                counterKey = Guid.NewGuid();
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(30),
                    Secure = false
                };
                Response.Cookies.Append(MewTwoCookieName, counterKey.ToString(), cookieOptions);
            }

            Count = db.Counters
                .Count(x => x.CounterKey == counterKey);

            Last = db.Counters
                .Where(x => x.CounterKey == counterKey)
                .OrderByDescending(x => x.Id)
                .FirstOrDefault();
        }

        private Guid GetCounterKey()
        {
            Guid counterKey;
            if(!Guid.TryParse(Request.Query["k"], out counterKey))
            {
                return default(Guid);
            }

            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(30),
                Secure = false
            };
            Response.Cookies.Append(MewTwoCookieName, counterKey.ToString(), cookieOptions);

            return counterKey;
        }

        public IActionResult OnPost()
        {
            Guid counterKey;
            if (!Request.Cookies.TryGetValue(MewTwoCookieName, out string key) || !Guid.TryParse(key, out counterKey))
            {
                return RedirectToPage();
            }

            var counter = new Counter
            {
                Date = DateTime.UtcNow,
                CounterKey = counterKey
            };
            db.Counters.Add(counter);
            db.SaveChanges();

            return RedirectToPage();
        }
    }
}
