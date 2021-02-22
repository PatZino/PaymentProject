using PaymentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Interfaces
{
    public interface IPaymentRequestModel
    {
        List<PaymentRequestModel> GetPayments();
        PaymentRequestModel GetPayment(long id);
        PaymentRequestModel StorePayment(PaymentRequestModel model);
        PaymentRequestModel UpdatePayment(PaymentRequestModel model);
    }
}
