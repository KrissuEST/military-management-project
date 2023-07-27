using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.EF.App;
using DAL.EF.App.Repositories;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Controllers
{
    public class MilitaryPlansController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly MilitaryPlanRepository _repository;
        private readonly UserManager<AppUser> _userManager;

        public MilitaryPlansController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _repository = new MilitaryPlanRepository(_context);
        }

        // All CRUD methods here
        // GET: MilitaryPlans
        public async Task<IActionResult> Index()
        {
            var vm = await _repository.AllAsync();
            return View(vm);
        }

        // GET: MilitaryPlans/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var militaryPlan = await _repository.FindAsync(id.Value);
            
            if (militaryPlan == null)
            {
                return NotFound();
            }

            return View(militaryPlan);
        }

        // GET: MilitaryPlans/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = GetUserSelectList();
            return View();
        }

        // POST: MilitaryPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MilitaryPlan militaryPlan)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(militaryPlan);   //database will take care of it
                
                //TODO??? where should this be 
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = GetUserSelectList(militaryPlan.AppUserId);
            
            return View(militaryPlan);
        }

        // GET: MilitaryPlans/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var militaryPlan = await _repository.FindAsync(id.Value);
            if (militaryPlan == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = GetUserSelectList(militaryPlan.AppUserId);
            
            return View(militaryPlan);
        }

        // POST: MilitaryPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, MilitaryPlan militaryPlan)
        {
            if (id != militaryPlan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _repository.Update(militaryPlan);
                //TODO???
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = GetUserSelectList(militaryPlan.AppUserId);
            
            return View(militaryPlan);
        }

        // GET: MilitaryPlans/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var militaryPlan = await _repository.FindAsync(id.Value);
            
            if (militaryPlan == null)
            {
                return NotFound();
            }

            return View(militaryPlan);
        }

        // POST: MilitaryPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _repository.RemoveAsync(id);
            //TODO???
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        private SelectList GetUserSelectList(Guid? selectedUserId = null)  //default value null
        {
            if (selectedUserId == null)
            {
                return new SelectList(
                    _userManager.Users,
                    nameof(AppUser.Id),
                    nameof(AppUser.Email)
                );
            }

            return new SelectList(
                _userManager.Users,
                nameof(AppUser.Id),
                nameof(AppUser.Email),
                selectedUserId);
        }

    }
}
