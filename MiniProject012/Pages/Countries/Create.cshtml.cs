using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace MiniProject012.Pages.Countries
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "The Country Name is required.")]
        [MaxLength(100, ErrorMessage = "The Country Name canot exceed 100 characters")]
        public string CountryName { get; set; } = "";

        [BindProperty]
        public decimal? Area { get; set; }

        [BindProperty]
        public decimal? Population { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                return;
            }

            try
            {
                string connectionString = "Data Source=LAPTOP-2JK6GIJ7\\SQLEXPRESS;Initial Catalog=MiniProjects;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO MiniProject012_Countries " +
                        "(CountryName, Area, Population) VALUES " +
                        "(@CountryName, @Area, @Population)";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@CountryName", CountryName);
                        cmd.Parameters.AddWithValue("@Area", Area);
                        cmd.Parameters.AddWithValue("@Population", Population);

                        cmd.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception e)
            {
                return;
            }

            Response.Redirect("/Countries/Index");
        }
    }
}
