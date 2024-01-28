using IbtecarBusiness.Core.DataContracts;
using IbtecarBusiness.Core.Services;
using IbtecarBusiness.Courses.Services;
using IbtecarFramework;
using Imtihani.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading;

namespace Imtihani.Controllers
{
    public class BaseController : Controller
    {
        protected MenuService _menuService;
        protected AssessmentService _assessmentService;
        protected StaticPageService _staticPageService;
        protected IHttpContextAccessor _context;


        public BaseController(MenuService menuService, AssessmentService assessmentService, StaticPageService staticPageService, IHttpContextAccessor context)
        {
            _menuService = menuService;
            _assessmentService = assessmentService;
            _staticPageService = staticPageService;
            _context = context;

        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string cultureName = ControllerContext.HttpContext.GetCookie("_culture");// ..get.HttpContext.Response.Cookies.get
            //string cultureName = this.HttpContext.GetCookie("_culture");
            if (string.IsNullOrEmpty(cultureName))
            {
                cultureName = CultureHelper.GetDefaultCulture();
                SetCulture(cultureName);
            }
            //Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName);
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            if (!IsAjax())
            {
                var menu = _menuService.GetFullAsync(2).Result;
                var About = _staticPageService.GetByUniqueNameAsync("/About").Result;
                var Contact = _staticPageService.GetByUniqueNameAsync("/Contact").Result;
                var Privacy = _staticPageService.GetByUniqueNameAsync("/privacy").Result;
                var FB = _staticPageService.GetByUniqueNameAsync("/FB").Result;
                var insta = _staticPageService.GetByUniqueNameAsync("/insta").Result;
                var snap = _staticPageService.GetByUniqueNameAsync("/snap").Result;
                var youtube = _staticPageService.GetByUniqueNameAsync("/youtube").Result;
                var twitter = _staticPageService.GetByUniqueNameAsync("/twitter").Result;





                menu.Items = menu.Items.BuildTree<MenuItem, int>();
                if (User.Identity.IsAuthenticated && User.Identity.IsTeacher())
                {
                    menu.Items.AddRange(_menuService.GetFullAsync(7).Result.Items);
                }
                else if (User.Identity.IsAuthenticated && User.Identity.IsStudent())
                {
                    menu.Items.AddRange(_menuService.GetFullAsync(6).Result.Items);
                }
                else
                {
                    menu.Items.AddRange(_menuService.GetFullAsync(5).Result.Items);
                }
                ViewBag.TopMenu = menu;
                ViewBag.About = About;
                ViewBag.Contact = Contact;
                ViewBag.Privacy = Privacy;
                ViewBag.FB = FB;
                ViewBag.insta = insta;
                ViewBag.snap = snap;
                ViewBag.youtube = youtube;
                ViewBag.twitter = twitter;



                ViewBag.PopularAssessments = _assessmentService.ListAsync(null, null, null, null, null, null, true, new IbtecarFramework.PagingInfo(3), null).Result;
            }

            base.OnActionExecuting(context);
        }

        public virtual ActionResult ChangeCulture()
        {
            string culture = CultureHelper.GetCurrentCulture();
            if (culture.ToLower().Contains("en"))
                return SetCulture("ar-JO");
            else
                return SetCulture("en-US");
        }

        public virtual ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            // Save culture in a cookie
            HttpContext.SetCookie("_culture", culture);
            string returnUrl = Url.Content("~/");
            string referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer))
                returnUrl = referer;
            return Redirect(returnUrl);
        }

        protected virtual bool IsAjax()
        {
            return HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
    }
}
