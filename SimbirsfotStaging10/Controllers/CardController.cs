using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimbirsfotStaging10.BLL.DTO;
using SimbirsfotStaging10.BLL.Services;
using SimbirsfotStaging10.BLL.Interfaces;

namespace SimbirsfotStaging10.Controllers
{
    public class CardController : Controller
    {
        private readonly ICardService _cardservice;
        private List<CardDTO> Cards { get; set; }

        public CardController(ICardService cardService)
        {
            _cardservice = cardService;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CardDTO card)
        {
            if (ModelState.IsValid)
            {
                var res = await _cardservice.AddNewCard(card);
                if (res.Succeeded)
                    return RedirectToAction("Display");
                else
                    ModelState.AddModelError("", res.Message);
            }
            return View(card);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            if (ModelState.IsValid)
            {
                var res = await _cardservice.GetCardById(id);
                if (res.Item2.Succeeded)
                    return View(res.Item1);
                else
                {
                    ModelState.AddModelError("", "Карточка с таким Id не найдена.");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CardDTO cardDto)
        {
            if (ModelState.IsValid)
            {
                var res = await _cardservice.EditCard(id, cardDto);
                if (res.Succeeded)
                    RedirectToAction("Index", "Home");
                else
                    ModelState.AddModelError("", res.Message);
            }

            return View(cardDto);
        }

        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var res = await _cardservice.DeleteCard(id);
                if (res.Succeeded)
                    RedirectToAction("Index", "Home");
                else
                    ModelState.AddModelError("", res.Message);
            }
            return View();
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Display()
        {
            if (ModelState.IsValid)
            {
                var res = await _cardservice.DisplayAllCardsFromDB();
                if (res.Item2.Succeeded)
                    return View(res.Item1);
                else
                    ModelState.AddModelError("", res.Item2.Message);
            }
            return View();
        }

    }
}