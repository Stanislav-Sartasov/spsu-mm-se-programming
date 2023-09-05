using System.ComponentModel.DataAnnotations;

namespace DOWEBAPI.Models
{
    public class Exam
    {
        [Display(Name = "Enter student id:")]
        [Required(ErrorMessage = "Student's id is required!")]
        public long studentId { get; set; }

        [Display(Name = "Enter course id:")]
        [Required(ErrorMessage = "Course's id is required!")]
        public long courseId { get; set; }
    }
}
