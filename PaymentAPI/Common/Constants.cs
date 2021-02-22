using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Common
{

    public static class Constants
    {
        public const string NULL_REQUEST = "Payment request object is null";
        public const string INTERNAL_SERVER_ERROR = "Internal server error";
        public const string PROCESSING_ERROR = "Something went wrong iwhile processing the request:";
        public const string INVALID_REQUEST = "Invalid payment request object sent from client.";
    }
}
