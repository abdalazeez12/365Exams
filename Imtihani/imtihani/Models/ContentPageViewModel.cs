using IbtecarBusiness.Articles.DataContracts;
using IbtecarBusiness.Core.DataContracts;
using IbtecarBusiness.Courses.DataContracts;
using IbtecarBusiness.Security.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User = IbtecarBusiness.Security.DataContracts.User;

namespace Imtihani.Models
{
    public class ContentPageViewModel
    {
        public ContentPageViewModel()
        {
            HeaderImage = "~/img/7.jpg";
        }

        public ContentPageViewModel(StaticPage page)
        {
            StaticPage = page;
        }

        public StaticPage StaticPage { get; set; }
        public string HeaderImage { get; set; }
    }

    public class TeachersPageViewModel : ContentPageViewModel
    {
        public TeachersPageViewModel()
        {
            HeaderImage = "~/img/11.jpg";
        }

        public TeachersPageViewModel(StaticPage page)
        {
            StaticPage = page;
        }

        public List<Instructor> Teachers { get; set; } = new List<Instructor>();
        public Instructor Teacher { get; set; }
        public List<Assessment> Assessments { get; set; }
        public int? AssessmentCount { get; set; }
    }

    public class StudentProfileViewModel : ContentPageViewModel
    {
        public StudentProfileViewModel()
        {
            HeaderImage = "~/Images/12.jpg";
        }

        public StudentProfileViewModel(StaticPage page)
        {
            StaticPage = page;
        }
        public Student Student { get; set; }
        public User User { get; set; }
        public ExtraInfos ExtraInfo { get; set; }
}

    public class JoinUsViewModel : ContentPageViewModel
    {
        public JoinUsViewModel()
        {
            HeaderImage = "~/img/7.jpg";
        }

        public JoinUsViewModel(StaticPage page)
        {
            StaticPage = page;
        }
    }
       public class NewsPageViewModel : ContentPageViewModel
    {
        public NewsPageViewModel()
        {
            HeaderImage = "~/img/11.jpg";
        }

        public NewsPageViewModel(StaticPage page)
        {
            StaticPage = page;
        }

        public List<News> AllNews { get; set; } = new List<News>();
        public News News { get; set; }
     }
}
