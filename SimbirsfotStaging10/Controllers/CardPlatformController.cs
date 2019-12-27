using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimbirsfotStaging10.BLL.DTO;
using SimbirsfotStaging10.BLL.Interfaces;
using SimbirsfotStaging10.BLL.Infrastructure;

namespace SimbirsfotStaging10.Controllers
{
    public class CardPlatformController : Controller
    {
        private readonly ICardPlatformService _cardPlatformService;
        private readonly ICardService _cardService;
        private readonly IUserService _userService;

        public CardPlatformController(ICardPlatformService cardPlatformService, IUserService userService, ICardService cardService)
        {
            _cardPlatformService = cardPlatformService;
            _cardService = cardService;
            _userService = userService;
        }


        // POST: CardPlatform/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IEnumerable<PlatformsDTO> platforms, DateTime dateEndForNewCard)
        {
            var user = await _userService.GetCurrentUserAsync();
            if (!CreateCardPlatformValidation(platforms, dateEndForNewCard, user) || !ModelState.IsValid)
                return View("~/Views/Platforms/Privacy.cshtml", platforms);

            var res = await _cardService.AddNew(CreateCardDto(dateEndForNewCard), user.Id);
            if (!res.Item2.Succeeded)
                return PlatformViewWithModelError(res.Item2.Message, platforms);

            var resLinking = await _cardPlatformService.AddCardWithPlatforms(res.Item1.Id,
                platforms.Where(p => p.IsReserved).Select(p => p.Id));
            if (!resLinking.Succeeded)
                return PlatformViewWithModelError(resLinking.Message, platforms);

            return RedirectToAction("ListForCurrentUser", "Card");
        }


        private IActionResult PlatformViewWithModelError(string message, object model)
        {
            ModelState.AddModelError("", message);
            return View("~/Views/Platforms/Privacy.cshtml", model);
        }


        private CardDTO CreateCardDto(DateTime dateEnd)
        {
            return new CardDTO
            {
                DateBegin = DateTime.Now,
                DateEnd = dateEnd,
            };
        }


        //наверное, лучше создать класс валидатор
        private bool CreateCardPlatformValidation(IEnumerable<PlatformsDTO> platforms, DateTime dateEnd, DAL.Entities.User curUser)
        {
            var errorFlag = false;

            if (ChangeFlagAddErrorIf(dateEnd.CompareByDateMonthYear(DateTime.Now) == -1, ref errorFlag))
                ModelState.AddModelError("", $"Дата не может быть меньше сегодняшней: { DateTime.Now.ToString()}");

            if (ChangeFlagAddErrorIf(curUser == null, ref errorFlag))
                ModelState.AddModelError("", "Для создания карты необходимо пройти аутентификацию.");

            if (ChangeFlagAddErrorIf(!platforms.Any(p => p.IsReserved), ref errorFlag))
                ModelState.AddModelError("", "Для создания карты необходимо выбрать хотя бы одну платформу.");

            return !errorFlag;
        }


        private bool ChangeFlagAddErrorIf(bool condition, ref bool flag)
        {
            if (condition)
            {
                flag = true;
                return true;
            }
            return false;
        }
    }
}