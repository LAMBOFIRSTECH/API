using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace api_bd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class contactsController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public contactsController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                string bddUri = _configuration.GetValue<string>("BDDUri");

                _httpClient.BaseAddress = new Uri(bddUri);
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await _httpClient.GetAsync("");
                Console.WriteLine(response.StatusCode);
                string responseBody = await response.Content.ReadAsStringAsync();

                return Ok(responseBody);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("tables")]
        public IActionResult GetTables()
        {
            string username = _configuration.GetValue<string>("BDDAuthentication:Username");
            string password = _configuration.GetValue<string>("BDDAuthentication:Password");
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            string connectionStringWithAuth = $"{connectionString};User Id={username};Password={password}";

            using (var connection = new SqlConnection(connectionStringWithAuth))
            {
                connection.Open();

                DataTable tables = connection.GetSchema("Tables");

                var tableNames = tables.Rows.Cast<DataRow>().Select(row => row["TABLE_NAME"].ToString());

                return Ok(tableNames);
            }
        }

        // ...


    }
}
