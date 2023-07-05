using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace NewsSystem.Mvc.RepoService
{
    public class NewsRepository// : IGenericRepository<News>
    {
        //private readonly HttpClient _httpClient;
        //private readonly HttpContext _httpContext;

        //private readonly string _URL;

        //public NewsRepository(HttpClient httpClient ,  HttpContext httpContext)
        //{
        //    _httpClient = httpClient;
        //    _URL = "https://localhost:7107/api/Authors";
        //    _httpContext = httpContext;

        //    #region remove this and make proper authorization
        //    var jwtToken = _httpContext.Request.Cookies["token"];
        //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
        //    
        //    #endregion
        //}


        //public async Task<bool> Delete(int id)
        //{
        //    HttpResponseMessage response = await _httpClient.DeleteAsync(_URL + "/" + id);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        string responseData = await response.Content.ReadAsStringAsync();
        //        return true;
        //    }
        //    return false;
        //}

        //public IEnumerable<News> GetAll()
        //{
        //    return _context.Set<News>();
        //}

        //public News? GetById(int id)
        //{
        //    return _context.News.FirstOrDefault(n => n.Id == id);
        //}

        //public void Insert(News obj)
        //{
        //    _context.News.Add(obj);
        //    Save();
        //}

        //public void Save()
        //{
        //    _context.SaveChanges();
        //}

        //public void Update(News obj)
        //{
        //    var toBeUpdated = _context.News.FirstOrDefault(n => n.Id == obj.Id);
        //    if (toBeUpdated != null)
        //    {
        //        toBeUpdated.news = obj.news;
        //        toBeUpdated.Title = obj.Title;
        //        toBeUpdated.PublicationDate = obj.PublicationDate;
        //        toBeUpdated.CreationDate = obj.CreationDate;
        //        toBeUpdated.Image = obj.Image;
        //        Save();
        //        //return true;
        //    }
        //    //return false;
        //}
    }
}
