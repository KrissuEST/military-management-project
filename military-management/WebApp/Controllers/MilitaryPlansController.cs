using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DAL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.EF.App;
using DAL.EF.App.Repositories;
using Domain.App;
using Domain.App.Identity;
using Helpers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Controllers
{
    [Authorize]   // Only logged in user can do anything on the page.
    public class MilitaryPlansController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUOW _uow;

        public MilitaryPlansController(
            UserManager<AppUser> userManager, 
            IAppUOW uow)
        {
            _userManager = userManager;
            _uow = uow;
        }

        // All CRUD methods here
        // GET: MilitaryPlans
        public async Task<IActionResult> Index()
        {
            var vm = await 
                _uow.MilitaryPlanRepository.AllAsync(User.GetUserId());
            return View(vm);
        }

        // GET: MilitaryPlans/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var militaryPlan = await _uow.MilitaryPlanRepository.FindAsync(id.Value);
            
            if (militaryPlan == null)
            {
                return NotFound();
            }

            return View(militaryPlan);
        }

        // GET: MilitaryPlans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MilitaryPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MilitaryPlan militaryPlan)
        {
            militaryPlan.AppUserId = User.GetUserId();
            
            if (ModelState.IsValid)
            {
                _uow.MilitaryPlanRepository.Add(militaryPlan);   //database will take care of it
                
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            return View(militaryPlan);
        }

        // GET: MilitaryPlans/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var militaryPlan = await _uow.MilitaryPlanRepository.FindAsync(id.Value);
            if (militaryPlan == null)
            {
                return NotFound();
            }
            
            return View(militaryPlan);
        }

        // POST: MilitaryPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, MilitaryPlan militaryPlan)
        {
            // TODO: check the ownership before edit!!
            if (id != militaryPlan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                militaryPlan.AppUserId = User.GetUserId();
                
                _uow.MilitaryPlanRepository.Update(militaryPlan);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            return View(militaryPlan);
        }

        // GET: MilitaryPlans/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var militaryPlan = await _uow.MilitaryPlanRepository.FindAsync(id.Value);
            
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
            await _uow.MilitaryPlanRepository.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            
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
