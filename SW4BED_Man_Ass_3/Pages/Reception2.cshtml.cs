using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SW4BED_Man_Ass_3.Data;

namespace SW4BED_Man_Ass_3.Pages
{
    [Authorize("Reception")]
    public class Reception2Model : PageModel
    {
        private readonly ApplicationDbContext _context;
        public string DateNow { get; set; }
        public ListDisplay listDisplay { get; set; }

        public class ListDisplay
        {
            public int RoomNumber { get; set; } = 0;
            public int Adults { get; set; } = 0;
            public int Children { get; set; } = 0;
        }

        public async Task OnGetAsync()
        {
        }
    }
}
