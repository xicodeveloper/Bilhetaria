namespace BlazorApp1.Services.DataBase.DBEntities
{
    public class WalletUser  : DbItem
    {
        
        public decimal MbwaySaldo { get; set; }
        public decimal ApplePaySaldo { get; set; }
        public decimal CreditCardSaldo { get; set; }
        public virtual User User { get; set; }
    }
}