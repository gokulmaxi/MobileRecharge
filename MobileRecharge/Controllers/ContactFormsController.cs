using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MobileRecharge.Areas.Identity.Data;
using MobileRecharge.Models;

namespace MobileRecharge.Controllers
{
    public class ContactFormsController : Controller
    {
        private readonly MobileRechargeContext _context;
        private readonly UserManager<MobileRechargeUser> _userManager;

        public ContactFormsController(MobileRechargeContext context, UserManager<MobileRechargeUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ContactForms
        public async Task<IActionResult> Index()
        {
            var currUser =await _userManager.GetUserAsync(this.User);
            if(User.IsInRole("Admin"))
            return _context.ContactForm != null ?
                        View(await _context.ContactForm.ToListAsync()) :
                        Problem("Entity set 'MobileRechargeContext.ContactForm'  is null.");
            return _context.ContactForm != null ?
                        View(await _context.ContactForm.
                        Where(x=> x.User == currUser)
                        .ToListAsync()) :
                        Problem("Entity set 'MobileRechargeContext.ContactForm'  is null.");
        }

        // GET: ContactForms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ContactForm == null)
            {
                return NotFound();
            }

            var contactForm = await _context.ContactForm
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (contactForm == null)
            {
                return NotFound();
            }

            return View(contactForm);
        }

        // GET: ContactForms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactForms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactId,Subject,Message,Status,AdminComment")] ContactForm contactForm)
        {
            var CurrentUser = await _userManager.GetUserAsync(this.User);
            contactForm.User = CurrentUser;
            _context.Add(contactForm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: ContactForms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ContactForm == null)
            {
                return NotFound();
            }

            var contactForm = await _context.ContactForm.FindAsync(id);
            if (contactForm == null)
            {
                return NotFound();
            }
            return View(contactForm);
        }

        // POST: ContactForms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContactId,Subject,Message,Status,AdminComment")] ContactForm contactForm)
        {
            if (id != contactForm.ContactId)
            {
                return NotFound();
            }

            try
            {
                _context.Update(contactForm);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactFormExists(contactForm.ContactId))
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

        // GET: ContactForms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ContactForm == null)
            {
                return NotFound();
            }

            var contactForm = await _context.ContactForm
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (contactForm == null)
            {
                return NotFound();
            }

            return View(contactForm);
        }

        // POST: ContactForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ContactForm == null)
            {
                return Problem("Entity set 'MobileRechargeContext.ContactForm'  is null.");
            }
            var contactForm = await _context.ContactForm.FindAsync(id);
            if (contactForm != null)
            {
                _context.ContactForm.Remove(contactForm);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactFormExists(int id)
        {
            return (_context.ContactForm?.Any(e => e.ContactId == id)).GetValueOrDefault();
        }
    }
}
