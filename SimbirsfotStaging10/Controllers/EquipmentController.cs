using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimbirsfotStaging10.BLL.Interfaces;
using SimbirsfotStaging10.BLL.DTO;
using SimbirsfotStaging10.BLL.Services;

namespace SimbirsfotStaging10.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly IEquipmentService _equipmentService;

        private List<EquipmentDTO> EquipmentItemList { get; set; }

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(EquipmentDTO dto)
        {
            if (ModelState.IsValid)
            {
                var res = await _equipmentService.AddNewAsync(dto);
                if (res.Succeeded)
                    return RedirectToAction("List");
                else
                    ModelState.AddModelError("", res.Message);
            }
            return View(dto);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            if (ModelState.IsValid)
            {
                var res = await _equipmentService.GetByIdAsync(id);
                if (res.Item2.Succeeded)
                    return View(res.Item1);
                else
                {
                    ModelState.AddModelError("", "Оборудование с таким Id не найдено.");
                }
            }
            return View();
        }


        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            EquipmentDTO dto;
            if (!id.HasValue)
            {
                dto = new EquipmentDTO();
            }
            else
            {
                dto = (await _equipmentService.GetByIdAsync(id.Value)).Item1;
            }
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EquipmentDTO dto)
        {
            if (ModelState.IsValid)
            {
                var res = await _equipmentService.EditAsync(id, dto);
                if (res.Succeeded)
                    return RedirectToAction("List");
                else
                    ModelState.AddModelError("", res.Message);
            }
            return View(dto);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            EquipmentDTO dto;
            if (!id.HasValue)
            {
                dto = new EquipmentDTO();
            }
            else
            {
                dto = (await _equipmentService.GetByIdAsync(id.Value)).Item1;
            }
            return View(dto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var res = await _equipmentService.DeleteAsync(id);
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
                var res = await _equipmentService.GetAllFromDBAsync();
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
                var res = await _equipmentService.GetAllFromDBAsync();
                if (res.Item2.Succeeded)
                    return View(res.Item1);
                else
                    ModelState.AddModelError("", res.Item2.Message);
            }
            return View();
        }
    }
}