using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.Metrics;
using System.IO;
using System.Xml.Linq;

namespace FormMaker.Pages
{
    [Authorize]
    public class formColumnModel : PageModel
    {
        public int FormID { get; set; }
        public string TypeForm { get; set; } = "";
        public int FormColumn { get; set; }
        public string Name { get; set; } = "";
        public string Note { get; set; } = "";
        public int Type { get; set; }
        public string Compute { get; set; } = "";
        public string Precompute { get; set; } = "";
        public string Outercompute { get; set; } = "";
        public int Summation { get; set; }
        public int Mandatory { get; set; }
        public int Copyng { get; set; }

        public void OnGet()
        {
            FormID = Convert.ToInt32(RouteData.Values["formid"]);
            TypeForm = Convert.ToString(RouteData.Values["type"]);

        }
        public IActionResult OnPostCancel()
        {
            return Redirect("/FormTable");
        }
        public IActionResult OnPostCreate()
        {
            FormColumn col = new FormColumn();
            col.formId = FormID;
            col.formcolumn = FormColumn;
            col.name = Name;
            col.note = Note;
            col.type = Type;
            col.compute = Compute;
            col.precompute = Precompute;
            col.outercompute = Outercompute;
            col.summation = Summation;
            col.mandatory = Mandatory;
            col.copyng = Copyng;

            string text = "INSERT INTO svod2.formcolumn(form,formcolumn,name,note,compute,precompute, " +
                    "outercompute,summation,mandatory,copyng,changedate) VALUES ";
            //using (StreamWriter writer = new StreamWriter(path, false))
            //{
            //    writer.Write(text);
            //}
            string values = $"({FormID},{FormColumn},{Name},{Note},{Type},{Compute},{Precompute}," +
                $"{Outercompute},{Summation},{Mandatory},{Copyng},{DateTime.Now}";
            //using (StreamWriter writer = new StreamWriter(path, true))
            //{
            //    writer.Write(values);
            //}

            return new RedirectToPageResult("/FormTable", new { FormID, Type, col });
        }
    }
}
