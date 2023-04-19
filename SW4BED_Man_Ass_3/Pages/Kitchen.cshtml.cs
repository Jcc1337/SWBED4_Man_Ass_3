using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SW4BED_Man_Ass_3.Data;
using SW4BED_Man_Ass_3.Models;

namespace SW4BED_Man_Ass_3.Pages
{


    [Authorize]
    public class KitchenModel : PageModel
    {


        private readonly SW4BED_Man_Ass_3.Data.ApplicationDbContext _context;

        public int adultsExpected;
        public int childrenExpected;
        public int totalExpected;
        public int adultsCheckedIn;
        public int childrenCheckedIn;
        public DateTime dateNow;


        public KitchenModel(SW4BED_Man_Ass_3.Data.ApplicationDbContext context)
        {
            _context = context;
            dateNow= DateTime.Now;
        }



        [BindProperty] public GuestCheckIn guestCheckIn { get; set; }

        [BindProperty] public GuestReserved guestReserved { get; set; }

        public async Task OnGet()
        {
            var dbExpected = await _context.GuestReserveds
                .Where(b => b.Date.Day == dateNow.Date.Day && b.Date.Month == dateNow.Date.Month)
                .ToListAsync();
            foreach (var item in dbExpected)
            {
                adultsExpected += item.Adults;
                childrenExpected += item.Children;
                
            }
            totalExpected= adultsExpected+chi;

            var dbCheckedIn = await _context.GuestCheckIns
                .Where(b => b.Date.Day == dateNow.Date.Day && b.Date.Month == dateNow.Date.Month)
                .ToListAsync();

            foreach (var item in dbCheckedIn)
            {
                adultsCheckedIn += item.Adults;
                childrenCheckedIn += item.Children;
            }


        }

        public async Task OnPost()
        {
            var dbExpected = await _context.GuestReserveds
                .Where(b => b.Date.Day == guestReserved.Date.Day && b.Date.Month == guestReserved.Date.Month)
                .ToListAsync();

            if (dbExpected == null)
            {
                ModelState.AddModelError("Input.Date", "No guest on this date");
                return;
            }

            foreach (var item in dbExpected)
            {
                adultsExpected += item.Adults;
                childrenExpected += item.Children;
            }

            var dbCheckedIn = await _context.GuestCheckIns
               .Where(b => b.Date.Day == guestCheckIn.Date.Day && b.Date.Month == guestCheckIn.Date.Month)
               .ToListAsync();

            if (dbCheckedIn == null)
            {
                ModelState.AddModelError("Input.Date", "No guest on this date");
                return;
            }

            foreach (var item in dbCheckedIn)
            {
                adultsCheckedIn += item.Adults;
                childrenCheckedIn += item.Children;
            }
        }



    }
        

        
    }
