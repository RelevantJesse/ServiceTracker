using System;
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
    public class BusinessServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BusinessServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BusinessServices
        public async Task<IActionResult> Index()
        {
            var businessServices = _context.BusinessServices.Include(b => b.BusinessType).Include(b => b.Service);
            
            return View(await businessServices.ToListAsync());
        }

        // GET: BusinessServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessService = await _context.BusinessServices
                .Include(b => b.BusinessType)
                .Include(b => b.Service)
                .SingleOrDefaultAsync(m => m.BusinessTypeId == id);
            if (businessService == null)
            {
                return NotFound();
            }

            return View(businessService);
        }

        // GET: BusinessServices/Create
        public IActionResult Create()
        {
            ViewData["BusinessTypeList"] = new SelectList(_context.BusinessTypes, "Id", "Name");
            ViewData["ServiceList"] = new SelectList(_context.Services, "Id", "Name");
            return View();
        }

        // POST: BusinessServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BusinessTypeId,ServiceId")] BusinessService businessService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(businessService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BusinessTypeId"] = new SelectList(_context.BusinessTypes, "Id", "Id", businessService.BusinessTypeId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", businessService.ServiceId);
            return View(businessService);
        }

        // GET: BusinessServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessService = await _context.BusinessServices.SingleOrDefaultAsync(m => m.BusinessTypeId == id);
            if (businessService == null)
            {
                return NotFound();
            }
            ViewData["BusinessTypeId"] = new SelectList(_context.BusinessTypes, "Id", "Id", businessService.BusinessTypeId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", businessService.ServiceId);
            return View(businessService);
        }

        // POST: BusinessServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BusinessTypeId,ServiceId")] BusinessService businessService)
        {
            if (id != businessService.BusinessTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(businessService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusinessServiceExists(businessService.BusinessTypeId))
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
            ViewData["BusinessTypeId"] = new SelectList(_context.BusinessTypes, "Id", "Id", businessService.BusinessTypeId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Id", businessService.ServiceId);
            return View(businessService);
        }

        // GET: BusinessServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var businessService = await _context.BusinessServices
                .Include(b => b.BusinessType)
                .Include(b => b.Service)
                .SingleOrDefaultAsync(m => m.BusinessTypeId == id);
            if (businessService == null)
            {
                return NotFound();
            }

            return View(businessService);
        }

        // POST: BusinessServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var businessService = await _context.BusinessServices.SingleOrDefaultAsync(m => m.BusinessTypeId == id);
            _context.BusinessServices.Remove(businessService);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusinessServiceExists(int id)
        {
            return _context.BusinessServices.Any(e => e.BusinessTypeId == id);
        }
    }
}
