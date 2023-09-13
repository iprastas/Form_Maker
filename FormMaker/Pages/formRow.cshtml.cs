using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FormMaker.Pages
{
    [Authorize]
    public class formRowModel : PageModel
    {
        public int FormID { get; set; }
        public int FormRow { get; set; }
        public string Name { get; set; } = "";
        public string Note { get; set; } = "";
        public int Type { get; set; }
        public string Unit { get; set; } = "";
        public string Balancesign { get; set; } = "";
        public string Okp { get; set; } = "";
        public string Measure { get; set; } = "";
        public string Compute { get; set; } = "";
        public int Mandatory { get; set; }
        public int Copyng { get; set; }

        public void OnGet()
        {
        }
    }
}
