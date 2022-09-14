using FaithLife.Api.Data;
using FaithLife.Api.DTOs;
using FaithLife.Api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FaithLife.Api.Controllers
{
   
    [Route("api/Account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;
   

        public AccountController(UserManager<ApplicationUser> userManager,
                                    SignInManager<ApplicationUser> signInManager, 
                                    IConfiguration config 
                                    )
            
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            
        }

        [Route("/Register")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] Register register)
        {

            if (ModelState.IsValid)
            {
                var  userExit = await _userManager.FindByEmailAsync(register.Email);

                if (userExit != null)
                {
                    return BadRequest("Email already exist");
                                                  
                    
                }

                var user = new ApplicationUser
                {
                    UserName = register.Email,
                    Email = register.Email,
                    PhoneNumber = register.PhoneNumber,
                    EmailConfirmed =false
                };

                var result = await _userManager.CreateAsync(user, register.Password);
                

                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var email_body = "Please confirm your email address <a href=\"#URL#\">Click here </a> ";

                    // https://localhost:8080/account/verifyemail/userid=sdas&code=dasdasd 
                    var callback_url = Request.Scheme + "://" + Request.Host + Url.Action("ConfirmEmail", "Account",
                        new { userId = user.Id, code = code });

                    var body = email_body.Replace("#URL#",
                        callback_url);

                    // SEND EMAIL
                    var resultA = SendEmail(body, user.Email);

                    if (resultA)
                        return Ok("Please verify your email, through the verification email we have just sent.");

                    

                    return Ok("Please request an email verification link");
                
                }

                return BadRequest();             
      

            }

            return BadRequest();


        }



        [HttpPost("/Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {


                var existingUser = await _userManager.FindByNameAsync(model.Email);

                if(existingUser == null)
                {
                    return BadRequest("Invalid payload");
                }

                if (!existingUser.EmailConfirmed)
                {
                    return BadRequest("Email need to be confirmed");
                }

                var userE = await _userManager.CheckPasswordAsync(existingUser, model.Password);

                if (!userE)
                {
                    return BadRequest("Invalid Credentials");


                }

                var jwtToken = GenerateJwtToken(existingUser);

                return Ok(new AuthResult()
                {
                    Token = jwtToken,
                    Result = true
                });

            }

            return BadRequest(model);
        }

        [Route("ConfirmEmail")]
        [HttpGet]
        
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return BadRequest("Invalid email confirmation url");
            }
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return BadRequest("Invalid email Parameter");

            }

            //code = Encoding.UTF8.GetString(Convert.FromBase64String(code));

            var result = await _userManager.ConfirmEmailAsync(user, code);

            var status = result.Succeeded ? "Thank you for confirming your email"
                : "Your email is not confirmed, please confirm your email";

            return Ok(status);
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_config.GetSection("JwtConfig:Secret").Value);

            // Token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, value:user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
            }),

                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

        private bool SendEmail(string body, string email)
        {
            // Create client
            var client = new RestClient("https://api.mailgun.net/v3");

            var request = new RestRequest("", Method.Post);

            client.Authenticator =
                new HttpBasicAuthenticator("api", _config.GetSection("EmailConfig:API_KEY").Value);

            request.AddParameter("domain", "sandbox877c025a16e74be2988d9d599d5deb65.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "MailgunSandbox<postmaster@sandbox877c025a16e74be2988d9d599d5deb65.mailgun.org>");
            request.AddParameter("to", "isaaccrown26@gmail.com");
            request.AddParameter("subject", "Email Verification ");
            request.AddParameter("text", body);
            request.Method = Method.Post;

            var response = client.Execute(request);

            return response.IsSuccessful;
        }

    }
}
