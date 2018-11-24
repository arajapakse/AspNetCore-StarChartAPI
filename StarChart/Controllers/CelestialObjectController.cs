using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;

namespace StarChart.Controllers
{
    [Route("")]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var celestialObject = _context.CelestialObjects.SingleOrDefault(c => c.Id == id);

            if (celestialObject == null)
            {
                return NotFound();
            }

            celestialObject.OrbitedObjectId = _context.CelestialObjects.SingleOrDefault(o =>
                o.OrbitedObjectId != null && o.OrbitedObjectId == celestialObject.Id)?.OrbitedObjectId;

            return Ok(celestialObject);
        }

        [HttpGet("name")]
        public IActionResult GetByName(string name)
        {
            var celestialObject = _context.CelestialObjects.SingleOrDefault(c => c.Name == name);

            if (celestialObject == null)
            {
                return NotFound();
            }

            celestialObject.OrbitedObjectId = _context.CelestialObjects.SingleOrDefault(o =>
                o.OrbitedObjectId != null && o.OrbitedObjectId == celestialObject.Id)?.OrbitedObjectId;

            return Ok(celestialObject);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var celestialObjects = _context.CelestialObjects.ToList();

            foreach (var celestialObject in celestialObjects)
            {
                celestialObject.OrbitedObjectId = _context.CelestialObjects.SingleOrDefault(o =>
                    o.OrbitedObjectId != null && o.OrbitedObjectId == celestialObject.Id)?.OrbitedObjectId;
            }

            return Ok(celestialObjects);
        }
    }
}
