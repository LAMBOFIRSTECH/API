using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace api_bd.Controllers;

[ApiController]
[Route("[controller]")]


public class contactsController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public contactsController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {

            _httpClient.BaseAddress = new Uri("http://your-api-url/"); //On recupere l'uri depuis appsettings
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            HttpResponseMessage response = await _httpClient.GetAsync("api/contacts");
            response.EnsureSuccessStatusCode();

            // Read the response content
            string responseBody = await response.Content.ReadAsStringAsync();

            // Return the response
            return Ok(responseBody);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
  
}

