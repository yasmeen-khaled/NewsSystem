using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
using NewsSystem.DAL.Dtos;
using NewsSystem.MVC.RepoService;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace NewsSystem.MVC.Areas.Identity.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserRepository _userService;

        public UsersController(UserRepository userService)
        {
            _userService = userService;
        }
        #region Login

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<TokenDto>> Login(LoginDto credentials)
        {
            var token = await _userService.RequestLogin(credentials);
            if(token == null)
                return NotFound();
            Response.Cookies.Append("authorizationToken", token.Token);//, new() { Expires=token.Expiry});
            return RedirectToAction("Index", "News");
        }

        #endregion
        
    }
}
