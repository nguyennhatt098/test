using Scenario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scenario.Domain.Model
{
    public class OrderRequest
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderName { get; set; }
        public int Amount { get; set; }

        public int ProductId { get; set; }

        public int CustomerId { get; set; }
    }
}
