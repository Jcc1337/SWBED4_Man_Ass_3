﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using SW4BED_Man_Ass_3.Data;
using SW4BED_Man_Ass_3.hub;
using SW4BED_Man_Ass_3.Models;

namespace SW4BED_Man_Ass_3.Pages
{
	[Authorize("Reception")]
	public class ReceptionModel : PageModel
    {
        private readonly SW4BED_Man_Ass_3.Data.ApplicationDbContext _context;

        private readonly IHubContext<HubKitchen, IHubKitchen> _HubKitchen;

        public ReceptionModel(SW4BED_Man_Ass_3.Data.ApplicationDbContext context, IHubContext<HubKitchen, IHubKitchen> hubkitchen)
        {
            _context = context;
            Input = new InputModel();
            _HubKitchen = hubkitchen;
        }
        [BindProperty] public InputModel Input { get; set; }

        public class InputModel
        {
	        [Microsoft.Build.Framework.Required]
	        [DataType(DataType.Date)]
	        public DateTime Date { get; set; } = DateTime.Today;

	        public int Adults { get; set; } = 0;
	        public int Children { get; set; } = 0;

        }

        public async Task<IActionResult> OnPostAsync()
        {
	        var guestReserved = new GuestReserved()
	        {
		        Adults = Input.Adults,
		        Children = Input.Children,
		        Date = Input.Date
	        };
	        _context.GuestReserveds.Add(guestReserved);
            await _context.SaveChangesAsync();

            await _HubKitchen.Clients.All.KitchenReload();

            return RedirectToPage("./Reception");
		}
        public IActionResult OnGet()
        {
            return Page();
        }

     
        

        
    }
}
