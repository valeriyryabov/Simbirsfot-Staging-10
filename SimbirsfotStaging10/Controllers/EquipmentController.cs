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

        private List<EquipmentDTO> Cards { get; set; }

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
    }
}