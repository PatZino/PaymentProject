using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static PaymentAPI.Common.EnumClasses;
using PaymentAPI.Common;
using PaymentAPI.DAL;
using PaymentAPI.Interfaces;
using PaymentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace PaymentAPI.Controllers
{
    [Route("api/PaymentRequest")]
    [ApiController]
    public class PaymentRequestController : ControllerBase
    {
        private readonly IPaymentGateway _paymentGatewayManager;
        private readonly IPaymentRequestModel _paymentManager;
        private readonly IMapper _mapper;
        private readonly ILogger<PaymentRequestController> _logger;

        public PaymentRequestController(IPaymentRequestModel paymentManager, IPaymentGateway paymentGatewayManager, IMapper mapper, ILogger<PaymentRequestController> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _paymentManager = paymentManager;
            _paymentGatewayManager = paymentGatewayManager;
        }


        // POST api/<PaymentRequestController>

        [HttpPost]
        public IActionResult ProcessPayment(PaymentRequest model)
        {
            try
            {

                if (model == null)
                {
                    _logger.LogError(Constants.NULL_REQUEST);
                    return StatusCode((int)HttpStatusCode.BadRequest, Constants.NULL_REQUEST);
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError(Constants.INVALID_REQUEST);
                    return StatusCode((int)HttpStatusCode.BadRequest, Constants.INVALID_REQUEST);
                }

                var request = _mapper.Map<PaymentRequestModel>(model);

                var created = _paymentManager.StorePayment(request);

                var response = _paymentGatewayManager.ProcessPayment(model);

                created.PaymentState = (response == true) ? PaymentStatus.processed : PaymentStatus.failed;

                _paymentManager.UpdatePayment(created);

                return StatusCode((int)HttpStatusCode.OK, request);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.PROCESSING_ERROR + ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, Constants.INTERNAL_SERVER_ERROR);
            }
        }

    }
}
