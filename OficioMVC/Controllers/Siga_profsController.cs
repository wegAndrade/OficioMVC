using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OficioMVC.Models;

namespace OficioMVC.Controllers
{
    public class Siga_profsController : Controller
    {
        private readonly OficioMVCContext _context;

        public Siga_profsController(OficioMVCContext context)
        {
            _context = context;
        }

        // GET: Siga_profs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Siga_profs.ToListAsync());
        }

        // GET: Siga_profs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siga_profs = await _context.Siga_profs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (siga_profs == null)
            {
                return NotFound();
            }

            return View(siga_profs);
        }

        // GET: Siga_profs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Siga_profs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,user_login,user_pass,user_nicename,ativo,dpto")] Siga_profs siga_profs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(siga_profs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(siga_profs);
        }

        // GET: Siga_profs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siga_profs = await _context.Siga_profs.FindAsync(id);
            if (siga_profs == null)
            {
                return NotFound();
            }
            return View(siga_profs);
        }

        // POST: Siga_profs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,user_login,user_pass,user_nicename,ativo,dpto")] Siga_profs siga_profs)
        {
            if (id != siga_profs.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(siga_profs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Siga_profsExists(siga_profs.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(siga_profs);
        }

        // GET: Siga_profs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siga_profs = await _context.Siga_profs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (siga_profs == null)
            {
                return NotFound();
            }

            return View(siga_profs);
        }

        // POST: Siga_profs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var siga_profs = await _context.Siga_profs.FindAsync(id);
            _context.Siga_profs.Remove(siga_profs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Siga_profsExists(int id)
        {
            return _context.Siga_profs.Any(e => e.ID == id);
        }

        public async Task<IActionResult> Verify(String user_login)
        {
            if (user_login == null)
            {
                return NotFound();
            }
            

            var siga_profs = await _context.Siga_profs
                .FirstOrDefaultAsync(m => m.user_login == user_login);
            if (siga_profs == null)
            {
                return NotFound();
            }

            return View(siga_profs);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Veifys(string name, string pass)
        {

                if (name == null)
                {
                    return NotFound();
                }


                var siga_profs_valid = await _context.Siga_profs
                    .FirstOrDefaultAsync(m => m.user_login == name);
                if (siga_profs_valid == null)
                {
                    return NotFound();
                }

                return View(siga_profs_valid);
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Verifys ()
        {
            return View();
        }

    }
}
