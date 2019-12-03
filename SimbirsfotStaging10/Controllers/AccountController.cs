﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimbirsfotStaging10.BLL.Interfaces;
using SimbirsfotStaging10.BLL.DTO;
using Microsoft.AspNetCore.Authentication;
using SimbirsfotStaging10.BLL.Services;
using SimbirsfotStaging10.BLL.VK;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimbirsfotStaging10.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly AuthServiceResolver authServiceResolver;

        public AccountController(IUserService userService, AuthServiceResolver authServiceResolver)
        {
            _userService = userService;
            this.authServiceResolver = authServiceResolver;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDTO userModel)
        {
            if (ModelState.IsValid)
            {
                var res = await _userService.Create(userModel);
                if (res.Succeeded)
                {
                    await _userService.SignIn(userModel);
                    return RedirectToAction("List", "Card");
                }
                else
                    foreach (var err in res.Errors)
                        ModelState.AddModelError("", err.Description);
            }

            return View(userModel);
        }


        [HttpPost]
        public async Task<IActionResult> LogIn(UserLoginDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                var res = await _userService.SignInByEmailPassword(userDTO);
                if (res.Succeeded)
                    return RedirectToAction("List", "Card");
                else
                    ModelState.AddModelError("", "Неверный логин/пароль");
                return View(userDTO);
            }
            return View(userDTO);
        }

        [HttpGet]
        public IActionResult SignInVk() => Redirect(authServiceResolver(AuthServices.Vk).UrlGetCode);


        [HttpGet]
        public async Task<IActionResult> Authorize(string service,string code)
        {
            //api вернул, если вдруг какие данные пользователя захотим получить в контроллере
            var authService = authServiceResolver(AuthServiceUtils.StringToEnumElement(service));
            var authUserApi = await authService.Authorize(code, HttpContext);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
