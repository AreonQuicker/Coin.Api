namespace CoinApi.Domain.Models.DomainModels
{
    public class CurrencyDomainModel : AuditDomainModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Sign { get; set; }
    }
}