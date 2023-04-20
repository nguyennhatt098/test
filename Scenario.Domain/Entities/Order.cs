using System;
using System.Collections.Generic;
using System.Text;

namespace Scenario.Domain.Entities
{
	public class Order
	{
		public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }
        public Product Product { get; set; }
        public string OrderName { get; set; }
        public int Amount { get; set; }

        public int ProductId { get; set; }

        public int CustomerId { get; set; }
    }
}
