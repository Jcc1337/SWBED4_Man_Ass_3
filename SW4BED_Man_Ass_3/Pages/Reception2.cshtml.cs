using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SW4BED_Man_Ass_3.Data;
using SW4BED_Man_Ass_3.Models;

namespace SW4BED_Man_Ass_3.Pages
{
    [Authorize("Reception")]
    public class Reception2Model : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<GuestCheckIn> GuestCheckedIn { get; set; } = new List<GuestCheckIn>();
        public string DateNow { get; set; }
        public ListDisplay listDisplay { get; set; }

        public class ListDisplay
        {
            public int RoomNumber { get; set; } = 0;
            public int Adults { get; set; } = 0;
            public int Children { get; set; } = 0;
        }

        public Reception2Model(ApplicationDbContext context)
        {
            _context=context;
            listDisplay=new ListDisplay();

            //Udfilterer timer, sekunder osv.
            DateNow = DateTime.Now.Day + "/" + DateTime.Now.Month;
        }

        public async Task OnGetAsync()
        {
            var dbGuestCheckIns = await _context.GuestCheckIns
                .Where(b => b.Date.Day == DateTime.Now.Day && b.Date.Month == DateTime.Now.Month)
                .ToListAsync();

            if (dbGuestCheckIns == null) { RedirectToPage("Error"); return; }

            GuestCheckedIn = dbGuestCheckIns;
        }
    }
}
