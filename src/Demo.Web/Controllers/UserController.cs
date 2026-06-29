using Demo.Application.Requests.Users;
using Demo.Application.Results;
using Demo.Application.Services.ProductService;
using Demo.Application.Services.UserService;
using Demo.Domain.Entities;
using Demo.Web.Models.Products;
using Demo.Web.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            List<User> users = _userService.GetAllUsers();
            List<UserListItemViewModel> model = users.Select(u => 
            new UserListItemViewModel
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                Phone = u.Phone,
                Address = u.Address,
                City = u.City,
                Region = u.Region,
                PostalCode = u.PostalCode,
            }).ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int id) 
        {
            ResultsGeneric<User> users = _userService.CheckResult(id);
            if(!users.IsSuccessed || users.Data == null)
            {
                TempData["ErrorMessage"] = users.Messsage;
                return RedirectToAction("Index");
            };
            UserListItemViewModel model = new()
            {
                UserName = users.Data.UserName,
                Email = users.Data.Email,
                Phone = users.Data.Phone,
                Address = users.Data.Address,
                City = users.Data.City,
                Region = users.Data.Region,
                PostalCode = users.Data.PostalCode,
            };      
            return View(model);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View(new AddUserViewModel());
        }
        [HttpPost]
        public IActionResult Add(AddUserViewModel model) 
        {
            AddUserRequest request = new()
            {
               
                UserName = model.UserName,
                Email = model.Email,
                Phone = model.Phone,
                Address = model.Address,
                City = model.City,
                Region = model.Region,
                PostalCode = model.PostalCode,
            };
            ResultsGeneric<User> results = _userService.AddUser(request);
            if (!results.IsSuccessed)
            {
                ViewBag.ErrorMessage = results.Messsage;
                return View(model);
            }
            TempData["SuccessMessage"] = results.Messsage;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            ResultsGeneric<User> results = _userService.CheckResult(id);
            if (!results.IsSuccessed || results.Data == null) 
            {
                TempData["ErrorMessage"] = results.Messsage;
                return RedirectToAction("Index");
            }
            UpdateUserViewModel model = new UpdateUserViewModel() 
            {
                Id = results.Data.Id,
                UserName = results.Data.UserName,
                Email = results.Data.Email,
                Phone = results.Data.Phone,
                Address = results.Data.Address,
                City = results.Data.City,
                Region = results.Data.Region,
                PostalCode= results.Data.PostalCode,
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Update(UpdateUserViewModel model)
        {
            UpdateUserRequest request = new UpdateUserRequest() 
            {
              
                UserName = model.UserName,
                Email = model.Email,
                Phone = model.Phone,
                Address = model.Address,
                City = model.City,
                Region = model.Region,
                PostalCode = model.PostalCode,

            };
            ResultsGeneric<User> results = _userService.UpdateUser(request);
            if (!results.IsSuccessed)
            {
                ViewBag.ErrorMessage = results.Messsage;
                return View(model);
            }
            TempData["SuccessMessage"] = results.Messsage;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id) 
        {
            ResultsGeneric<User> results = _userService.CheckResult(id);
            if (!results.IsSuccessed || results.Data == null)
            {
                TempData["ErrorMessage"] = results.Messsage;
                return RedirectToAction("Index");
            }
            DeleteUserViewModel model = new DeleteUserViewModel()
            {
                Id = results.Data.Id,
                UserName = results.Data.UserName,
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(DeleteUserViewModel model)
        {
            DeleteUserRequest request = new DeleteUserRequest() 
            {
                Id = model.Id,
            };
            ResultsGeneric<User> results = _userService.DeleteUser(request);
            if (!results.IsSuccessed)
            {
                ViewBag.ErrorMessage = results.Messsage;
                return View(model);
            }
            TempData["SuccessMessage"] = results.Messsage;
            return RedirectToAction("Index");
        }
    }
}
