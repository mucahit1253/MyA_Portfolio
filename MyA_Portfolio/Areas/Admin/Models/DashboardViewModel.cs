using MyA_Portfolio.Entities;

namespace MyA_Portfolio.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public int AboutCount { get; set; }
        public int EducationCount { get; set; }
        public int ExperienceCount { get; set; }
        public int MessageCount { get; set; }
        public int UnreadMessageCount { get; set; }
        public int CategoryCount { get; set; }
        public int ProjeCount { get; set; }
        public int ActiveProjectCount { get; set; }
        public int SkillCount { get; set; }
        public List<Education> UpcomingEducations { get; set; } = new();
        public List<RecentMessageVm> RecentMessages { get; set; } = new();
        public List<CategoryProjectCountVm> ProjectsByCategory { get; set; } = new();
    }


    public class RecentMessageVm
    {
        public int MessageId { get; set; }
        public string SenderName { get; set; }
        public string Subject { get; set; }
        public DateTime SentDate { get; set; }
        public bool IsRead { get; set; }
        public string Body { get; set; }
    }
    public class CategoryProjectCountVm
    {
        public string CategoryName { get; set; }
        public int ProjectCount { get; set; }
    }
}
