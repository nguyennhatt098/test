using Moq;
using Scenario.Domain.Entities;
using Scenario.Domain.Interfaces;
using Scenario.Domain.Interfaces.Repository;

namespace TestProject1
{
    public class UnitTest1
    {
        private Mock<IOrderRepository> _orderRepositoryMock;
        private Mock<IProductRepository> _productRepositoryMock;
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<IUnitOfWork> _uowMock;
        private Mock<ICategoryRepository> _categoryRepositoryMock;
        public UnitTest1()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _uowMock = new Mock<IUnitOfWork>();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
        }
        [Fact]
        public void Test1()
        {
            var mockList = new List<Order>
            {
                new Order { OrderName = "mock article 1" },
                new Order { OrderName = "mock article 2" }
            };
            var mockQueryable = mockList.AsQueryable();
            _orderRepositoryMock.Setup(repo => repo.Get(null, null, null, null))
                .Returns(mockQueryable);

            Assert.Equal(2, mockQueryable.Count());  
        }
    }
}