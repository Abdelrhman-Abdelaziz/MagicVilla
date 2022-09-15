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
using MagicVilla_VillaApi.Repository.IRepository;

namespace MagicVilla_VillaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<VillaController> _logger;
        private readonly IMapper _mapper;

        public VillaController(IUnitOfWork unitOfWork, ILogger<VillaController> logger, IMapper _mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            this._mapper = _mapper;
        }

        // GET: api/Villa
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            if (_unitOfWork.Villas == null)
            {
                return NotFound();
            }
            var vallas = await _unitOfWork.Villas.GetAllAsync();
            var vaillasDTO = _mapper.Map<List<VillaDTO>>(vallas);

            return Ok(vaillasDTO);
        }

        // GET: api/Villa/5
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDTO>> GetVilla(int id)
        {
            if (_unitOfWork.Villas == null)
            {
                return NotFound();
            }
            var villa = await _unitOfWork.Villas.GetByIdAsync(id);

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

        public async Task<IActionResult> UpdateVilla(int id, VillaUpdateDTO updateDTO)
        {
            if (!VillaExists(id))
            {
                return NotFound();
            }

            if (id != updateDTO.Id)
            {
                return BadRequest();
            }

            var villa = _mapper.Map<Villa>(updateDTO);
            _unitOfWork.Villas.Update(villa);

            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        // POST: api/Villa
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDTO>> CreateVilla(VillaCreateDTO createDTO)
        {
            if (_unitOfWork.Villas == null)
            {
                //return Problem("Entity set 'AppDbContext.VillaDTO'  is null.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Some Thing Went Wrong!");
            }

            var villa = _mapper.Map<Villa>(createDTO);
            await _unitOfWork.Villas.AddAsync(villa);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction("GetVilla", new { id = villa.Id }, villa);
        }

        // DELETE: api/Villa/5
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (_unitOfWork.Villas == null)
            {
                return NotFound();
            }
            var villa = await _unitOfWork.Villas.GetByIdAsync(id);
            if (villa == null)
            {
                return NotFound();
            }

            _unitOfWork.Villas.Remove(villa);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        private bool VillaExists(int id)
        {
            var villa = _unitOfWork.Villas.GetAsync(v => v.Id == id, false).Result;

            return villa != null;
        }
    }
}
