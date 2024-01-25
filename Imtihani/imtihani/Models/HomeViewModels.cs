using IbtecarBusiness.Articles.DataContracts;
using IbtecarBusiness.Core.DataContracts;
using IbtecarBusiness.Courses.DataContracts;
using IbtecarFramework;
using System;
using System.Collections.Generic;

namespace Imtihani.Models
{
    public class HomeIndexViewModel : ContentPageViewModel
    {
        public HomeIndexViewModel()
        {
            TopSlider = new List<Photo>();
            WhoWeAre = new List<Partner>();
            CompanyOverView = new List<Partner>();
            WhyUs = new List<Partner>();
            Clients = new List<Partner>();
            Team = new List<Partner>();
            Services = new List<Partner>();
            OurWork = new List<Photo>();
            Process = new List<Partner>();
            Numbers = new List<Partner>();
            Skills = new List<Partner>();
            WorkCategories = new List<string>();
            Slides = new Dictionary<string, List<Photo>>();
            Grades = new List<Grade>();
            //ElementaryGrades = new List<Grade>();
        }
        public List<Partner> WhoWeAre { get; set; }
        public List<Partner> CompanyOverView { get; set; }
        public List<Partner> WhyUs { get; set; }
        public List<Partner> Clients { get; set; }
        public List<Partner> Team { get; set; }
        public List<Partner> Services { get; set; }
        public List<Partner> Numbers { get; set; }
        public List<Grade> Grades { get; set; }
        public List<Subject> Subjects { get; set; }
        public Dictionary<int?, List<Grade>> GradesDictionary { get; set; }
        public List<Category> Countries { get; set; }
        public List<Photo> TopSlider { get; set; }
        public List<Photo> OurWork { get; set; }
        public List<Partner> Process { get; set; }
        public List<Partner> Skills { get; set; }
        public List<string> WorkCategories { get; set; }
        public Dictionary<string, List<Photo>> Slides { get; set; }
        public List<Photo> Products { get; set; }
        public List<Assessment> PopularAssessments { get; set; }
        public List<News> News { get; set; }
        public News SingleNews { get; set; }
        public List<News> Events { get; set; }
    }

    public class ProgramsViewModel : ContentPageViewModel
    {
        public ProgramsViewModel()
        {
            Countries = new List<Category>();
            Grades = new List<Grade>();
            Subjects = new List<Subject>();
            PriceRanges = new List<PriceRange>();
            Assessments = new List<Assessment>();
        }
        public List<Category> Countries { get; set; }
        public List<Grade> Grades { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<PriceRange> PriceRanges { get; set; }
        public List<AssessmentsCount> Counts { get; set; }
        public List<Assessment> Assessments { get; set; }
        
    }

    public class ProductsViewModel : ContentPageViewModel
    {
        public ProductsViewModel()
        {
            MainCategories = new List<Category>();
            Categories = new List<Category>();
        }
        public Category Category { get; set; }
        public List<Category> MainCategories { get; set; }
        public List<Category> Categories { get; set; }
        public IbtecarBusiness.Catalog.DataContracts.Product Product { get; set; }
        public List<IbtecarBusiness.Catalog.DataContracts.Product> Products { get; set; }
        public Dictionary<string, List<IbtecarBusiness.Catalog.DataContracts.Product>> ProductsDictionary { get; set; }
        public string Keyword { get; set; }
    }

    public class GalleryViewModel : ContentPageViewModel
    {
        public GalleryViewModel()
        {
            Photos = new List<Photo>();
            Albums = new List<Album>();
            Strings = new List<string>();

        }
        public List<Album> Albums { get; set; }
        public List<Photo> Photos { get; set; }
        public List<Partner> Careers { get; set; }
        public List<string> Strings { get; set; }
        public Partner Partner { get; set; }
    }

    public class ContactPageViewModel : ContactViewModel
    {
        public string Phone { get; set; }
        public string Subject { get; set; }
    }

    public class CareersViewModel
    {
        public int JobId { get; set; }
        public string JobName { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Wechat { get; set; }
        public string Education { get; set; }
        public string Experience { get; set; }
        public string Trainings { get; set; }
        public string Skills { get; set; }
    }
    public class CoursesPageViewModel : ContactViewModel
    {
        public CoursesPageViewModel()
        {
            Grades = new List<Grade>();
            Assessments = new List<Assessment>();
        }

        public List<Grade> Grades { get; set; }
        public List<Assessment> Assessments { get; set; }
        public int ActiveGrade { get; set; }
    }
}
