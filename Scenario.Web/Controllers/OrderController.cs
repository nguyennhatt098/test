using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Scenario.Domain.Entities;
using Scenario.Domain.Interfaces;
using Scenario.Domain.Interfaces.Repository;
using Scenario.Domain.Model;

namespace Scenario.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _uow;
        public OrderController(IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IUnitOfWork uow,
            ICategoryRepository categoryRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _uow = uow;
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> GetOrder()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            var orderData =(from order in _orderRepository.Get(orderBy: sortColumn, sortColumnDirection: sortColumnDirection)
                            join customer in _customerRepository .Get() on order.CustomerId equals customer.Id
                            join product in _productRepository.Get() on order.ProductId equals product.Id 
                            join category in _categoryRepository.Get on product.CategoryId equals category.Id
                            select new { 
                                ProductName=product.Name,
                                CategoryName=category.na
            });
            var data = await orderData.Skip(skip).Take(pageSize).ToListAsync();
            var recordsTotal = orderData.Count();
            var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
            return Ok(jsonData);
        }
        public async Task<IActionResult> Create()
        {
            ViewData["Customers"] = await _customerRepository.Get().ToListAsync();
            ViewData["Products"] = await _productRepository.Get().ToListAsync();
            return PartialView("_Create");
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderRequest orderModelRequest)
        {
            var order = new Order();
            if (!ModelState.IsValid)
            {
                var res = new { code = "Error", message = "Please enter all fields" };
                return Ok(res);
            }
            var product =await _productRepository.GetByID(orderModelRequest.ProductId);
            if (product != null && product.Id > 0)
            {
                if (product.Quantity < orderModelRequest.Amount)
                {
                    var res = new { code = "Error", message = "The amount of the order exceeds the quantity of the product." };
                    return Ok(res);
                }
            }
            order.Amount = orderModelRequest.Amount;
            order.OrderDate = orderModelRequest.OrderDate;
            order.CustomerId = orderModelRequest.CustomerId;
            order.ProductId = orderModelRequest.ProductId;
            order.OrderName = orderModelRequest.OrderName;
            await _orderRepository.Insert(order);
            await _uow.Commit();
            return RedirectToAction("Index");

        }
    }
}
