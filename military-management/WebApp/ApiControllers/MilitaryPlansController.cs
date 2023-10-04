using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Contracts.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.EF.App;
using Domain.App;
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
        
        private readonly IAppUOW _uow;
        private readonly MilitaryPlanMapper _mapper;

        public MilitaryPlansController(ApplicationDbContext context, IAppUOW uow, IMapper autoMapper)
        {
            _context = context;   // here too
            _uow = uow;
            _mapper = new MilitaryPlanMapper(autoMapper);
        }

        // GET: api/MilitaryPlans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Public.DTO.v1.MilitaryPlan>>> GetMilitaryPlans()
        {
            // Only returning military plans to that specific user.
            var data = await 
                _uow.MilitaryPlanRepository.AllWithPlansCountAsync(User.GetUserId());

            var result = data
                .Select(e => _mapper.MapWithCount(e))
                .ToList();
            
            return result;
        }

        // GET: api/MilitaryPlans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Public.DTO.v1.MilitaryPlan>> GetMilitaryPlan(Guid id)
        {
            var militaryPlan = await _uow.MilitaryPlanRepository.FindAsync(id, User.GetUserId());

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
        public async Task<IActionResult> PutMilitaryPlan(Guid id, MilitaryPlan militaryPlan)
        {
            if (id != militaryPlan.Id)
            {
                return BadRequest();
            }

            _context.Entry(militaryPlan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MilitaryPlanExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MilitaryPlans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MilitaryPlan>> PostMilitaryPlan(MilitaryPlan militaryPlan)
        {
            _context.MilitaryPlans.Add(militaryPlan);
            _uow.MilitaryPlanRepository.Add(militaryPlan);
                
            await _uow.SaveChangesAsync();

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
