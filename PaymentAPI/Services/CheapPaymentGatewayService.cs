﻿using PaymentAPI.Interfaces;
using PaymentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Services
{
    public class CheapPaymentGatewayService : ICheapPaymentGateway
    {
        public bool ProcessPayment(PaymentRequest model)
        {
            return true;
        }
    }
}
