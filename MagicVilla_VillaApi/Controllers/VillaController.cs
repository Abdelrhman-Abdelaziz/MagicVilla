using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MagicVilla_VillaApi.DataAccess;
using MagicVilla_VillaApi.Models.DTOs;
using AutoMapper;
using MagicVilla_VillaApi.Models;

namespace MagicVilla_VillaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<VillaController> _logger;
        private readonly IMapper _mapper;

        public VillaController(AppDbContext context, ILogger<VillaController> logger, IMapper _mapper)
        {
            _context = context;
            _logger = logger;
            this._mapper = _mapper;
        }

        // GET: api/Villa
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            if (_context.Villas == null)
            {
                return NotFound();
            }
            var vallas = await _context.Villas.ToListAsync();
            var vaillasDTO = _mapper.Map<VillaDTO>(vallas);

            return Ok(vaillasDTO);
        }

        // GET: api/Villa/5
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDTO>> GetVilla(int id)
        {
            if (_context.Villas == null)
            {
                return NotFound();
            }
            var villa = await _context.Villas.FindAsync(id);

            if (villa == null)
            {
                return NotFound();
            }

            var villaDTO = _mapper.Map<VillaDTO>(villa);
            return Ok(villaDTO);
        }

        // PUT: api/Villa/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> UpdateVilla(int id, VillaUpdateDTO villaDTO)
        {
            if (id != villaDTO.Id)
            {
                return BadRequest();
            }
            var villa = _mapper.Map<Villa>(villaDTO);
            _context.Villas.Update(villa);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VillaExists(id))
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

        // POST: api/Villa
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDTO>> CreateVilla(VillaCreateDTO villaDTO)
        {
            if (_context.Villas == null)
            {
                //return Problem("Entity set 'AppDbContext.VillaDTO'  is null.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Some Thing Went Wrong!");
            }

            var villa = _mapper.Map<Villa>(villaDTO);
            _context.Villas.Add(villa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVilla", new { id = villa.Id }, villa);
        }

        // DELETE: api/Villa/5
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (_context.Villas == null)
            {
                return NotFound();
            }
            var villa = await _context.Villas.FindAsync(id);
            if (villa == null)
            {
                return NotFound();
            }

            _context.Villas.Remove(villa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VillaExists(int id)
        {
            return ( _context.Villas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
