using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp1.Services
{
    public class WalletUser
    {
        [Key]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public decimal MbwaySaldo { get; set; }
        public decimal ApplePaySaldo { get; set; }
        public decimal CreditCardSaldo { get; set; }

        public virtual User User { get; set; }
    }
}