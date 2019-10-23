using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimbirsfotStaging10.BLL.Interfaces;
using SimbirsfotStaging10.BLL.DTO;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimbirsfotStaging10.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;


        public AccountController(IUserService userService)
        {
            _userService = userService;
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
                    return RedirectToAction("Index", "Home");
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
            var res = await _userService.SignInByEmailPassword(userDTO);
            if (res.Succeeded)
                return RedirectToAction("Index", "Home");
            else
                ModelState.AddModelError("", "Неверный логин/пароль");
            return View(userDTO);
        }


        public async Task<IActionResult> LogOut()
        {
            await _userService.LogOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
