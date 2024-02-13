using Dapper;
using IbtecarBusiness.Articles.Services;
using IbtecarBusiness.Core.DataContracts;
using IbtecarBusiness.Core.Services;
using IbtecarBusiness.Courses.DataContracts;
using IbtecarBusiness.Courses.Models;
using IbtecarBusiness.Courses.Services;
using IbtecarBusiness.Security.Services;
using IbtecarFramework;
using IbtecarModules.Jwt.Models;
using Imtihani.Helpers;
using Imtihani.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Imtihani.Controllers
{


    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        InstructorService _instructorService;
        SubjectService _subjectService;
        PhotoService _photoService;
        PartnerService _partnerService;
        CategoryService _categoryService;
        GradeService _gradeService;
        StudentService _studentService;
        UserService _userService;
        NewsService _newsService;
        EmailSettings _emailSettings = new EmailSettings();
        SiteSettings _siteSettings = new SiteSettings();

        private CountryService _CountryService;

        private IStringLocalizer<SharedResource> _sharedLocalizer;

        public HomeController(ILogger<HomeController> logger, IConfiguration config, PhotoService photoService, PartnerService partnerService, CategoryService categoryService, GradeService gradeService, AssessmentService assessmentService, InstructorService instructorService, SubjectService subjectService, MenuService menuService, StudentService studentService, UserService userService, NewsService newsService, IStringLocalizer<SharedResource> sharedLocalizer, StaticPageService staticPageService, IHttpContextAccessor context)
            : base(menuService, assessmentService, staticPageService, context)
        {

            _logger = logger;
            _config = config;
            _photoService = photoService;
            _partnerService = partnerService;
            _categoryService = categoryService;
            _gradeService = gradeService;
            _assessmentService = assessmentService;
            _studentService = studentService;
            _userService = userService;
            _newsService = newsService;
            _instructorService = instructorService;
            _subjectService = subjectService;
            _menuService = menuService;
            _sharedLocalizer = sharedLocalizer;
            config.Bind("EmailSettings", _emailSettings);
            config.Bind("SiteSettings", _siteSettings);
        }

        public IActionResult ComingSoon()
        {
            var guid = Guid.NewGuid();
            var model = new ContentPageViewModel();
            return View(model);
        }

        public string Lang()
        {
            string culture = CultureHelper.GetCurrentCulture();
            string[] Culture = culture.Split("-");
            return Culture[0];
        }

        public async Task<IActionResult> Coming()
        {
            var model = new ContentPageViewModel();
            model.StaticPage = await _staticPageService.GetByUniqueNameAsync("/coming");
            return View(model);
        }

        public async Task<IActionResult> Index()
        {
            //var qusai = _sharedLocalizer["qusai"];

            var model = new HomeIndexViewModel();
            model.StaticPage = await _staticPageService.GetByUniqueNameAsync("/index");
            model.Countries = (await _categoryService.ListAsync("Country", "Assessment", -7, new IbtecarFramework.PagingInfo(10), null)).OrderBy(s => s.Rank).ToList();
            model.TopSlider = await _photoService.ListAsync(-5, null, new IbtecarFramework.PagingInfo(10), null);
            model.Services = await _partnerService.ListAsync(null, "Services", new IbtecarFramework.PagingInfo(10), null);
            model.Numbers = await _partnerService.ListAsync(null, "Numbers", new IbtecarFramework.PagingInfo(4), null);
            model.Grades = (await _gradeService.ListAsync(null, new IbtecarFramework.PagingInfo(200), null)).OrderBy(s => s.Rank).ToList();
            model.Subjects = await _subjectService.ListAsync(new IbtecarFramework.PagingInfo(200), null);
            model.GradesDictionary = new Dictionary<int?, List<IbtecarBusiness.Courses.DataContracts.Grade>>();
            model.PopularAssessments = await _assessmentService.ListAsync(null, null, null, null, null, null, true, new IbtecarFramework.PagingInfo(3), null);
            model.News = await _newsService.ListAsync(Lang(), null, 1, null, true, new IbtecarFramework.PagingInfo(3), null);
            model.Events = await _newsService.ListAsync(Lang(), null, 2, null, true, new IbtecarFramework.PagingInfo(3), null);

            foreach (var category in model.Countries)
            {
                model.GradesDictionary.Add(category.Id, model.Grades.Where(s => s.CategoryId == category.Id).ToList());
            }
            return View(model);
        }

        public async Task<IActionResult> About()
        {
            var model = new ContentPageViewModel();
            model.StaticPage = await _staticPageService.GetByUniqueNameAsync("/About");
            ViewBag.Title = model.StaticPage.LocalizedName;
            return View(model);
        }

        public async Task<IActionResult> Team()
        {
            var model = new ContentPageViewModel();
            model.StaticPage = await _staticPageService.GetByUniqueNameAsync("/Team");
            return View(model);
        }
        public async Task<IActionResult> Contact()
        {
            var model = new ContactPageViewModel();
            model.StaticPage = await _staticPageService.GetByUniqueNameAsync("/Contact");
            return View(model);
        }


        public async Task<IActionResult> Services()
        {
            var model = new ContentPageViewModel();
            model.StaticPage = await _staticPageService.GetByUniqueNameAsync("/Services");
            return View(model);
        }

        public async Task<IActionResult> Courses(int id)
        {
            try
            {
                var grade = await _gradeService.GetAsync(id);
                var model = new CoursesPageViewModel();
                model.StaticPage = await _staticPageService.GetByUniqueNameAsync("/Courses");
                model.Grades = await _gradeService.ListAsync(null, new IbtecarFramework.PagingInfo(200), null);
                //model.Assessments = await _assessmentService.ListAsync(grade.Id, null, null, null, null, null, new IbtecarFramework.PagingInfo(200), null);
                //model.Assessments.Where(s => s.GradeId == grade.Id).ToList();
                //model.ActiveGrade = grade.Id;
                return View(model);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> CheckOut()
        {
            var model = new ContactPageViewModel();
            model.StaticPage = await _staticPageService.GetByUniqueNameAsync("/Checkout");
            return View(model);
        }

        public async Task<IActionResult> Assessment()
        {
            var model = new ContentPageViewModel();
            model.StaticPage = await _staticPageService.GetByUniqueNameAsync("/Assessment");
            return View(model);
        }

        public async Task<IActionResult> Privacy()
        {
            var model = new ContentPageViewModel();
            model.StaticPage = await _staticPageService.GetByUniqueNameAsync("/Privacy");
            return View(model);
        }

        public async Task<IActionResult> Purchasing()
        {
            var model = new ContentPageViewModel();
            model.StaticPage = await _staticPageService.GetByUniqueNameAsync("/Purchasing");
            return View(model);
        }

        public async Task<IActionResult> FAQ()
        {
            var model = new ContentPageViewModel();
            model.StaticPage = await _staticPageService.GetByUniqueNameAsync("/FAQ");
            return View(model);
        }


        public async Task<IActionResult> OrderSummery()
        {
            var model = new ContentPageViewModel();
            model.StaticPage = await _staticPageService.GetByUniqueNameAsync("/OrderSummery");
            return View(model);
        }

        public async Task<IActionResult> Teachers(string searchText = null)
        {
            var model = new TeachersPageViewModel();
            model.StaticPage = await _staticPageService.GetByUniqueNameAsync("/Teachers");
            model.Teachers = await _instructorService.ListAsync(new PagingInfo(100, 1), searchText);
            return View(model);
        }

        public async Task<IActionResult> Teacher(string id)
        {
            var model = new TeachersPageViewModel();
            model.StaticPage = await _staticPageService.GetByUniqueNameAsync("/Teacher");
            model.Teacher = await _instructorService.GetAsync(id);
            var paging = new PagingInfo(10, 1);
            model.Assessments = await _assessmentService.ListAsync(null, null, null, model.Teacher.Id, null, null, true, paging, null);
            model.AssessmentCount = paging.TotalItemCount;
            return View(model);
        }

        public async Task<IActionResult> Terms()
        {
            var model = new ContentPageViewModel();
            model.StaticPage = await _staticPageService.GetByUniqueNameAsync("/Terms");
            return View(model);
        }

        public async Task<IActionResult> News()
        {
            var model = new HomeIndexViewModel();
            model.StaticPage = await _staticPageService.GetByUniqueNameAsync("/News");
            TempData["StaticPage"] = JsonConvert.SerializeObject(model.StaticPage);

            model.News = await _newsService.ListAsync(Lang(), null, 1, null, true, new IbtecarFramework.PagingInfo(3), null);

            return View(model);
        }
        public async Task<IActionResult> Events()
        {
            var model = new HomeIndexViewModel();
            model.StaticPage = await _staticPageService.GetByUniqueNameAsync("/Events");
            TempData["StaticPage"] = JsonConvert.SerializeObject(model.StaticPage);

            model.Events = await _newsService.ListAsync(Lang(), null, 2, null, true, new IbtecarFramework.PagingInfo(3), null);

            return View(model);
        }
        public async Task<IActionResult> SingleNews(Guid id)
        {
            var model = new HomeIndexViewModel();

            object o;
            TempData.TryGetValue("StaticPage", out o);
            IbtecarBusiness.Core.DataContracts.StaticPage StaticPage = o == null ? null : JsonConvert.DeserializeObject<IbtecarBusiness.Core.DataContracts.StaticPage>((string)o);
            model.StaticPage = StaticPage;
            TempData.Keep();
            model.SingleNews = await _newsService.GetAsync(id);
            return View(model);
        }
        ////[Authorize]
        public async Task<IActionResult> StudentProfile()
        {
            var model = new StudentProfileViewModel();
            string email = User.Identity.GetUserEmail();
            model.User = await _userService.GetAsync(email);

            model.StaticPage = await _staticPageService.GetByUniqueNameAsync("/StudentProfile");
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            string sql = @$"
WITH RankedExamSubmission AS (
    SELECT 
        ES.[Id],
        ES.[UserId],
        A.[Name],
        A.[NameAr],
        A.[Price],
        A.[Duration],
        ES.[AssessmentId],
        ES.[SubmissionDate],
        ES.[Answers],
        ES.[IsCorrected],
        ES.[QuestionCount],
        ES.[CorrectCount],
        ES.[CertificatePath],
        G.[Name] AS GradeName,
        I.[Name] AS InstructorName,
        ROW_NUMBER() OVER (PARTITION BY ES.[AssessmentId] ORDER BY ES.[Id]) AS RowNum
    FROM
        [Users] U
    LEFT JOIN
        [ExamSubmission] ES ON (U.[Id] = ES.[UserId])
    LEFT JOIN
        [Assessment] A ON (ES.[AssessmentId] = A.Id)
    LEFT JOIN
        [Grade] G ON (G.Id = A.GradeId)
    LEFT JOIN
        [Instructor] I ON (I.[UserId] = U.[Id])
    WHERE
        ES.[UserId] ='{model.User.Id}' and A.[Price] is NOT NULL
)
SELECT
    [Id],
    [UserId],
    [Name],
    [NameAr],
    [Price],
    [Duration],
    [AssessmentId],
    [SubmissionDate],
    [Answers],
    [IsCorrected],
    [QuestionCount],
    [CorrectCount],
    [CertificatePath],
    [GradeName],
    [InstructorName]
FROM
    RankedExamSubmission
WHERE
    RowNum = 1;";
            DynamicParameters parameters = new();
            parameters.Add("UserId", model.User.Id);

            List<ExamSubmissionResult> acc = (List<ExamSubmissionResult>)await connection.QueryAsync<ExamSubmissionResult>(sql, parameters, commandTimeout: 120, commandType: CommandType.Text);
            ViewBag.acc = acc;

            string sql2 = @$"
            WITH RankedExamSubmission AS (
                SELECT 
                    ES.[Id],
                    ES.[UserId],
                    A.[Name],
                    A.[NameAr],
                    A.[Price],
                    A.[Duration],
                    ES.[AssessmentId],
                    ES.[SubmissionDate],
                    ES.[Answers],
                    ES.[IsCorrected],
                    ES.[QuestionCount],
                    ES.[CorrectCount],
                    ES.[CertificatePath],
                    G.[Name] AS GradeName,
                    I.[Name] AS InstructorName,
                    U.[AliasName],
                    ROW_NUMBER() OVER (PARTITION BY ES.[AssessmentId] ORDER BY ES.[Id]) AS RowNum
                FROM
                    [Users] U
                LEFT JOIN
                    [ExamSubmission] ES ON (U.[Id] = ES.[UserId])
                LEFT JOIN
                    [Assessment] A ON (ES.[AssessmentId] = A.Id)
                LEFT JOIN
                    [Grade] G ON (G.Id = A.GradeId)
                LEFT JOIN
                    [Instructor] I ON (I.[UserId] = U.[Id])
                WHERE
                    ES.[UserId] ='{model.User.Id}'
            )
            SELECT
                [Id],
                [UserId],
                [Name],
                [NameAr],
                [Price],
                [Duration],
                [AssessmentId],
                [SubmissionDate],
                [Answers],
                [IsCorrected],
                [QuestionCount],
                [CorrectCount],
                [CertificatePath],
                [GradeName],
                [InstructorName],
                [Price]
            FROM
                RankedExamSubmission
            WHERE
                RowNum = 1;";
            DynamicParameters parameterss = new();
            parameters.Add("UserId", model.User.Id);

            List<ExamSubmissionResult> acce = (List<ExamSubmissionResult>)await connection.QueryAsync<ExamSubmissionResult>(sql2, parameterss, commandTimeout: 120, commandType: CommandType.Text);
            ViewBag.acce = acce;


            var model2 = new IbtecarBusiness.Courses.DataContracts.Grade();
            var grades = await _gradeService.ListAsync(null, new PagingInfo(200, 1), null);
            var grades2 = new List<IbtecarBusiness.Courses.DataContracts.Grade>();
            ViewBag.Grades2 = grades2.ToSelectList("Id", "LocalizedName");



            return View(model);

        }
        public async Task<IActionResult> Programs(string gradename = null, string subjectname = null, int gradeId = 0, int subjectId = 0/*, int? systemId, int? gradeId, int? subjectId, int? minPrice, int? maxPrice, int page = 1*/)
        {
            var model = new ProgramsViewModel();
            model.StaticPage = await _staticPageService.GetByUniqueNameAsync("/Programs");
            model.Grades = await _gradeService.ListAsync(null, new IbtecarFramework.PagingInfo(200), gradename);
            model.Countries = _categoryService.ListAsync("Country", "Assessment", -7, new IbtecarFramework.PagingInfo(10), null).Result.OrderBy(s => s.Rank).ToList();
            model.Subjects = _subjectService.ListAsync(new IbtecarFramework.PagingInfo(10), subjectname).Result.ToList();
            var ranges = new List<PriceRange>();
            ranges.Add(new PriceRange() { Min = 0, Max = 0, Name = "Free", NameAr = "مجاني" });
            ranges.Add(new PriceRange() { Min = 1, Max = 5, Name = "1-5JD", NameAr = "1-5JD" });
            ranges.Add(new PriceRange() { Min = 6, Max = 20, Name = "6-20JD", NameAr = "6-20JD" });
            ranges.Add(new PriceRange() { Min = 21, Max = 2000, Name = "21JD+", NameAr = "21JD+" });
            model.PriceRanges = ranges;
            model.Counts = await _assessmentService.GetAssessmentsCounts();
            model.Assessments = await _assessmentService.ListAsync(null, null, subjectId, null, null, null, true, new IbtecarFramework.PagingInfo(200), subjectname);

            var assessments = await _assessmentService.ListAsync(null, null, null, null, null, null, true,new IbtecarFramework.PagingInfo(200), null);
            ViewBag.assessments = assessments;


            //string UserID = Imtihani.Helpers.CookieHelper.GetCookie(HttpContext, "UserID");
            //var result = SignInStatus.Failure;
            string UserID = string.Empty;
            if (User.Identity.IsAuthenticated)
            {
                UserID = GetSession("UserID");
            }
            bool islogin = false;
            if (!String.IsNullOrEmpty(UserID))
            {
                islogin = true;
            }
            if (gradeId != 0)
            {
                var grade = await _gradeService.GetAsync(gradeId);
            }
            if (subjectId != 0)
            {
                var subject = await _subjectService.GetAsync(subjectId);
            }
            //var paging = new PagingInfo(12, page);
            //model.Assessments = await _assessmentService.ListAsync(systemId, gradeId, subjectId, null, minPrice, maxPrice, paging, null);
            //model.Paging = paging;
            ViewBag.islogin = islogin;
            ViewBag.gradeid = gradeId;
            ViewBag.subjectId = subjectId;


            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.ApiUrl = _siteSettings.ApiUrl;
            base.OnActionExecuting(context);
        }
        public async Task<IActionResult> AssessmentDetails()
        {
            var model = new CoursesPageViewModel();

            model.Assessments = await _assessmentService.ListAsync(null, null, null, null, null, null, true, new IbtecarFramework.PagingInfo(3), null);

            model.Assessments = model.Assessments.Where(assessment => assessment.GradeId > 80).ToList();

            model.ActiveGrade = model.Assessments.Count;

            return View(model);
        }
        public async Task<IActionResult> MyCourses()
        {
            var model = new StudentProfileViewModel();
            string email = User.Identity.GetUserEmail();
            model.User = await _userService.GetAsync(email);
            model.StaticPage = await _staticPageService.GetByUniqueNameAsync("/StudentProfile");
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            string sql = @$"
            WITH RankedExamSubmission AS (
                SELECT 
                    ES.[Id],
                    ES.[UserId],
                    A.[Name],
                    A.[NameAr],
                    A.[Price],
                    A.[Duration],
                    ES.[AssessmentId],
                    ES.[SubmissionDate],
                    ES.[Answers],
                    ES.[IsCorrected],
                    ES.[QuestionCount],
                    ES.[CorrectCount],
                    ES.[CertificatePath],
                    G.[Name] AS GradeName,
                    I.[Name] AS InstructorName,
                    U.[AliasName],
                    ROW_NUMBER() OVER (PARTITION BY ES.[AssessmentId] ORDER BY ES.[Id]) AS RowNum
                FROM
                    [Users] U
                LEFT JOIN
                    [ExamSubmission] ES ON (U.[Id] = ES.[UserId])
                LEFT JOIN
                    [Assessment] A ON (ES.[AssessmentId] = A.Id)
                LEFT JOIN
                    [Grade] G ON (G.Id = A.GradeId)
                LEFT JOIN
                    [Instructor] I ON (I.[UserId] = U.[Id])
                WHERE
                    ES.[UserId] ='{model.User.Id}'
            )
            SELECT
                [Id],
                [UserId],
                [Name],
                [NameAr],
                [Price],
                [Duration],
                [AssessmentId],
                [SubmissionDate],
                [Answers],
                [IsCorrected],
                [QuestionCount],
                [CorrectCount],
                [CertificatePath],
                [GradeName],
                [InstructorName],
                [Price]
            FROM
                RankedExamSubmission
            WHERE
                RowNum = 1;";
            DynamicParameters parameters = new();
            parameters.Add("UserId", model.User.Id);
            List<ExamSubmissionResult> acc = (List<ExamSubmissionResult>)await connection.QueryAsync<ExamSubmissionResult>(sql, parameters, commandTimeout: 120, commandType: CommandType.Text);
            ViewBag.acc = acc;
            return View(model);
        }
        public async Task<IActionResult> GoToAnalysis()
        {
            return View();
        }
    }
}
