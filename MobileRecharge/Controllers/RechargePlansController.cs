using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MobileRecharge.Areas.Identity.Data;
using MobileRecharge.Models;

namespace MobileRecharge.Controllers
{
    public class RechargePlansController : Controller
    {
        private readonly MobileRechargeContext _context;

        public RechargePlansController(MobileRechargeContext context)
        {
            _context = context;
        }

        // GET: RechargePlans
        public async Task<IActionResult> Index()
        {
              return _context.RechargePlans != null ? 
                          View(await _context.RechargePlans.ToListAsync()) :
                          Problem("Entity set 'MobileRechargeContext.RechargePlans'  is null.");
        }

        // GET: RechargePlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RechargePlans == null)
            {
                return NotFound();
            }

            var rechargePlans = await _context.RechargePlans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rechargePlans == null)
            {
                return NotFound();
            }

            return View(rechargePlans);
        }

        // GET: RechargePlans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RechargePlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlanName,Price,DataBenfits,CallBenifits,OtherBenifits,Validity,PlanType")] RechargePlans rechargePlans)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rechargePlans);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rechargePlans);
        }

        // GET: RechargePlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RechargePlans == null)
            {
                return NotFound();
            }

            var rechargePlans = await _context.RechargePlans.FindAsync(id);
            if (rechargePlans == null)
            {
                return NotFound();
            }
            return View(rechargePlans);
        }

        // POST: RechargePlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlanName,Price,DataBenfits,CallBenifits,OtherBenifits,Validity,PlanType")] RechargePlans rechargePlans)
        {
            if (id != rechargePlans.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rechargePlans);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RechargePlansExists(rechargePlans.Id))
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
            return View(rechargePlans);
        }

        // GET: RechargePlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RechargePlans == null)
            {
                return NotFound();
            }

            var rechargePlans = await _context.RechargePlans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rechargePlans == null)
            {
                return NotFound();
            }

            return View(rechargePlans);
        }

        // POST: RechargePlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RechargePlans == null)
            {
                return Problem("Entity set 'MobileRechargeContext.RechargePlans'  is null.");
            }
            var rechargePlans = await _context.RechargePlans.FindAsync(id);
            if (rechargePlans != null)
            {
                _context.RechargePlans.Remove(rechargePlans);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RechargePlansExists(int id)
        {
          return (_context.RechargePlans?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
