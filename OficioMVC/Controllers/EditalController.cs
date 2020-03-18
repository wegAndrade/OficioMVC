using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OficioMVC.Models;
using OficioMVC.Service;

namespace OficioMVC.Controllers
{
    public class EditalController : Controller
    {
        private readonly EditalService _editalService;

        public EditalController(EditalService editalService)
        {
            _editalService = editalService;
        }

        // GET: Edital
        public ActionResult Index()
        {
            return View();
        }

        // GET: Edital/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Edital/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Edital/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Edital edital)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _editalService.InsertAsync(edital);
            return RedirectToAction(nameof(Index));
        }

        // GET: Edital/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Edital/Edit/5
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

        // GET: Edital/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Edital/Delete/5
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