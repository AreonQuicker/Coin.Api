namespace CoinApi.Domain.Models.DomainModels
{
    public class CoinDomainModel : AuditDomainModel
    {
        public int Id { get; set; }
        public int Rank { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Slug { get; set; }
    }
}