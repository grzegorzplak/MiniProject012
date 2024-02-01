using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniProject012.Models;
using System.Data.Common;
using System.Data.SqlClient;

namespace MiniProject012.Pages.Countries
{
    public class IndexModel : PageModel
    {
        public List<Country> countries = new List<Country>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=LAPTOP-2JK6GIJ7\\SQLEXPRESS;Initial Catalog=MiniProjects;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT Id, CountryName, Area, Population FROM MiniProject012_Countries";
                    
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Country country = new Country();
                                country.Id = reader.GetInt32(0);
                                country.CountryName = reader.GetString(1);
                                country.Area = reader.GetDecimal(2);
                                country.Population = reader.GetDecimal(3);
                                countries.Add(country);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
