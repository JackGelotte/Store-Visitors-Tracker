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

        // Post every "sensor"-call made, with section and timestamp
        [HttpPost("[Action]")]
        public async Task<ActionResult<SensorLog>> Log(string input)
        {
            // The post-request simulates a sensor-trigger and the paramater "input" consists of which section was entered and which section was exited, separated by ".".
            // Example: "Electronics.Kitchen" says a visitor entered the section "Electronics" and exited the section "Kitchen".
            var enterName = input.Split(".")[0];
            var exitName = input.Split(".")[1];

            var enterSection = await _context.StoreSections
                .FirstOrDefaultAsync(ss => ss.Name == enterName);
            if (enterSection == default)
                return BadRequest(new { Error = $"Section '{enterSection}' does not exists" });
            var exitSection = await _context.StoreSections
                .FirstOrDefaultAsync(ss => ss.Name == exitName);
            if (exitSection == default)
                return BadRequest(new { Error = $"Section '{exitSection}' does not exists" });

            enterSection.VisitorCount++;
            exitSection.VisitorCount--;

            var log = new SensorLog()
            {
                TimeStamp = DateTime.Now,
                EnterStoreSection = enterSection,
                ExitStoreSection = exitSection,
                Direction = Direction.Enter
            };
            await _context.AddAsync(log);
            await _context.SaveChangesAsync();

            string warning = "";
            if (exitSection.VisitorCount < 0)
            {
                warning = $"Warning, section '{exitSection.Name}' has less then 0 visitors. Something is wrong with the sensors, not the code.";
            }

            return Created(Request.HttpContext.Request.Path, new
            {
                Log = $"Enter : {enterSection.Name}, Exit : {exitSection.Name}, Time Stamp : {DateTime.Now}",
                Warning = String.IsNullOrEmpty(warning) ? "No warnings, all good" : warning
            });
        }

        // Get all sections, and visitor count in each.
        [HttpGet("[Action]")]
        public async Task<ActionResult> GetAllSections()
        {
            var sections = await _context.StoreSections
                .Include(e => e.EnterSensorLog)
                .Include(x => x.ExitSensorLog)
                .Select(s => new { Name = s.Name, Visitors = CurrentSectionCount(s.EnterSensorLog, s.ExitSensorLog) })
                .ToListAsync();
            if (sections.Count < 1) 
                return NoContent();
            return Ok(sections);
        }


        // Reset the visitor count in specified section
        [HttpPut("[action]")]
        public async Task<ActionResult> ResetSection(string targetSection)
        {
            var section = await _context.StoreSections
                .Where(e => e.Name == targetSection)
                .FirstOrDefaultAsync();
            if (section == default) 
                return NoContent();
            section.VisitorCount = 0;
            await _context.SaveChangesAsync();
            return Ok();
        }

        private static int CurrentSectionCount(IEnumerable<SensorLog> enter, IEnumerable<SensorLog> exit) 
            => enter.Count() - exit.Count();
    }
}
