using AutoMapper;
using MagicVilla_Utillity;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MagicVilla_Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public VillaController(IVillaService villaService, IMapper mapper)
        {
            _villaService = villaService;
            _mapper = mapper;
        }

        // GET: VillaController
        public async Task<IActionResult> Index()
        {
            List<VillaDTO> list = new();
            string token = HttpContext.Session.GetString(SD.SessionToken);
            var response = await _villaService.GetAllAync<APIResponse>(token);
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        [Authorize(Roles = "admin")]
        public  ActionResult Create()
        {
            return View();
        }

        // POST: VillaController/Create
        [Authorize(Roles ="admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VillaCreateDTO villaDTO)
        {
            if (ModelState.IsValid)
            {
                string token = HttpContext.Session.GetString(SD.SessionToken);
                var response = await _villaService.CreateAsync<APIResponse>(villaDTO,token);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa created successfully";
                    return RedirectToAction(nameof(Index));
                }
            }
            TempData["error"] = "Error encountered.";
            return View(villaDTO);
        }

        // GET: VillaController/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(int id)
        {
            string token = HttpContext.Session.GetString(SD.SessionToken);
            var response = await _villaService.GetAync<APIResponse>(id,token);
            if (response != null && response.IsSuccess)
            {
                var villaDTO = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
                var villaUpdate = _mapper.Map<VillaUpdateDTO>(villaDTO);
                return View(villaUpdate);
            }
            return NotFound();
        }

        // POST: VillaController/Edit/5
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(VillaUpdateDTO villaDTO)
        {
            if (ModelState.IsValid)
            {
                string token = HttpContext.Session.GetString(SD.SessionToken);
                var response = await _villaService.UpdateAsync<APIResponse>(villaDTO,token);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa updated successfully";
                    return RedirectToAction(nameof(Index));
                    
                }
            }
            TempData["error"] = "Error encountered.";
            return View(villaDTO);
        }

        // GET: VillaController/Delete/5
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            string token = HttpContext.Session.GetString(SD.SessionToken);
            var response = await _villaService.GetAync<APIResponse>(id,token);

            if (response != null && response.IsSuccess)
            {
                var villaDTO = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
                return View(villaDTO);
            }
            return NotFound();
        }

        // POST: VillaController/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(VillaDTO villaDTO)
        {
            string token = HttpContext.Session.GetString(SD.SessionToken);
            var response = await _villaService.DeleteAync<APIResponse>(villaDTO.Id,token);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Villa deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Error encountered.";
            return View(villaDTO);
        }
    }
}
