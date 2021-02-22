using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PaymentAPI.Controllers;
using PaymentAPI.DAL;
using PaymentAPI.Interfaces;
using PaymentAPI.Models;
using PaymentAPI.Services;
using PaymentAPI.Common;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PaymentTest
{
    public class UnitTestCreate
    {
        PaymentRequestController _controller;
        Mock<IPaymentRequestModel> _paymentManager;
        Mock<IPaymentGateway> _paymentGatewayManager;
        IMapper _mapper;
        Mock<ILogger<PaymentRequestController>> _logger;
        IRequestRepository _service;
        public UnitTestCreate()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new AutoMapperProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            _paymentManager = new Mock<IPaymentRequestModel>();
            _paymentGatewayManager = new Mock<IPaymentGateway>();
            _logger = new Mock<ILogger<PaymentRequestController>>();
            _controller = new PaymentRequestController(_paymentManager.Object, _paymentGatewayManager.Object, _mapper, _logger.Object);
        }



        //Missing Card Number
        [Fact]
        public void Create_MissingFieldPassed_ReturnsBadRequest()
        {
            // Arrange
            var CreditCardMissingItem = new PaymentRequest()
            {
                CardHolder = "Promise",
                ExpirationDate = DateTime.Now.Date.AddDays(+1),
                SecurityCode = "924",
                Amount = 11.00m
            };
            _controller.ModelState.AddModelError("CreditCardNumber", "Required");

            // Act
            var badResponse = _controller.ProcessPayment(CreditCardMissingItem);

            // Assert
            var errorResult = Assert.IsType<ObjectResult>(badResponse);
            Assert.Equal((int)HttpStatusCode.BadRequest, errorResult.StatusCode);
        }

        //Past date passed
        [Fact]
        public void Create_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var CreditCardMissingItem = new PaymentRequest()
            {
                CreditCardNumber = "6030740812618157",
                CardHolder = "Promise",
                ExpirationDate = DateTime.Now.Date.AddDays(-1),
                SecurityCode = "924",
                Amount = 11.00m
            };
            _controller.ModelState.AddModelError("ExpirationDate", "CurrentDate");

            // Act
            var badResponse = _controller.ProcessPayment(CreditCardMissingItem);

            // Assert
            var errorResult = Assert.IsType<ObjectResult>(badResponse);
            Assert.Equal((int)HttpStatusCode.BadRequest, errorResult.StatusCode);
        }

    }
}
