using Demo.Application.Requests.UserLogin;
using Demo.Application.Results;
using Demo.Application.Services.UserLoginService;
using Demo.Domain.Entities;
using Demo.Web.Models.UserLogin;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Demo.Web.Controllers
{
    public class UserLoginController : Controller
    {
        private readonly UserLoginService _userLoginService;

        public UserLoginController(UserLoginService userLoginService)
        {
            _userLoginService = userLoginService;
        }
        [HttpGet]
        public IActionResult Index()
        {
           
            return View();
        }
        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            UserLoginRequest request = new()
            {
                UserName = model.UserName,
                Password = model.Password,
            };
            var result = _userLoginService.CheckResult(request);
            if (!result.IsSuccessed) 
            {
                ViewBag.Error = result.Messsage;
                ResultsGeneric<UserLogin>.Fail("không tìm thấy User");
            }
            if(result.Data?.Roler == "Admin")
            {
               return RedirectToAction("Index","Admin");
            }
            if (result?.Data?.Roler == "User")
            {
                return RedirectToAction("Index", "User");
            }
            return RedirectToAction("Index","Home");
        }
    }
}
