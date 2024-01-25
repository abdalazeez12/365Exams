//using Microsoft.AspNetCore.Mvc.Rendering;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;

//namespace Imtihani.Models
//{

//    public class ExternalLoginConfirmationViewModel : ContentPageViewModel
//    {
//        [Required]
//        [Display(Name = "Email")]
//        public string Email { get; set; }
//    }

//    public class ExternalLoginListViewModel : ContentPageViewModel
//    {
//        public string ReturnUrl { get; set; }
//    }

//    public class SendCodeViewModel : ContentPageViewModel
//    {
//        public string UserId { get; set; }
//        public string SelectedProvider { get; set; }
//        public ICollection<SelectListItem> Providers { get; set; }
//        public string ReturnUrl { get; set; }
//        public bool RememberMe { get; set; }
//    }

//    public class VerifyCodeViewModel : ContentPageViewModel
//    {
//        [Required]
//        public string Provider { get; set; }

//        [Required]
//        [Display(Name = "Code")]
//        public string Code { get; set; }
//        public string ReturnUrl { get; set; }

//        [Display(Name = "Remember this browser?")]
//        public bool RememberBrowser { get; set; }

//        public bool RememberMe { get; set; }
//    }

//    public class ForgotViewModel
//    {
//        [Required]
//        [Display(Name = "Email")]
//        public string Email { get; set; }
//    }

//    public class LoginViewModel : ContentPageViewModel
//    {
//        public int UserTypeId { get; set; }
//        public string InstitutionName { get; set; }

//        [Required]
//        [Display(Name = "Email")]
//        [EmailAddress]
//        public string Email { get; set; }

//        [Display(Name = "StudentName")]
//        public string StudentName { get; set; }

//        [Required]
//        [DataType(DataType.Password)]
//        [Display(Name = "Password")]
//        public string Password { get; set; }

//        [Display(Name = "Remember me?")]
//        public bool RememberMe { get; set; }
//    }

//    public class RegisterViewModel : ContentPageViewModel
//    {
//        [Required]
//        public string FirstName { get; set; }

//        [Required]
//        public string LastName { get; set; }

//        //[Required]
//        //public DateTime BirthDate { get; set; }

//        [Required]
//        [EmailAddress]
//        public string Email { get; set; }

//        [Required]
//        public int CountryId { get; set; }

//        //[Required]
//        //public string CountryCode { get; set; }

//        //public string OfficeContact { get; set; }

//        //[Required]
//        //public int PreferedLanguageId { get; set; }

//        //public string Mobile { get; set; }

//        [Required]
//        //[MinLength(6, ErrorMessageResourceName = "MinLengthAttribute", ErrorMessageResourceType = typeof(Resources.Validation))]
//        //[MaxLength(100, ErrorMessageResourceName = "MaxLengthAttribute", ErrorMessageResourceType = typeof(Resources.Validation))]
//        [DataType(DataType.Password)]
//        public string Password { get; set; }

//        [Required]
//        [DataType(DataType.Password)]
//        //[Compare("Password", ErrorMessageResourceName = "CompareAttribute", ErrorMessageResourceType = typeof(Resources.Validation))]
//        public string ConfirmPassword { get; set; }

//        //public string Gender { get; set; }

//        [Required]
//        public int UserType { get; set; }
//    }

//    /*public class RegisterInstitutionViewModel : Institution
//    {
//        [Required]
//        //[MinLength(6, ErrorMessageResourceName = "MinLengthAttribute", ErrorMessageResourceType = typeof(Resources.Validation))]
//        //[MaxLength(100, ErrorMessageResourceName = "MaxLengthAttribute", ErrorMessageResourceType = typeof(Resources.Validation))]
//        [DataType(DataType.Password)]
//        public string Password { get; set; }

//        [Required]
//        [DataType(DataType.Password)]
//        //[Compare("Password", ErrorMessageResourceName = "CompareAttribute", ErrorMessageResourceType = typeof(Resources.Validation))]
//        public string ConfirmPassword { get; set; }
//    }*/

//    public class ResetPasswordViewModel : ContentPageViewModel
//    {
//        [Required]
//        [EmailAddress]
//        [Display(Name = "Email")]
//        public string Email { get; set; }

//        [Required]
//        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
//        [DataType(DataType.Password)]
//        [Display(Name = "Password")]
//        public string Password { get; set; }

//        [DataType(DataType.Password)]
//        [Display(Name = "Confirm password")]
//        //[Compare("Password", ErrorMessageResourceName = "CompareAttribute", ErrorMessageResourceType = typeof(Resources.Validation))]
//        public string ConfirmPassword { get; set; }

//        public string Code { get; set; }
//    }

//    public class ForgotPasswordViewModel : ContentPageViewModel
//    {
//        [Required]
//        [EmailAddress]
//        [Display(Name = "Email")]
//        public string Email { get; set; }
//    }
//}