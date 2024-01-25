using Dapper;
using IbtecarBusiness.Core.Services;
using IbtecarBusiness.Courses.DataContracts;
using IbtecarBusiness.Courses.Services;
using IbtecarFramework;
using Imtihani.Helpers;
using Imtihani.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Imtihani.Controllers
{
    public class AccountController : BaseController
    {

        public AccountController(CountryService countryService, GradeService gradeService, MenuService menuService, AssessmentService assessmentService, StaticPageService staticPageService, IHttpContextAccessor context)
        : base(menuService, assessmentService, staticPageService, context)
        {
            _CountryService = countryService;
            _GradeService = gradeService;
        }

        private CountryService _CountryService;
        private GradeService _GradeService;

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {

            TempData["ReturnUrl"] = returnUrl;
            return View();
        }


        public async Task<IActionResult> Register(string returnUrl = "")
        {
            TempData["ReturnUrl"] = returnUrl;
            if (TempData["name"] != null)
            {
                ViewBag.name = TempData["name"].ToString();
                string[] nameParts = ViewBag.name.Split(' ');
                if (nameParts.Length == 2)
                {
                    string firstName = nameParts[0];
                    string lastName = nameParts[1];
                    ViewBag.FirstName = firstName;
                    ViewBag.LastName = lastName;
                }
            }
            if (TempData["email"] != null)
            {
                ViewBag.email = TempData["email"].ToString();
                
            }
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Auth", new { email = TempData["email"], password = "Aa.12345" });
            }

            ViewBag.Countries = (await _CountryService.ListAsync(new PagingInfo(200, 1), null)).ToSelectList("Id", "LocalizedName");
            var grades = await _GradeService.ListAsync(null, new PagingInfo(200, 1), null);
            var categories = grades.Select(s => s.CategoryId).Distinct().ToList();
            var grades2 = new List<Grade>();
            for (int i = 0; i < categories.Count; i++)
            {
                grades2.AddRange(grades.Where(s => s.CategoryId == categories[i]).OrderBy(s => s.Rank).ToList());
            }
            ViewBag.Grades = grades2.ToSelectList("Id", "LocalizedName");
            return View();
        }

        public IActionResult JoinUsAsync()
        {
            var model = new JoinUsViewModel();
            //model.StaticPage = await _staticPageService.GetByUniqueNameAsync("/JoinUs");
            return View(model);
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

            return Redirect("~/Account/Login");
        }
    }
}
