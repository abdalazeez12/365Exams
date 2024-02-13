using IbtecarBusiness.Core.Services;
using IbtecarBusiness.Courses.Services;
using IbtecarBusiness.Security;
using IbtecarBusiness.Security.Services;
using IbtecarFramework;
using IbtecarFramework.Apis.Models;
using IbtecarFramework.Apis.Services;
using IbtecarModules.Jwt;
using Imtihani.Helpers;
using Imtihani.Models;
using Imtihani.Models.Requests;
using Imtihani.Models.Responses;
using Imtihani.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static IbtecarFramework.Enums;
using PasswordVerificationResult = Microsoft.AspNetCore.Identity.PasswordVerificationResult;
using Org.BouncyCastle.Asn1.Crmf;
using System.Reflection;
using Newtonsoft.Json.Linq;
using RestSharp;
using Microsoft.AspNetCore.Authorization;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Text.Json;
using IbtecarBusiness.Security.DataContracts;
using System.Linq;
using Microsoft.Extensions.Primitives;
using IbtecarBusiness.Security.Models;
using IbtecarModules.Jwt.Models;
using IbtecarBusiness.Courses.DataContracts;
using Org.BouncyCastle.Ocsp;
using System.Security.Policy;
using System.Web;
using Dapper;
using Microsoft.Data.SqlClient;
using static Org.BouncyCastle.Math.EC.ECCurve;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Imtihani.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        PasswordHasher<IbtecarBusiness.Security.DataContracts.User> _passwordHasher;
        //SecurityContext _securityContext;
        UserService _userService;
        JwtService _jwtService;
        EmailConfiguration _emailConfig;
        EmailSettings _emailSettings = new EmailSettings();
        SiteSettings _siteSettings = new SiteSettings();
        CountryService _CountryService;
        GradeService _GradeService;      

        public AuthController(CountryService countryService,IPolicy policy, IConfiguration config, SecurityContext securityContext, UserService userService, JwtService jwtService, EmailConfiguration emailConfig, MenuService menuService, AssessmentService assessmentService, StaticPageService staticPageService, IHttpContextAccessor context, HttpClient httpClient )
        : base(menuService, assessmentService, staticPageService, context)

        {
            _config = config;
            _userService = userService;
            //_securityContext = securityContext;
            _userService = userService;
            _httpClient = httpClient;
            _jwtService = jwtService;
            _emailConfig = emailConfig;
            _passwordHasher = new PasswordHasher<IbtecarBusiness.Security.DataContracts.User>();
            config.Bind("EmailSettings", _emailSettings);
            config.Bind("SiteSettings", _siteSettings);
        }


        [HttpPost()]
        public async Task<IActionResult> Login([FromForm] LoginRequest request)
        {
            try
            {
                var user = await _userService.GetAsync(request.Email);
                var requestUrl = "https://api.365exams.com/Security/Auth/GetToken";
                 var requestData = new
                {
                    userName = request.Email,
                    password = request.Password
                };
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(requestUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    UserDto responseContent = await response.Content.ReadFromJsonAsync<UserDto>();

                    if (User.Identity.IsAuthenticated )
                        Logoutvoid();

                    var token = _jwtService.GetToken(responseContent.Email, responseContent.Email, responseContent.Name, responseContent.Id);
                    var ok = SetSession(responseContent.Id.ToString());

                    await SaveClaimsIdentity(user);
                   
                    //for react
                    Imtihani.Helpers.CookieHelper.SetCookie(_context.HttpContext, "Token", responseContent.Token);
                    return Ok(new AuthResponse(token.Token, token.RefreshToken, user.UserType));
                }
                else
                {
                    return BadRequest(new AuthResponse("Wrong Password", new string[] { }));

                }
            }
            catch
            {
                return BadRequest(new AuthResponse("Wrong Password", new string[] { }));
            }
        }


        //public IActionResult GoogleLogIn(string code)
        //{
        //        var client = new RestClient("https://www.googleapis.com/oauth2/v4/token");
        //        var request = new RestRequest(Method.Post.ToString());
        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        //        request.AddParameter("grant_type", "authorization_code");
        //        request.AddParameter("code", code);
        //        request.AddParameter("redirect_uri", "https://localhost:44382/User/RedirectGoogleLogin");
        //        request.AddParameter("client_id", "816419319259-mmh03bgf9d7la53n40dmog3peaccqhnh.apps.googleusercontent.com");
        //        request.AddParameter("client_secret", "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");

        //        RestResponse response = client.Execute(request);
        //        var content = response.Content;
        //        var res = JObject.Parse(content);

        //        if (res.TryGetValue("access_token", out var accessToken))
        //        {
        //            var client2 = new RestClient("https://www.googleapis.com/oauth2/v3/userinfo");
        //            client2.AddDefaultHeader("Authorization", "Bearer " + accessToken);
        //            var request2 = new RestRequest(Method.Get.ToString());

        //            var response2 = client2.Execute(request2);
        //            var content2 = response2.Content;
        //            var userIds = JObject.Parse(content2);

        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            return View("Error");
        //        }
        //}
        //public IActionResult FacebookLogin()
        //{
        //    var appId = "346124578302792";
        //    var redirectUri = "https://localhost:44355/api/Auth/FacebookCallback";
        //    var scope = "public_profile,email";

        //    var fb = new FacebookClient();
        //    var loginUrl = fb.GetLoginUrl(new
        //    {
        //        client_id = appId,
        //        redirect_uri = redirectUri,
        //        scope = scope
        //    });

        //    return Redirect(loginUrl.ToString());
        //}
        //public IActionResult FacebookCallback(string code)
        //{
        //    var appId = "346124578302792";
        //    var appSecret = "ec83d2278052bd0dac7b41ec9be11d55";
        //    var redirectUri = "https://localhost:44355/api/Auth/FacebookCallback";
        //    var fb = new FacebookClient();
        //    dynamic result = fb.Post("oauth/access_token", new
            
        //    {
        //        client_id = appId,
        //        client_secret = appSecret,
        //        redirect_uri = redirectUri,
        //        code = code
        //    });
        //    var accessToken = result.access_token;
        //    dynamic me = fb.Get("/me?fields=name,email&access_token=" + accessToken);
        //    string name = me.name;
        //    string email = me.email;
        //    TempData["name"] = name;
        //    TempData["email"] = email;

        //    return RedirectToAction("LogORRegis", "Auth");
        //}
        public async Task<IActionResult> LogORRegis()
        {
            ViewBag.name = TempData["name"].ToString();
            ViewBag.email = TempData["email"].ToString();
            string email = ViewBag.email;

            var model = new StudentProfileViewModel();
            //string email = User.Identity.GetUserEmail();
            model.User = await _userService.GetAsync(email);
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            string sql = @"SELECT [Email] FROM [365exams].[dbo].[Users]";
            List<ExamSubmissionResult> allUsers = (List<ExamSubmissionResult>)await connection.QueryAsync<ExamSubmissionResult>(sql, commandTimeout: 120, commandType: CommandType.Text);
            ViewBag.acc = allUsers;

            foreach (var item in allUsers)
            {
                if (item.Email == email)
                {
                    LoginRequest userlogin = new LoginRequest();
                    userlogin.Email = ViewBag.email;
                    userlogin.Password = "Aa.12345";
                    await Login(userlogin);
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Register", "Account");
        }

        [HttpPost()]
        public async Task<IActionResult> Register([FromForm] RegisterRequest request)
        {
            Regex EmailRegux = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Regex Passregex = new Regex("^(?=.*?[0-9])(?=.*?[a-z]).{8,}$");

            if (!EmailRegux.IsMatch(request.Email))
                return BadRequest(new AuthResponse("Write a valid Email", new string[] { }));
            if (request.Password != request.RepeatPassword)
                return BadRequest(new AuthResponse("Password and RePassword Aren't The Same", new string[] { }));
            if (!Passregex.IsMatch(request.Password) || !Passregex.IsMatch(request.RepeatPassword))
                return BadRequest(new AuthResponse("Password must have a letters and number With more than 8 digit", new string[] { }));


            var user = await _userService.GetAsync(request.Email);

            if (user != null)
            {
                return BadRequest(new AuthResponse("Email Already Used", new string[] { }));
            }

            var extraInfo = new { Nationality = request.Nationality, FirstName = request.FirstName, LastName = request.LastName, Grade = request.Grade };
            user = new IbtecarBusiness.Security.DataContracts.User()
            {
                Id = Guid.NewGuid(),
                AliasName = request.FirstName + " " + request.LastName,
                Email = request.Email,
                BirthDate = request.DateOfBirth,
                UserType = (int)UserTypes.User,
                ExtraInfo = JsonConvert.SerializeObject(extraInfo),
                SecurityStamp = Guid.NewGuid().ToString(),
                IsDeletable = true,
                IsEnabled = true,
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
            
            await _userService.AddAsync(user);

            var token = _jwtService.GetToken(user.Email, user.Email, user.AliasName, user.Id);

            await SaveClaimsIdentity(user);
            LoginRequest userlogin = new LoginRequest();
            userlogin.Email = user.Email;
            userlogin.Password = request.Password;
            await Login(userlogin);
            return Ok(new AuthResponse(token.Token, token.RefreshToken, user.UserType));
        }

        [HttpPost()]
        public IActionResult RegisterSchool([FromForm] RegisterSchoolRequest request)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();

            try
            {
                StringBuilder mes = new StringBuilder();
                Services.EmailService Eservices = new Services.EmailService(_emailConfig);
                string message = "<section style=\"padding: 2%;\">    <div style=\"text-align:center; padding: 20px;\">     <div><img style=\"width:162px;position: absolute;right:7%;\" src=\"https://365exams.com/img/logo-dark.png\"></div>        <div style=\"color: red;text-shadow: 0 0 black;\">New Request From " + request.Name + " </div>        <!--<a href=\"link\">Click here to access your booking</a>-->    </div>    <center>        <table border=\"1\">            <tr>                <th style=\"border: 1px solid black\" colspan=\"2\"> School Information </th>            </tr>            <tr>                <td>School Name : </td>                <td>" + request.Name + "</td>            </tr>            <tr>                <td>Email : </td>                <td>" + request.Email + "</td>            </tr>            <tr>                <td>City : </td>                <td>" + request.City + "</td>            </tr>            <tr>                <td>Country : </td>                <td>" + request.Country + "</td>            </tr>            <tr>                <td>Phone Number : </td>                <td>" + request.Phone + "</td>            </tr>            <tr>                <td>Level : </td>                <td>" + request.Level + "  </td>            </tr>               </table></section>";
                mes.AppendFormat(message);
                Eservices.SendEmail(mes.ToString(), _emailConfig.To, "School Regestration");
                response.Add("Success", 1);
                response.Add("Message", "Request Sent Successfully");
            }
            catch (Exception ex)
            {
                response.Add("Success", 0);
                response.Add("Message", "Something Went Wrong, Please Try Again Later.");
                Ok(new BaseResponse("Failed To Send Request" + ex.Message.ToString()));


            }


            //await _emailService.SendEmail()
            return Ok(new BaseResponse() { Success = true, Message = "Request Sent Successfully" });
        }

        [HttpPost()]
        public IActionResult ContactUs([FromForm] ContactUsRequest request)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();

            try
            {
                StringBuilder mes = new StringBuilder();
                Services.EmailService Eservices = new Services.EmailService(_emailConfig);
                mes.AppendFormat("<div style=\"background-image:initial;background-position:initial;background-size:initial;background-repeat:initial;background-origin:initial;background-clip:initial;padding:3%;text-align:center\"> <div style=\"font-weight:500\"> <p dir=\"ltr\" class=\"MsoNormal\">  <img src=\"https://365exams.com/img/logo-dark.png\" height=\"120\" style=\"text-align:start;background-color:rgb(251,251,251)\" class=\"CToWUd a6T\" data-bit=\"iit\" tabindex=\"0\"> <div class=\"a6S\" dir=\"ltr\" style=\"opacity: 0.01; left: 402.578px; top: 125.109px;\"><div id=\":pu\" class=\"T-I J-J5-Ji aQv T-I-ax7 L3 T-I-KL a5q\" role=\"button\" tabindex=\"0\" aria-label=\"تحميل المرفق \" data-tooltip-class=\"a1V\" data-tooltip=\"تنزيل\"><div class=\"akn\"><div class=\"aSK J-J5-Ji aYr\"></div></div></div></div></span></p> <p dir=\"ltr\" class=\"MsoNormal\"><span style=\"font-family:Roboto;font-size:large\">Contact Us </span><img alt=\"\" style=\"font-family:Roboto;font-size:large\"></p> <p dir=\"ltr\" style=\"color: red;\" class=\"MsoNormal\">New Message From " + request.FullName.ToUpper() + "<span lang=\"AR-JO\"><font face=\"Roboto\" size=\"4\"><br><br><br></font></span></p> <div align=\"center\"> <center> <table dir=\"ltr\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\"> <tbody> <tr> <td width=\"197\" valign=\"top\"> <br> <p class=\"MsoNormal\" align=\"center\" dir=\"ltr\"><span lang=\"EN-US\" dir=\"LTR\" style=\"font-size:12.0pt\">Full Name</span><span dir=\"ltr\"></span><span dir=\"ltr\"></span><span lang=\"EN-US\"><span dir=\"ltr\"></span><span dir=\"ltr\"></span> </span><span lang=\"AR-JO\"></span></p> <br> </td> <td width=\"245\" valign=\"top\"> <br> <p class=\"MsoNormal\" align=\"center\" dir=\"ltr\"><span lang=\"AR-JO\">&nbsp;</span>  " + request.FullName + " </p> <br> </td> </tr> <tr> <td width=\"197\" valign=\"top\"> <br> <p class=\"MsoNormal\" align=\"center\" dir=\"ltr\"><span lang=\"EN-US\" dir=\"LTR\" style=\"font-size:12pt\">Email</span><span lang=\"AR-JO\"></span></p> <br> </td> <td width=\"245\" valign=\"top\"> <br> <p class=\"MsoNormal\" align=\"center\" dir=\"ltr\"><span lang=\"EN-US\" dir=\"LTR\" style=\"font-size:12pt\"> " + request.Email + "</span><span lang=\"AR-JO\"></span></p> <br> </td> </tr> <tr> <td width=\"197\" valign=\"top\"> <br> <p class=\"MsoNormal\" align=\"center\" dir=\"ltr\"><span lang=\"EN-US\" dir=\"LTR\" style=\"font-size:12.0pt\">Phone</span><span dir=\"ltr\"></span><span dir=\"ltr\"></span><span lang=\"EN-US\"><span dir=\"ltr\"></span><span dir=\"ltr\"></span> </span><span lang=\"AR-JO\"></span></p> <br> </td> <td width=\"245\" valign=\"top\"> <br> <p class=\"MsoNormal\" align=\"center\" dir=\"ltr\"><span lang=\"EN-US\" dir=\"LTR\" style=\"font-size:12.0pt\"> " + request.Phone + " </span><span dir=\"ltr\"></span><span dir=\"ltr\"></span><span lang=\"EN-US\"><span dir=\"ltr\"></span><span dir=\"ltr\"></span> </span><span lang=\"AR-JO\"></span></p> <br> </td> </tr> <tr> <td width=\"197\" valign=\"top\"> <br> <p class=\"MsoNormal\" align=\"center\" dir=\"ltr\"><span lang=\"EN-US\" dir=\"LTR\" style=\"font-size:12pt\">Description</span><span lang=\"AR-JO\"></span></p> <br> </td> <td width=\"245\" valign=\"top\"> <br> <p class=\"MsoNormal\" align=\"center\" dir=\"ltr\"><span lang=\"EN-US\" dir=\"LTR\" style=\"font-size:12pt\"> " + request.Description + " </span><span lang=\"AR-JO\"></span></p> <br> </td> </tr> </tbody> </table> </center> </div> <div align=\"right\"> <div align=\"right\"><br></div> </div> </div> </div>");
                Eservices.SendEmail(mes.ToString(), _emailConfig.To, "Contact Us");
                response.Add("Success", 1);
                response.Add("Message", "Message Sent Successfully");
            }
            catch (Exception ex)
            {
                response.Add("Success", 0);
                response.Add("Message", "Something Went Wrong, Please Try Again Later.");
                Ok(new BaseResponse("Failed To Send Request"));


            }

            return Ok(new BaseResponse() { Success = true, Message = "Request Sent Successfully" });
        }

        public async Task<IActionResult> EditProfile([FromForm] UserProfilInfo request)
        {
            var model = await _userService.GetAsync(request.Email);
            if (!string.IsNullOrEmpty(model.ExtraInfo))
            {
                try
                {
                    UserExtraInfo info = JsonConvert.DeserializeObject<UserExtraInfo>(model.ExtraInfo);
                    info.FirstName = request.FirstName;
                    info.LastName = request.LastName;
                    model.ExtraInfo = JsonConvert.SerializeObject(info);
                }
                catch 
                {
                    UserExtraInfo info = new UserExtraInfo();
                    info.FirstName = request.FirstName;
                    info.LastName = request.LastName;
                    model.ExtraInfo = JsonConvert.SerializeObject(info);
                }
            }
            model.AliasName = $"{request.FirstName} {request.LastName}";
            object value = await _userService.EditAsync(model);
            return RedirectToAction("StudentProfile", "Home");
        }


        [HttpPost()]
        public IActionResult RegisterTeacher([FromForm] RegisterTeacherRequest request)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();

            try
            {
                StringBuilder mes = new StringBuilder();
                Services.EmailService Eservices = new Services.EmailService(_emailConfig);
                string message = "<section style=\"padding: 2%;\">    <div style=\"text-align:center; padding: 20px;\">     <div><img style=\"width:162px;position: absolute;right:7%;\" src=\"https://365exams.com/img/logo-dark.png\"></div>        <div style=\"color: red;text-shadow: 0 0 black;\">New Request From " + request.FullName + " </div>        <!--<a href=\"link\">Click here to access your booking</a>-->    </div>    <center>        <table border=\"1\">            <tr>                <th style=\"border: 1px solid black\" colspan=\"2\"> Teacher Information </th>            </tr>            <tr>                <td>Teacher Name : </td>                <td>" + request.FullName + "</td>            </tr>            <tr>                <td>Nationality : </td>                <td>" + request.Nationality + "</td>            </tr>            <tr>                <td>Country Of Residence : </td>                <td>" + request.Country + "</td>            </tr>            <tr>                <td>Address : </td>                <td>" + request.Address + "</td>            </tr>            <tr>                <td>Phone Number : </td>                <td>" + request.PhoneNumber + "</td>            </tr>            <tr>                <td>Phone Number 2 : </td>                <td>" + request.PhoneNumber2 + "  </td>            </tr>            <tr>                <td>IsWorking : </td>                <td>" + request.IsWorking + " </td>            </tr>            <tr>                <td>Experience : </td>                <td>" + request.Experience + " </td>            </tr>            <tr>                <td>Years Of Experience : </td>                <td>" + request.YearsOfExperience + " </td>            </tr>      </table></section>";
                mes.AppendFormat(message);
                Eservices.SendEmail(mes.ToString(), _emailConfig.To, "Teacher Regestration");
                response.Add("Success", 1);
                response.Add("Message", "Request Sent Successfully");
            }
            catch (Exception ex)
            {
                response.Add("Success", 0);
                response.Add("Message", "Something Went Wrong, Please Try Again Later.");
                Ok(new BaseResponse("Failed To Send Request" + ex.Message.ToString()));


            }


            //await _emailService.SendEmail()
            return Ok(new BaseResponse() { Success = true, Message = "Request Sent Successfully" });
        }
        public IActionResult SetSession(string id)
        {
            try
            {
                _context.HttpContext.Session.SetString("UserID", id);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        public string GetSession(string key)
        {
            try
            {
                var value = _context.HttpContext.Session.GetString(key);
                return value;
            }
            catch (Exception ex)
            {
                return (ex.ToString());
            }
        }
        private async Task SaveClaimsIdentity(IbtecarBusiness.Security.DataContracts.User user)
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Email));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.AliasName));
            identity.AddClaim(new Claim("UserType", user.UserType.ToString()));
            var principal = new ClaimsPrincipal(identity);
            await _context.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(1)
                });
        }
        public void Logoutvoid()
        {
            if (Request.Cookies["UserID"] != null)
            {

                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddMonths(-1);
                _context.HttpContext.Response.Cookies.Delete("UserID", options);

            }
            _context.HttpContext.Session.Remove("UserID");

            _context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        }
        public async Task<ActionResult> Logout()
        {
            if (Request.Cookies["UserID"] != null)
            {
                //HttpContext.Response.Cookies["UserID"].Value = null;
                //HttpContext.Response.Cookies["UserID"].Expires = DateTime.Now.AddMonths(-1);
                //CookieHelper.SetCookie(_context.HttpContext, "UserID", String.Empty, DateTime.Now.AddMonths(-1));

                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddMonths(-1);
                _context.HttpContext.Response.Cookies.Delete("UserID", options);

            }
            _context.HttpContext.Session.Remove("UserID");

            await _context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("~/Account/Logout");
        }
        public async Task RemoveClaimsIdentity()
        {
            await _context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }


     
    }

    public class UserExtraInfo
    {
        public string Nationality { get; set; } = "Jordan";
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Grade { get; set; }
    }
}
