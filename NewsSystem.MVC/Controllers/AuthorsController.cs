using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsSystem.DAL;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace NewsSystem.MVC.Controllers
{
    //[Authorize]
    public class AuthorsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _authorsURL;
        private readonly HttpContext _httpContext;
        public AuthorsController(HttpClient httpClient , IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _authorsURL = "https://localhost:7107/api/Authors";

            _httpContext = httpContextAccessor.HttpContext;
            var jwtToken = _httpContext.Request.Cookies["authorizationToken"];
            if (jwtToken != null)
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            
        }
        // GET: AuthorsController
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_authorsURL);
            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                var content = await response.Content.ReadFromJsonAsync<List<Author>>();
                return View(content);
            }
            return BadRequest();
        }

        // GET: AuthorsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_authorsURL + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                var content = await response.Content.ReadFromJsonAsync<Author>();
                return View(content);
            }
            return BadRequest();
        }

        // GET: AuthorsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthorsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Id,Name")] Author author)
        {
            try
            {
                string requestBody = JsonSerializer.Serialize(author);
                HttpContent httpContent = new StringContent(requestBody, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(_authorsURL , httpContent);
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return RedirectToAction(nameof(Index));
                }
                return BadRequest();
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_authorsURL + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                var content = await response.Content.ReadFromJsonAsync<Author>();
                return View(content);
            }
            return BadRequest();
        }

        // POST: AuthorsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Id,Name")] Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                string requestBody = JsonSerializer.Serialize(author);
                HttpContent httpContent = new StringContent(requestBody, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PutAsync(_authorsURL + "/" + id, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(author);
        }

        // GET: AuthorsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_authorsURL + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<Author>();
                return View(content);
            }
            return BadRequest();
        }

        // POST: AuthorsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync(_authorsURL + "/" + id);
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return RedirectToAction(nameof(Index));
                }
                else return BadRequest();
            }
            catch
            {
                return View();
            }
        }
    }
}
