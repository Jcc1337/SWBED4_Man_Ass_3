using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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


		public KitchenModel(SW4BED_Man_Ass_3.Data.ApplicationDbContext context)
		{
			_context = context;
		}

		[BindProperty(SupportsGet = true)]
		public DateTime Date { get; set; }
		[BindProperty]
		public GuestReserved guestReserved { get; set; }

		public async Task OnGet()
		{
			Date = DateTime.Today;

			var dbExpected = await _context.GuestReserveds
				.Where(b => b.Date.Date == Date.Date)
				.ToListAsync();
			foreach (var item in dbExpected)
			{
				adultsExpected += item.Adults;
				childrenExpected += item.Children;

			}
			totalExpected = adultsExpected + childrenExpected;

			var dbCheckedIn = await _context.GuestCheckIns
				.Where(b => b.Date.Date == Date.Date)
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
				.Where(b => b.Date.Date == Date.Date)
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
				totalExpected = adultsExpected + childrenExpected;
			}

			var dbCheckedIn = await _context.GuestCheckIns
				.Where(b => b.Date.Date == Date.Date)
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
