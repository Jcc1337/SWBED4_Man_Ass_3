using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SW4BED_Man_Ass_3.Data;
using SW4BED_Man_Ass_3.Models;

namespace SW4BED_Man_Ass_3.Pages
{
    public class WaiterModel : PageModel
    {
        private readonly SW4BED_Man_Ass_3.Data.ApplicationDbContext _context;

        public WaiterModel(SW4BED_Man_Ass_3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public GuestCheckIn Guest { get; set; } = default!;
        
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.GuestReserveds == null || Guest == null)
            {
                return Page();
            }

            _context.GuestCheckIns.Add(Guest);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}

