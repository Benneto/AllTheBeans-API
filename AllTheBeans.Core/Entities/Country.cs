namespace AllTheBeans.Core.Entities
{
    public class Country
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public ICollection<Bean> Beans { get; set; } = new List<Bean>();
    }
}
