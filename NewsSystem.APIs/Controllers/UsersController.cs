using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;//for user manager
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NewsSystem.DAL.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APis.Identity.Day3.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IConfiguration _configuration;//to get secret key
    private readonly UserManager<IdentityUser> _userManager;

    public UsersController(IConfiguration configuration,
        UserManager<IdentityUser> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }
    
    #region Login

    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<TokenDto>> Login(LoginDto credentials)
    {
        var user = await _userManager.FindByNameAsync(credentials.UserName);
        if (user == null)
        {
            return NotFound();
        }

        var isAuthenitcated = await _userManager.CheckPasswordAsync(user, credentials.Password);
        if (!isAuthenitcated)
        {
            return Unauthorized();
        }

        var claimsList = await _userManager.GetClaimsAsync(user);

        var secretKeyString = _configuration.GetValue<string>("SecretKey") ?? string.Empty;
        var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKeyString);
        var secretKey = new SymmetricSecurityKey(secretKeyInBytes);

        //Combination SecretKey, HashingAlgorithm
        var siginingCreedentials = new SigningCredentials(secretKey,
            SecurityAlgorithms.HmacSha256Signature);

        var expiry = DateTime.Now.AddDays(1);

        var token = new JwtSecurityToken(
            claims: claimsList,
            expires: expiry,
            signingCredentials: siginingCreedentials);

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenString = tokenHandler.WriteToken(token);

        return new TokenDto(tokenString, expiry);
    }

    #endregion 
    
    #region Register

    [HttpPost]
    [Route("Register")]
    public async Task<ActionResult> Register(RegisterDto registerDto)
    {
        var userToAdd = new IdentityUser
        {
            UserName = registerDto.UserName,
            Email = registerDto.Email
        };

        var result = await _userManager.CreateAsync(userToAdd, registerDto.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userToAdd.Id)
        };

        await _userManager.AddClaimsAsync(userToAdd, claims);

        return NoContent();
    }

    #endregion

}
