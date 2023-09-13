using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Npgsql;
using NpgsqlTypes;
using static System.Net.Mime.MediaTypeNames;

namespace FormMaker.Pages
{
    [Authorize]
    public class PageFormModel : PageModel
    {
        public string TypeForm { get; set; } = "";
        public bool IsMaster { get; set; } = false;
        public bool IsDaughter { get; set; } = false;
        public bool IsSimple { get; set; } = false;
        public bool IsAnalitic { get; set; } = false;

        public ICollection<SelectListItem> Items = new List<SelectListItem>();
        [BindProperty] public string FormMaster { get; set; } = "";
        [BindProperty] public int FormID { get; set; }
        [BindProperty] public int Period { get; set; }
        [BindProperty] public int HasSubject { get; set; }
        [BindProperty] public int HasFacility { get; set; }
        [BindProperty] public int HasTerritory { get; set; }
        [BindProperty] public string ShortName { get; set; } = "";
        [BindProperty] public string Name { get; set; } = "";
        [BindProperty] public string? Note { get; set; } = "";
        [BindProperty] public string? Department { get; set; } = "";
        [BindProperty] public DateTime? Upto { get; set; }
        public int Modeenter { get; set; } = 2;

        string? connectionString;
        readonly IConfiguration? configuration;
        public PageFormModel(IConfiguration? _configuration)
        {
            configuration = _configuration;
            connectionString = configuration == null ? string.Empty : configuration.GetConnectionString("DefaultConnection");
        }
        public void OnGet()
        {
            TypeForm = Convert.ToString(RouteData.Values["type"]);
            switch (RouteData.Values["type"])
            {
                case "Master":
                    IsMaster = true;
                    break;
                case "Daughter":
                    IsDaughter = true;
                    break;
                case "Simple": 
                    IsSimple = true;
                    break;
                case "Analitic":
                    IsAnalitic = true;
                    break;
            }
            
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(connectionString))
            {
                conn.Open();
                Npgsql.NpgsqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select max(form) from svod2.form";
                Npgsql.NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    FormID = reader.GetInt32(0) + 1;
                }
            }

            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(connectionString))
            {
                conn.Open();
                Npgsql.NpgsqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select form, coalesce(t.name, t.short) from svod2.form t where modeenter=2 and " +
                    "coalesce(upto,current_date)>=current_date and master is null order by name;";
                Npgsql.NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = reader.GetInt32(0).ToString();
                    if (!reader.IsDBNull(1)) 
                        item.Text = reader.GetString(1);
                    Items.Add(item);
                }
            }
        }
        public IActionResult OnPostCancel()
        {
            return Redirect("/Home");
        }
        public IActionResult OnPostCreate(string type)
        {
            switch (type)
            {
                case "Master":
                    IsMaster = true;
                    break;
                case "Daughter":
                    IsDaughter = true;
                    break;
                case "Simple":
                    IsSimple = true;
                    break;
				case "Analitic":
					IsAnalitic = true;
                    Modeenter = 10;
					break;
			}
            string path = $"{type}Form.txt";
            if (IsDaughter)
            {
                string text = "INSERT INTO svod2.form(form,master,periodic,modeenter,hassubject,hasfacility, " +
                    "hasterritory,short,name,upto,note,department,changedate) VALUES ";
                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    writer.Write(text);
                }
                int master = int.Parse(FormMaster);
                string values = $"({FormID},{master},";
                using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    Npgsql.NpgsqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "select periodic,modeenter,hassubject,hasfacility,hasterritory," +
                        $"upto,note,department,changedate from svod2.form t where form={master}";
                    Npgsql.NpgsqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                            values += reader.GetInt32(0) + ",";
                        if (!reader.IsDBNull(1))
                            values += reader.GetInt32(1) + ",";
                        if (!reader.IsDBNull(2))
                            values += reader.GetInt32(2) + ",";
                        if (!reader.IsDBNull(3))
                            values += reader.GetInt32(3) + ",";
                        if (!reader.IsDBNull(4))
                            values += reader.GetInt32(4) + ",";
                        if (!reader.IsDBNull(5))
                            values += "\'" + reader.GetString(5) + $"\',\'{ShortName}\',\'{Name}\',";
                        else
                            values += "\'" + $"{DBNull.Value}" + $"\',\'{ShortName}\',\'{Name}\',";
                        if (!reader.IsDBNull(6))
                            values += "\'" + reader.GetString(6) + "\',";
                        else
                            values += "\'" + $"{DBNull.Value}" + "\',";
                        if (!reader.IsDBNull(7))
                            values += "\'" + reader.GetString(7) + "\',";
                        else
                            values += "\'" + $"{DBNull.Value}" + "\',";
                        if (!reader.IsDBNull(8))
                            values += "\'" + reader.GetDateTime(8) + "\')";
                        else
                            values += $"{DBNull.Value})";
                    }
                }
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.Write(values);
                }
            }
            else
            {
                string text = "INSERT INTO svod2.form(form,periodic,modeenter,hassubject,hasfacility, " +
                    "hasterritory,short,name,upto,note,department,changedate) VALUES ";
                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    writer.Write(text);
                }
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.Write($"({FormID},{Period},{Modeenter},{HasSubject},{HasFacility},{HasTerritory}," +
                        $"\'{ShortName}\',\'{Name}\',\'{Upto}\',\'{Note}\',\'{Department}\',\'{DateTime.Now}\')");
                }
                //using (NpgsqlConnection ins = new NpgsqlConnection(connectionString))
                //{
                //    ins.Open();
                //    string connINS = "INSERT INTO svod2.form(form,periodic,modeenter,hassubject,hasfacility, " +
                //        "hasterritory,short,name,upto,note,department,changedate) VALUES " +
                //        "(@form, @periodic, @modeenter, @hassubject, @hasfacility, @hasterritory," +
                //        $" @short, @name, @upto, @note, @department, @changedate)";
                //    NpgsqlCommand plcom = new NpgsqlCommand(connINS, ins);
                //    plcom.Parameters.Add("@form", NpgsqlDbType.Integer).Value = FormID;
                //    plcom.Parameters.Add("@periodic", NpgsqlDbType.Integer).Value = Period;
                //    plcom.Parameters.Add("@modeenter", NpgsqlDbType.Integer).Value = Modeenter;
                //    plcom.Parameters.Add("@hassubject", NpgsqlDbType.Smallint).Value = Convert.ToInt16(HasSubject);
                //    plcom.Parameters.Add("@hasfacility", NpgsqlDbType.Smallint).Value = Convert.ToInt16(HasFacility);
                //    plcom.Parameters.Add("@hasterritory", NpgsqlDbType.Smallint).Value = Convert.ToInt16(HasTerritory);
                //    plcom.Parameters.Add("@short", NpgsqlDbType.Varchar).Value = ShortName != null ? ShortName : DBNull.Value;
                //    plcom.Parameters.Add("@name", NpgsqlDbType.Varchar).Value = Name != null ? Name : DBNull.Value;
                //    plcom.Parameters.Add("@upto", NpgsqlDbType.Timestamp).Value = Upto;
                //    plcom.Parameters.Add("@note", NpgsqlDbType.Varchar).Value = Note != null ? Note : DBNull.Value;
                //    plcom.Parameters.Add("@department", NpgsqlDbType.Varchar).Value = Department != null ? Department : DBNull.Value;
                //    plcom.Parameters.Add("@changedate", NpgsqlDbType.Timestamp).Value = DateTime.Now;

                //    plcom.ExecuteNonQuery();

                //    ins.Close();
                //}
                if (IsMaster)
                {
                    return Redirect("/Home");
                }
            }
            return new RedirectToPageResult("/FormTable", new { FormID, FormMaster, type, Name });
        }
    }
}
