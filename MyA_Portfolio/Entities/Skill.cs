namespace MyA_Portfolio.Entities
{
    public class Skill
    {
        public int SkillId { get; set; }
        public string Name { get; set; }            // "C#", "ASP.NET Core"

        public int SkillGroupId { get; set; }       // FK
        public SkillGroup SkillGroup { get; set; }
        public List<Experience> Experiences { get; set; } = new();
    }
}
