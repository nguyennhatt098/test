
using Moq;
using NUnit.Framework;
using Scenario.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{
    [TestFixture]

    public class OrderControllerTests
    {
        private Mock<IOrderRepository> _mockRepo;
        //private Mock<IProductService> _mockRepo1;
        //private Mock<ICustomerService> _mockRepo2;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IOrderRepository>();
        }
        [Test]
        public void GetAllOrders_ReturnsAllOrders()
        {
        }

    }
}
