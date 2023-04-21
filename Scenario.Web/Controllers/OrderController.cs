using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Scenario.Domain.Entities;
using Scenario.Domain.Interfaces;
using Scenario.Domain.Interfaces.Repository;
using Scenario.Domain.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Scenario.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _uow;
        private static object _locker = new object();
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
            var orderData =(from order in _orderRepository.Get()
                            join customer in _customerRepository.Get() on order.CustomerId equals customer.Id
                            join product in _productRepository.Get() on order.ProductId equals product.Id 
                            join category in _categoryRepository.Get(filter:x=>x.Name.Contains(searchValue)|| string.IsNullOrEmpty(searchValue)) on product.CategoryId equals category.Id
                            select new { 
                                ProductName=product.Name,
                                CategoryName=category.Name,
                                CustomerName=customer.Name,
                                OrderDate=order.OrderDate,
                                Amount=order.Amount,
                                Id=order.Id,
                                OrderName=order.OrderName
            });
            if (!string.IsNullOrEmpty(sortColumn))
            {
                if (sortColumnDirection == "asc")
                {
                    orderData = orderData.OrderBy(p => EF.Property<object>(p, sortColumn));
                }
                else
                {
                    orderData = orderData.OrderByDescending(p => EF.Property<object>(p, sortColumn));
                }
            }
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
        public IActionResult CreateOrder(OrderRequest orderModelRequest)
        {
            lock (_locker)
            {
                var order = new Order();
                if (!ModelState.IsValid)
                {
                    var res = new { code = "Error", message = "Please enter all fields" };
                    return Ok(res);
                }
                var product =  _productRepository.GetByID(orderModelRequest.ProductId).Result;
                if (product != null && product.Id > 0)
                {
                    if (product.Quantity < orderModelRequest.Amount)
                    {
                        var res = new { code = "Error", message = "The amount of the order exceeds the quantity of the product." };
                        return Ok(res);
                    }
                    product.Quantity-=orderModelRequest.Amount;
                }
                order.Amount = orderModelRequest.Amount;
                order.OrderDate = orderModelRequest.OrderDate;
                order.CustomerId = orderModelRequest.CustomerId;
                order.ProductId = orderModelRequest.ProductId;
                order.OrderName = orderModelRequest.OrderName;
                 _orderRepository.Insert(order);
                _productRepository.Update(product);
                var test= _uow.Commit().Result;
            }
            return RedirectToAction("Index");

        }
    }
}
