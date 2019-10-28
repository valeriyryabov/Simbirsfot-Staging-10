﻿using System;
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

        public CardController(ICardService cardService)
        {
            _cardservice = cardService;
        }

        // GET: Card
        public ActionResult Index()
        {
            return View();
        }

        // GET: Card/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Card/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Card/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CardDTO cardModel)
        {
            if (ModelState.IsValid)
            {
            }


            try
            {
                // TODO: Add insert logic here


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Card/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Card/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Card/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Card/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}