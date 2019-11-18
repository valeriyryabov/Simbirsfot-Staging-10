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
                var res = await _cardservice.AddNew(card);
                if (res.Succeeded)
                    return RedirectToAction("List");
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
                var res = await _cardservice.GetById(id);
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
        public async Task<ActionResult> Edit(int? id)
        {
            CardDTO cardDTO;
            if (!id.HasValue)
            {
                cardDTO = new CardDTO();
            }
            else
            {
                cardDTO = (await _cardservice.GetById(id.Value)).Item1;
            }
            return View(cardDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CardDTO cardDto)
        {
            if (ModelState.IsValid)
            {
                var res = await _cardservice.Edit(id, cardDto);
                if (res.Succeeded)
                    return RedirectToAction("List");
                else
                    ModelState.AddModelError("", res.Message);
            }
            return View(cardDto);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            CardDTO cardDTO;
            if (!id.HasValue)
            {
                cardDTO = new CardDTO();
            }
            else
            {
                cardDTO = (await _cardservice.GetById(id.Value)).Item1;
            }
            return View(cardDTO);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var res = await _cardservice.Delete(id);
                if (res.Succeeded)
                    return RedirectToAction("List");
                else
                    ModelState.AddModelError("", res.Message);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            if (ModelState.IsValid)
            {
                var res = await _cardservice.GetAllFromDB();
                if (res.Item2.Succeeded)
                    return View(res.Item1);
                else
                    ModelState.AddModelError("", res.Item2.Message);
            }
            return View();
        }

        [HttpPost, ActionName("List")]
        public async Task<IActionResult> ListConfirmed()
        {
            if (ModelState.IsValid)
            {
                var res = await _cardservice.GetAllFromDB();
                if (res.Item2.Succeeded)
                    return View(res.Item1);
                else
                    ModelState.AddModelError("", res.Item2.Message);
            }
            return View();
        }

    }
}