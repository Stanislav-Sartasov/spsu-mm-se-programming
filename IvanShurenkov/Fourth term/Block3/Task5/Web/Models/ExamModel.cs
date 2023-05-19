namespace Web.Models
{
    public class ExamModel
    {
        public long StudentId { get; set; }
        public long CourseId { get; set; }

        public string LableText { get; set; }

        public ExamModel() 
        {
            StudentId = 0;
            CourseId = 0;
            LableText = String.Empty;
        }
    }
}
