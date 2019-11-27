using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimbirsfotStaging10.BLL.DTO;
using SimbirsfotStaging10.BLL.Interfaces;
using SimbirsfotStaging10.Data.Interface;

namespace SimbirsfotStaging10.Controllers
{
    public class PlatformsController : Controller
    {
        private readonly IAllPlatforms _allPlatfoms;
        private readonly IPlatformsCategory _allCategories;
        //---------------------------------------------------

        private readonly IPlatformsService _platformService;
        private List<PlatformsDTO> Platforms { get;set; }

        public PlatformsController(IPlatformsService platformsService)
        {
            _platformService = platformsService;
        }

        //public PlatformsController(IAllPlatforms allPlatforms, IPlatformsCategory iPlatformsCategory)
        //{
        //    _allPlatfoms = allPlatforms;
        //    _allCategories = iPlatformsCategory;
        //}

        [HttpGet("/Views/Platform/{page?}")]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(PlatformsDTO platform)
        {
            if (ModelState.IsValid)
            {
                var res = await _platformService.AddNewPlatform(platform);
                if (res.Succeeded)
                    //  return RedirectToAction("List");
                    //  RedirectToAction("Privacy", "Platform");//****************************
                    //  RedirectToAction("Privacy");//**************************
                    RedirectToAction("Privacy","Create");
                else
                    ModelState.AddModelError("", res.Message);
            }
            return View(platform);
        }

      



        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            if (ModelState.IsValid)
            {
                var res = await _platformService.GetPlatformById(id);
                if (res.Item2.Succeeded)
                    return View(res.Item1);
                else
                {
                    ModelState.AddModelError("", "Склон с таким Id не найден.");
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
        public async Task<ActionResult> Edit(int id, PlatformsDTO platformsDTO)
        {
            if (ModelState.IsValid)
            {
                var res = await _platformService.EditPlatform(id,platformsDTO);
                if (res.Succeeded)
                    RedirectToAction("Privacy", "Platform");
                else
                    ModelState.AddModelError("", res.Message);
            }
            return View(platformsDTO);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var res = await _platformService.DeletePlatform(id);
                if (res.Succeeded)
                    RedirectToAction("Privacy", "Platform");
                else
                    ModelState.AddModelError("", res.Message);
            }
            return View();
        }


        //---------------------------------------------------
    
        //public ViewResult List() 
        //{
        //   // var Platforms = _allPlatfoms.Platforms;
        //   // return View(new List<PlatformsDTO>());
        //}
        public IActionResult Index()
        {
         //  var Platforms = _allPlatfoms.Platforms;
            return View("~/Views/Privacy.cshtml", new List<PlatformsDTO>());
        }
    }
}