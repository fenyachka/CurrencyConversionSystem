namespace Domain.Entities.Currencies
{
    public class Currency : BaseEntity<int>
    {
        public string Code { get; set; }
        public string NameGeo { get; set; }
        public string NameLat { get; set; }
    }
}