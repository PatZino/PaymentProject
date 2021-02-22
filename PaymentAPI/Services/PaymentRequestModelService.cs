using static PaymentAPI.Common.EnumClasses;
using PaymentAPI.DAL;
using PaymentAPI.Interfaces;
using PaymentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Services
{
    public class PaymentRequestModelService : IPaymentRequestModel
    {
        private readonly IRequestRepository _requestRepository;

        public PaymentRequestModelService(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public List<PaymentRequestModel> GetPayments()
        {
            return _requestRepository.GetPayments();
        }

        public PaymentRequestModel GetPayment(long id)
        {
            return _requestRepository.GetPayment(id);
        }

        public PaymentRequestModel StorePayment(PaymentRequestModel model)
        {
            model.CreatedDate = DateTime.Now;
            model.PaymentState = PaymentStatus.pending;
            var created = _requestRepository.StorePayment(model);
            return created;
        }

        public PaymentRequestModel UpdatePayment(PaymentRequestModel model)
        {
            model.UpdatedDate = DateTime.Now;
            var updated = _requestRepository.UpdatePayment(model);
            return updated;
        }
    }
}
