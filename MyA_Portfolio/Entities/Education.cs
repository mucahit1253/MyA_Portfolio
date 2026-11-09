namespace MyA_Portfolio.Entities
{
    public class Education
    {
        public int EducationId { get; set; }
        public string Title { get; set; }
        public string? SubTitle { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } 
    }
}
