using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MewtwoCounter.Pages
{
    public class IndexModel : PageModel
    {
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
            Count = db.Counters.Count();
            Last = db.Counters.OrderByDescending(x => x.Id).FirstOrDefault();
        }

        public IActionResult OnPost()
        {
            var counter = new Counter
            {
                Date = DateTime.UtcNow
            };
            db.Counters.Add(counter);
            db.SaveChanges();

            return RedirectToPage();
        }
    }
}
