using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace MiniProject012.Pages.Countries
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public int Id { get; set; }

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
            string requestId = Request.Query["id"];

            try
            {
                string connectionString = "Data Source=LAPTOP-2JK6GIJ7\\SQLEXPRESS;Initial Catalog=MiniProjects;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM MiniProject012_Countries WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", requestId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Id = reader.GetInt32(0);
                                CountryName = reader.GetString(1);
                                Area = reader.GetDecimal(2);
                                Population = reader.GetDecimal(3);
                            }
                            else
                            {
                                Response.Redirect("/Countries/Index");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Response.Redirect("/Countries/Index");
            }
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
                    string sql = "UPDATE MiniProject012_Countries SET " +
                        " CountryName = @CountryName " +
                        " , Area = @Area " +
                        " , Population = @Population " +
                        " WHERE id = @id ";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@CountryName", CountryName);
                        cmd.Parameters.AddWithValue("@Area", Area);
                        cmd.Parameters.AddWithValue("@Population", Population);
                        cmd.Parameters.AddWithValue("@id", Id);

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
