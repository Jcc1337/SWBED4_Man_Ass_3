using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SW4BED_Man_Ass_3.Data;
using SW4BED_Man_Ass_3.Models;

namespace SW4BED_Man_Ass_3.Pages
{
    [Authorize("Waiter")]
    public class WaiterModel : PageModel
    {
        
        [BindProperty] public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            [DataType(DataType.Date)]
            public DateTime Date { get; set; } = DateTime.Today;

            public int Roomnumber { get; set; }
            public int Adults { get; set; } = 0;
            public int Children { get; set; } = 0;
        }
        
        private readonly SW4BED_Man_Ass_3.Data.ApplicationDbContext _context;

        public WaiterModel(SW4BED_Man_Ass_3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            var guestCheckin = new GuestCheckIn
            {
                RoomNumber = Input.Roomnumber,
                Adults = Input.Adults,
                Children = Input.Children,
                Date = Input.Date
            };

            _context.GuestCheckIns.Add(guestCheckin);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Waiter");
        }

    }
}

