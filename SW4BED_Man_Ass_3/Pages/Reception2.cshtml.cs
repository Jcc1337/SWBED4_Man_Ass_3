using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SW4BED_Man_Ass_3.Pages
{
    [Authorize("Reception")]
    public class Reception2Model : PageModel
    {
        public void OnGet()
        {
        }
    }
}
