using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.EF.App;
using Helpers.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Public.DTO.Mappers;

namespace WebApp.ApiControllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MilitaryPlansController : ControllerBase
    {
        private readonly ApplicationDbContext _context;   // not needed in the future
        
        private readonly IAppBLL _bll;
        private readonly MilitaryPlanMapper _mapper;

        public MilitaryPlansController(ApplicationDbContext context, IAppBLL bll, IMapper autoMapper)
        {
            _context = context;   // here too
            _bll = bll;
            _mapper = new MilitaryPlanMapper(autoMapper);
        }

        // GET: api/MilitaryPlans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.MilitaryPlan>>> GetMilitaryPlans()
        {
            // Only returning military plans to that specific user.
            var data = await 
                _bll.MilitaryPlanService.AllWithPlansCountAsync(User.GetUserId());

            var result = data
                .Select(e => _mapper.MapWithCount(e))
                .ToList();
            
            return result;
        }

        // GET: api/MilitaryPlans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.MilitaryPlan>> GetMilitaryPlan(Guid id)
        {
            var militaryPlan = await _bll.MilitaryPlanService.FindAsync(id, User.GetUserId());

            if (militaryPlan == null)
            {
                return NotFound();
            }

            var result = _mapper.Map(militaryPlan);

            return result;
        }

        // PUT: api/MilitaryPlans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMilitaryPlan(Guid id, Public.DTO.v1.MilitaryPlan militaryPlan)
        {
            if (id != militaryPlan.Id)
            {
                return BadRequest();
            }
            
            // Peaks siin olema, tuleb üle vaadata
            
            // if (!await _bll.MilitaryPlanService.IsOwnedByUserAsync(militaryPlan.Id, User.GetUserId()))
            // {
            //     return BadRequest("No hacking (bad user id)!");
            // }

            var bllMilitaryPlan = _mapper.Map(militaryPlan);
            bllMilitaryPlan!.AppUserId = User.GetUserId();
            _bll.MilitaryPlanService.Update(bllMilitaryPlan);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/MilitaryPlans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Public.DTO.v1.MilitaryPlan>> PostMilitaryPlan(Public.DTO.v1.MilitaryPlan militaryPlan)
        {
            var bllMilitaryPlan = _mapper.Map(militaryPlan);

            bllMilitaryPlan!.AppUserId = User.GetUserId();
            _bll.MilitaryPlanService.Add(bllMilitaryPlan);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetMilitaryPlan", new { id = militaryPlan.Id }, militaryPlan);
        }

        // DELETE: api/MilitaryPlans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMilitaryPlan(Guid id)
        {
            var militaryPlan = await _context.MilitaryPlans.FindAsync(id);
            if (militaryPlan == null)
            {
                return NotFound();
            }

            _context.MilitaryPlans.Remove(militaryPlan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MilitaryPlanExists(Guid id)
        {
            return _context.MilitaryPlans.Any(e => e.Id == id);
        }
    }
}
