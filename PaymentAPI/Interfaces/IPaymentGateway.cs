using PaymentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Interfaces
{
    public interface IPaymentGateway
    {
        bool ProcessPayment(PaymentRequest model);
    }
}
