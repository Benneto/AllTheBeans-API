namespace AllTheBeans.Core.Entities
{
    public class BeanImage
    {
        public required Guid Id { get; set; }
        public required string Url { get; set; }

        public ICollection<Bean> Beans { get; set; } = new List<Bean>();
    }
}
