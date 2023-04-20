using System;
using System.Collections.Generic;
using System.Text;

namespace Scenario.Domain.Entities
{
	public class Product
	{
        public int Id { get; set; }
		public string Name { get; set; }
        public double Price { get; set; }
        public string Desctiption { get; set; }
        public int Quantity { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
