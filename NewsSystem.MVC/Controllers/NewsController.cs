using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsSystem.DAL;
using NewsSystem.DAL.Helpers;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace NewsSystem.MVC.Controllers
{
    public class NewsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _newsURL;
        private readonly string _authorsURL;
        private readonly HttpContext _httpContext;
        private readonly bool isSigned = false;
        public NewsController(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _newsURL = "https://localhost:7107/api/News";
            _authorsURL = "https://localhost:7107/api/Authors";

            _httpContext = httpContextAccessor.HttpContext;
            var jwtToken = _httpContext.Request.Cookies["authorizationToken"];
            if (jwtToken != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                isSigned = true;
            }
        }

        // GET: NewsController
        public async Task<ActionResult> Index()
        {
            ViewBag.isSigned = isSigned;
            // Make GET request to API
            HttpResponseMessage response = await _httpClient.GetAsync(_newsURL);
            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                var content = await response.Content.ReadFromJsonAsync<List<News>>();
                return View(content);
            }
            return BadRequest();
        }

        // GET: NewsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_newsURL + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<News>();
                return View(content);
            }
            return View();
        }

        // GET: NewsController/Create
        public async Task<ActionResult> Create()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_authorsURL);
            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                var content = await response.Content.ReadFromJsonAsync<List<Author>>();
                ViewData["AuthorsIds"] = new SelectList(content, "Id", "Name");
            }
            return View();
        }

        // POST: NewsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Title,news,PublicationDate,CreationDate,AuthorId")] News _news
                                                        , IFormFile ImageFile)
        {
            try
            {
                var image = ImageHelpers.ImageToByteArray(ImageFile);
                _news.Image = image;
                string requestBody = JsonSerializer.Serialize(_news);
                HttpContent httpContent = new StringContent(requestBody, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(_newsURL, httpContent);
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

        // GET: NewsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_newsURL + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                var content = await response.Content.ReadFromJsonAsync<News>();
                //**get authors
                HttpResponseMessage response2 = await _httpClient.GetAsync(_authorsURL);
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    var content2 = await response2.Content.ReadFromJsonAsync<List<Author>>();
                    ViewData["AuthorsIds"] = new SelectList(content2, "Id", "Name");
                }
                return View(content);
            }
            return BadRequest();
        }

        // POST: NewsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id
            , [Bind("Id,Title,news,PublicationDate,CreationDate,AuthorId,Image")] News _news
                                                        , IFormFile ImageFile)
        {
            if (id != _news.Id)
            {
                return BadRequest();
            }

            if(ImageFile != null)
            {
                var image = ImageHelpers.ImageToByteArray(ImageFile);
                _news.Image = image;
            }
            if (ModelState.IsValid ||(ModelState.ErrorCount == 1 && ModelState["ImageFile"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid))
            {
                string requestBody = JsonSerializer.Serialize(_news);
                HttpContent httpContent = new StringContent(requestBody, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PutAsync(_newsURL + "/" + id, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(_news);
        }

        // GET: NewsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_newsURL + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<News>();
                return View(content);
            }
            return View();
        }

        // POST: NewsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync(_newsURL + "/" + id);
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
