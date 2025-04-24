namespace AllTheBeans.Core.Entities
{
    public class Bean
    {
        public required Guid Id { get; set; }
        public required string ImportId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Cost { get; set; }
        public required string Colour { get; set; }
        public required bool IsBeanOfTheDay { get; set; }
        public required Guid CountryId { get; set; }
        public Country? Country { get; set; }
        public required Guid ImageId { get; set; }
        public BeanImage? Image { get; set; }
    }
}
