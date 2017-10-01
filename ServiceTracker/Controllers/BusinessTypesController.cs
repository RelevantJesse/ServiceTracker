﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceTracker.Data;
using ServiceTracker.Models;

namespace ServiceTracker.Controllers
{
    public class BusinessTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BusinessTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BusinessTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.BusinessTypes.ToListAsync());
        }

        // GET: BusinessTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessType = await _context.BusinessTypes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (businessType == null)
            {
                return NotFound();
            }

            return View(businessType);
        }

        // GET: BusinessTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BusinessTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] BusinessType businessType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(businessType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(businessType);
        }

        // GET: BusinessTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessType = await _context.BusinessTypes.SingleOrDefaultAsync(m => m.Id == id);
            if (businessType == null)
            {
                return NotFound();
            }
            return View(businessType);
        }

        // POST: BusinessTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] BusinessType businessType)
        {
            if (id != businessType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(businessType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusinessTypeExists(businessType.Id))
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
            return View(businessType);
        }

        // GET: BusinessTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessType = await _context.BusinessTypes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (businessType == null)
            {
                return NotFound();
            }

            return View(businessType);
        }

        // POST: BusinessTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var businessType = await _context.BusinessTypes.SingleOrDefaultAsync(m => m.Id == id);
            _context.BusinessTypes.Remove(businessType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusinessTypeExists(int id)
        {
            return _context.BusinessTypes.Any(e => e.Id == id);
        }
    }
}
