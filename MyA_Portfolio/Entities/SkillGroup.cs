namespace MyA_Portfolio.Entities
{
    public class SkillGroup
    {
        public int SkillGroupId { get; set; }
        public string Title { get; set; }          // "Backend Development"
        public string Description { get; set; }    // "Güvenli, modüler ve yüksek performanslı..."
        public string? IconClass { get; set; }      // "bi bi-hdd-network" gibi, kartın ikonu için

        public List<Skill> Skills { get; set; } = new();
    }
}
