using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MobileRecharge.Areas.Identity.Data;
using MobileRecharge.Models;

namespace MobileRecharge.Controllers
{
    [Authorize]
    public class RechargeReportsController : Controller
    {
        private readonly MobileRechargeContext _context;
        private readonly UserManager<MobileRechargeUser> _userManager;

        public RechargeReportsController(MobileRechargeContext context,UserManager<MobileRechargeUser> userManager)
        {
            _context = context;
            _userManager  = userManager;
        }

        // GET: RechargeReports
        public async Task<IActionResult> Index()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var user = await _userManager.GetUserAsync(User);
            return _context.RechargeReport != null ?
                        View(await _context.RechargeReport
                        .Where(x =>x.User==user)
                        .Include(x=> x.Plan)
                        .ToListAsync()) :
                        Problem("Entity set 'MobileRechargeContext.RechargeReport'  is null.");
        }

        // GET: RechargePlans
        public async Task<IActionResult> Plans()
        {
            return _context.RechargePlans != null ?
                        View(await _context.RechargePlans.ToListAsync()) :
                        Problem("Entity set 'MobileRechargeContext.RechargePlans'  is null.");
        }
        // GET: RechargeReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RechargeReport == null)
            {
                return NotFound();
            }

            var rechargeReport = await _context.RechargeReport
                .FirstOrDefaultAsync(m => m.RechargeId == id);
            if (rechargeReport == null)
            {
                return NotFound();
            }

            return View(rechargeReport);
        }

        // GET: RechargeReports/Create
        public async Task<IActionResult> Create(int? id)
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

        // POST: RechargeReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("RechargeId,RechargedDate")] RechargeReport rechargeReport)
        {
            RechargeReport newRecharge = new();
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var user = await _userManager.GetUserAsync(User);
            var plan = await _context.RechargePlans.Where(x=> x.Id == id).FirstOrDefaultAsync();
            newRecharge.Plan = plan;
            newRecharge.User = user;
            newRecharge.RechargedDate = DateTime.Now;
            _context.RechargeReport.Add(newRecharge);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: RechargeReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RechargeReport == null)
            {
                return NotFound();
            }

            var rechargeReport = await _context.RechargeReport.FindAsync(id);
            if (rechargeReport == null)
            {
                return NotFound();
            }
            return View(rechargeReport);
        }

        // POST: RechargeReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RechargeId,RechargedDate")] RechargeReport rechargeReport)
        {
            if (id != rechargeReport.RechargeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rechargeReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RechargeReportExists(rechargeReport.RechargeId))
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
            return View(rechargeReport);
        }

        // GET: RechargeReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RechargeReport == null)
            {
                return NotFound();
            }

            var rechargeReport = await _context.RechargeReport
                .FirstOrDefaultAsync(m => m.RechargeId == id);
            if (rechargeReport == null)
            {
                return NotFound();
            }

            return View(rechargeReport);
        }

        // POST: RechargeReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RechargeReport == null)
            {
                return Problem("Entity set 'MobileRechargeContext.RechargeReport'  is null.");
            }
            var rechargeReport = await _context.RechargeReport.FindAsync(id);
            if (rechargeReport != null)
            {
                _context.RechargeReport.Remove(rechargeReport);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RechargeReportExists(int id)
        {
            return (_context.RechargeReport?.Any(e => e.RechargeId == id)).GetValueOrDefault();
        }
    }
}
