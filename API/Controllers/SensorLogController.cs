using API.Data;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensorLogController : ControllerBase
    {
        private readonly ApiContext _context;
        public SensorLogController(ApiContext context)
        {
            _context = context;
        }

        // This does two logs. One "Enter"-log and one "Exit"-log.
        // This is to, without actual sensors, show that when entering a section, you will have exited another.
        // With the exception of the entrance of the store.
        [HttpPost("[Action]")]
        public async Task<ActionResult<SensorLog>> Log(string input)
        {
            var enterName = input.Split(".")[0];
            var exitName = input.Split(".")[1];
            var enterSection = await _context.StoreSections.FirstOrDefaultAsync(ss => ss.Name == enterName);
            if (enterSection == default) return BadRequest(new { Error = $"Section '{enterSection}' does not exists"});
            var exitSection = await _context.StoreSections.FirstOrDefaultAsync(ss => ss.Name == exitName);
            if (exitSection == default) return BadRequest(new { Error = $"Section '{exitSection}' does not exists" });

            var log = new SensorLog() { TimeStamp = DateTime.Now, EnterStoreSection = enterSection, ExitStoreSection=exitSection, Direction = Direction.Enter };
            await _context.AddAsync(log);
            await _context.SaveChangesAsync();

            return Created(Request.HttpContext.Request.Path, new {  Log  =  $"Enter : {enterSection.Name}, Exit : {exitSection.Name}, Time Stamp : {DateTime.Now}"  });
        }

        // TODO: Get all zones, and visitor count in each.
        [HttpGet("[Action]")]
        public async Task<ActionResult> GetAllSections()
        {
            var sections = _context.StoreSections.Include(e => e.EnterSensorLog).Include(x => x.ExitSensorLog)
                .Select(s => new { Name = s.Name, Visitors = CurrentSectionCount(s.EnterSensorLog, s.ExitSensorLog) });
            if (sections.Count() < 1) return NoContent();
                return Ok(sections);
        }

        private static int CurrentSectionCount(IEnumerable<SensorLog> enter, IEnumerable<SensorLog> exit) => enter.Count() - exit.Count();
    }
}
