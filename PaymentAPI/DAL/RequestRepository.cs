using Microsoft.EntityFrameworkCore;
using PaymentAPI.DAL;
using PaymentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.DAL
{
    public class RequestRepository : IRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public RequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<PaymentRequestModel> GetPayments()
        {
            return _context.PaymentRequestModels.ToList();
        }

        public PaymentRequestModel GetPayment(long id)
        {
            return _context.PaymentRequestModels.Find(id);
        }

        public PaymentRequestModel StorePayment(PaymentRequestModel model)
        {
            _context.PaymentRequestModels.Add(model);
            _context.SaveChanges();
            return model;
        }

        public PaymentRequestModel UpdatePayment(PaymentRequestModel model)
        {
            _context.Entry(model).State = EntityState.Modified;
            return model;
        }
    }
}
