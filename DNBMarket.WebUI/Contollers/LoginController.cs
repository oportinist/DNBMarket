using DNBMarket.BusinessLogic.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DNBMarket.WebUI.Contollers
{
    public class LoginController : BaseController
    {
        private readonly IUserService _userService;
        public LoginController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Control(string email, string password)
        {
            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password))
            {
                var user = _userService.GetUser(email, password).Result.Data;
                if (user != null)
                {

                    HttpContext.Session.Set("User", JsonSerializer.SerializeToUtf8Bytes(user));
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ViewBag.Message = "Kullanıcı Bulunamadı";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewBag.Message = "E - Mail / Şifre Bilgisi Boş Olamaz";
                return RedirectToAction("Index");
            } 
        }
    }
}
