using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Common.DbHelper;
using DataAccess.Net.DAL;
using DataAccess.Net.DataObject;
using DataAccess.Net.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BE_092024.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : Controller
{
    private IAccountRepository _accountRepository;

    public AccountController(IAccountRepository accountRepository)
    {
        this._accountRepository = accountRepository;
    }
    
    [HttpPost("Account_Register")]
    public async Task<IActionResult> Register([FromBody] AccountDTO account)
    {
        account.PassWord = Security.HashPassword(account.PassWord);

        var user = new User
        {
            UserName = account.UserName,
            PassWord = account.PassWord
        };

        await _accountRepository.RegisterUser(user);

        return Ok(new { message = "User registered successfully!" });
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AccountDTO account)
    {
        var user = await _accountRepository.User_Login(account);

        if (user == null)
        {
            return Unauthorized(new { message = "Invalid credentials!" });
        }

        // Kiểm tra mật khẩu người dùng nhập vào với mật khẩu đã mã hóa
        if (user.PassWord != null && !Security.VerifyPassword(account.PassWord, user.PassWord))
        {
            return Unauthorized(new { message = "Invalid credentials!" });
        }

        // Tạo JWT 
        var token = GenerateJwtToken(user);
        return Ok(new { token });
    }
    
    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.NameId,user.UserId.ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("DLCGnQGYlH4Xfe53aOXP8F6V32eo25gw"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "http://localhost:4200",
            audience: "http://localhost:4200",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
}