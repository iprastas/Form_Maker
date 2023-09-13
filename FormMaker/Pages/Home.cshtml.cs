using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FormMaker.Pages
{
	[Authorize]
	public class HomeModel : PageModel
    {
		[BindProperty] public string TypeForm { get; set; } = "";
        public void OnGet()
        {
        }

		public IActionResult OnPostCreate()
		{
			string type = TypeForm;
			return new RedirectToPageResult("/PageForm", new {type});
		}
	}
}
