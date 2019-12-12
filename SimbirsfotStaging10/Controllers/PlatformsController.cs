using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimbirsfotStaging10.BLL.DTO;
using SimbirsfotStaging10.BLL.Interfaces;

namespace SimbirsfotStaging10.Controllers
{
    public class PlatformsController : Controller
    {
        private readonly IPlatformsService _platformService;
        
        public PlatformsController(IPlatformsService platformsService)
        {
            _platformService = platformsService;
        }

        [HttpGet]
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
                    RedirectToAction("Privacy","Create");
                else
                    ModelState.AddModelError("", res.Message);
            }
            return RedirectToAction("Index");

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
        public async Task<ActionResult> Edit(int? id)
        {
            PlatformsDTO DTO;
            if (!id.HasValue)
            {
                DTO = new PlatformsDTO();
            }
            else
            {
                DTO = (await _platformService.GetPlatformById(id.Value)).Item1;
            }
            return View(DTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, PlatformsDTO Dto)
        {
            if (ModelState.IsValid)
            {
                var res = await _platformService.EditPlatform(id, Dto);
                if (res.Succeeded)
                    return RedirectToAction("Index");
                else
                    return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(Dto);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            PlatformsDTO DTO;
            if (!id.HasValue)
            {
                DTO = new PlatformsDTO();
            }
            else
            {
                DTO = (await _platformService.GetPlatformById(id.Value)).Item1;
            }
            return View(DTO);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var res = await _platformService.DeletePlatform(id);
                if (res.Succeeded)
                    return RedirectToAction("Index");
                else
                    return StatusCode(StatusCodes.Status404NotFound);
            }
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var res = await _platformService.GetAllPlatformsFromDB();
            if (res.Item2.Succeeded)
                return View("~/Views/Platforms/Privacy.cshtml", res.Item1);
            else
                return StatusCode(StatusCodes.Status404NotFound);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            if (ModelState.IsValid)
            {
                var res = await _platformService.GetAllPlatformsFromDB();
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
                var res = await _platformService.GetAllPlatformsFromDB();
                if (res.Item2.Succeeded)
                    return View(res.Item1);
                else
                    ModelState.AddModelError("", res.Item2.Message);
            }
            return View();
        }
    }
}