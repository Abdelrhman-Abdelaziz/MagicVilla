using AutoMapper;
using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Models.DTOs;
using MagicVilla_VillaApi.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_VillaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private APIResponse _response;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<VillaController> _logger;
        private readonly IMapper _mapper;

        public VillaController(IUnitOfWork unitOfWork, ILogger<VillaController> logger, IMapper _mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            this._mapper = _mapper;
            this._response = new();
        }

        // GET: api/Villa
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillas()
        {
            var vallas = await _unitOfWork.Villas.GetAllAsync();
            var vaillasDTO = _mapper.Map<List<VillaDTO>>(vallas);

            _response.Result = vaillasDTO;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        // GET: api/Villa/5
        [HttpGet("{id:int}")]
        [Authorize(Roles ="admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVilla(int id)
        {

            var villa = await _unitOfWork.Villas.FindAsync(id);

            if (villa == null)
            {
                return NotFound();
            }

            var villaDTO = _mapper.Map<VillaDTO>(villa);

            _response.Result = villaDTO;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        // PUT: api/Villa/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, VillaUpdateDTO updateDTO)
        {
            var villaExist = await _unitOfWork.Villas.ContainsAsync(id);
            if (!villaExist)
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

            _response.StatusCode = HttpStatusCode.NoContent;

            return _response;
        }

        // POST: api/Villa
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CreateVilla(VillaCreateDTO createDTO)
        {

            var villa = _mapper.Map<Villa>(createDTO);
            await _unitOfWork.Villas.AddAsync(villa);
            await _unitOfWork.SaveAsync();

            _response.Result = _mapper.Map<VillaDTO>(villa);
            _response.StatusCode = HttpStatusCode.Created;
            _response.IsSuccess = true;

            return CreatedAtAction("GetVilla", new { id = villa.Id }, _response);
        }

        // DELETE: api/Villa/5
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {

            var villa = await _unitOfWork.Villas.FindAsync(id);
            if (villa == null)
            {
                return NotFound();
            }

            _unitOfWork.Villas.Remove(villa);
            await _unitOfWork.SaveAsync();

            _response.StatusCode = HttpStatusCode.NoContent;
            return _response;
        }

    }
}
