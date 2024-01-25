using System;

namespace Imtihani.Models
{
    public class ExamSubmissionResult
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public int AssessmentId { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string Answers { get; set; }
        public bool IsCorrected { get; set; }
        public int QuestionCount { get; set; }
        public int CorrectCount { get; set; }
        public string CertificatePath { get; set; }
        public int Duration { set; get; }
        public int? Price { set; get; }
        public string GradeName { set; get; }
        public string InstructorName { set; get; }
        public string Email { set; get; }


    }

}
