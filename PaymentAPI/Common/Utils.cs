using PaymentAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Common
{
    public class Utils
    {
        public class CurrentDateAttribute : ValidationAttribute
        {
            public CurrentDateAttribute()
            {
            }

            public override bool IsValid(object value)
            {
                var dt = (DateTime)value;
                if (dt >= DateTime.Now)
                {
                    return true;
                }
                return false;
            }
        }



        public static bool IsIExpensivePaymentGatewayAvailable()
        {
            return true;
        }

        public static bool IsPaymentProcessed()
        {
            return true;
        }

    }
}
