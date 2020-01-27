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
    public class EquipmentTypeController : Controller
    {
        private readonly IEquipmentTypeService _equipmentTypeService;

        private List<EquipmentDTO> EquipmentItemList { get; set; }

        public EquipmentTypeController(IEquipmentTypeService equipmentService)
        {
            _equipmentTypeService = equipmentService;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(EquipmentTypeDTO dto)
        {
            if (ModelState.IsValid)
            {
                var res = await _equipmentTypeService.AddNewAsync(dto);
                if (res.Succeeded)
                    return RedirectToAction("List");
                else
                    ModelState.AddModelError("", res.Message);
            }
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            if (ModelState.IsValid)
            {
                var res = await _equipmentTypeService.GetAllFromDBAsync();
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
                var res = await _equipmentTypeService.GetAllFromDBAsync();
                if (res.Item2.Succeeded)
                    return View(res.Item1);
                else
                    ModelState.AddModelError("", res.Item2.Message);
            }
            return View();
        }
    }
}