using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FormMaker.Pages
{
    [Authorize]
    public class FormTableModel : PageModel
    {
        public int FormID { get; set; }
        public string FormMaster { get; set; } = "";
		public string TypeForm { get; set; } = "";
		public string Name { get; set; } = "";
        static List<FormColumn> formColumns = new List<FormColumn>();
        public int ColumnsNum { get; set; }
        static List<FormRow> formRows = new List<FormRow>();
        public int RowNum { get; set; }

        public void OnGet()
        {
            FormID = Convert.ToInt32(RouteData.Values["FormID"]);
            FormMaster = Convert.ToString(RouteData.Values["FormMaster"]);
            TypeForm = Convert.ToString(RouteData.Values["type"]);
            Name = Convert.ToString(RouteData.Values["Name"]);
            object? col = RouteData.Values["col"];
            object? row = RouteData.Values["row"];
            
        }

        public IActionResult OnPostRow(int formid, string type)
        {
            return new RedirectToPageResult("/formRow", new { formid, type });
        }
        public IActionResult OnPostColumn(int formid, string type)
        {
            return new RedirectToPageResult("/formColumn", new { formid, type });
        }
    }
}
