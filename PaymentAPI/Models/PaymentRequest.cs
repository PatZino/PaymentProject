using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static PaymentAPI.Common.Utils;

namespace PaymentAPI.Models
{
    public class PaymentRequest
    {
        [Required]
        [CreditCard]
        public string CreditCardNumber { get; set; }

        [Required]
        public string CardHolder { get; set; }

        [Required]
        [CurrentDate(ErrorMessage = "Date must be after or equal to current date")]
        public DateTime ExpirationDate { get; set; }

        [StringLength(3, ErrorMessage = "Security Code should be 3 digits")]
        public string SecurityCode { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public decimal Amount { get; set; }
    }
}
