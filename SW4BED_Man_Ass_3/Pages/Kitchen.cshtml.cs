using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SW4BED_Man_Ass_3.Data;
using SW4BED_Man_Ass_3.Models;

namespace SW4BED_Man_Ass_3.Pages
{


    [Authorize("Kitchen")]
    public class KitchenModel : PageModel
    {


        private readonly SW4BED_Man_Ass_3.Data.ApplicationDbContext _context;

        public int adultsExpected;
        public int childrenExpected;
        public int adultsCheckedIn;
        public int childrenCheckedIn;

        public KitchenModel(SW4BED_Man_Ass_3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        

        
    }
}
