using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Common
{
    public class EnumClasses
    {
        public enum PaymentStatus
        {
            pending = 1,
            processed = 2,
            failed = 3
        }


    }
}
