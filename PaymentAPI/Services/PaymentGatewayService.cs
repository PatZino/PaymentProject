using Microsoft.Extensions.Logging;
using PaymentAPI.Interfaces;
using PaymentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PaymentAPI.Common.Utils;
using static PaymentAPI.Common.EnumClasses;

namespace PaymentAPI.Services
{
    public class PaymentGatewayService : IPaymentGateway
    {
        private readonly ICheapPaymentGateway _cheapPaymentManager;
        private readonly IExpensivePaymentGateway _expensivePaymentManager;
        private readonly IPremiumPaymentService _premiumPaymentManager;
        private readonly ILogger<PaymentGatewayService> _logger;

        public PaymentGatewayService(ICheapPaymentGateway cheapPaymentManager, IExpensivePaymentGateway expensivePaymentManager, IPremiumPaymentService premiumPaymentManager, ILogger<PaymentGatewayService> logger)
        {
            _cheapPaymentManager = cheapPaymentManager;
            _expensivePaymentManager = expensivePaymentManager;
            _premiumPaymentManager = premiumPaymentManager;
            _logger = logger;
        }
        public bool ProcessPayment(PaymentRequest model)
        {
            bool paymentStatus = false;
            if (model.Amount >= 0 && model.Amount <= 20)
            {
                paymentStatus = _cheapPaymentManager.ProcessPayment(model);
            }

            if (model.Amount >= 21 && model.Amount <= 500)
            {
                if (IsIExpensivePaymentGatewayAvailable())
                {
                    paymentStatus = _expensivePaymentManager.ProcessPayment(model);
                }
                else
                {
                    paymentStatus = _cheapPaymentManager.ProcessPayment(model);
                }
            }

            if (model.Amount > 500)
            {
                int i = 0;
                do
                {
                    if (IsPaymentProcessed())
                    {
                        paymentStatus = _premiumPaymentManager.ProcessPayment(model);
                        break;
                    }
                    i++;
                }
                while (i < 3);
            }
            return paymentStatus;
        }
    }
}
