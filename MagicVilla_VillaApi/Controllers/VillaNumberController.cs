using AutoMapper;
using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Models.DTOs;
using MagicVilla_VillaApi.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_VillaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaNumberController : ControllerBase
    {
        private APIResponse _response;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<VillaController> _logger;
        private readonly IMapper _mapper;

        public VillaNumberController(IUnitOfWork unitOfWork, ILogger<VillaController> logger, IMapper _mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            this._mapper = _mapper;
            this._response = new();
        }

        // GET: api/Villa
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillaNumber()
        {
            var villaNumbers = await _unitOfWork.VillaNumbers.GetAllWithVillasAsync();
            var vaillasDTO = _mapper.Map<List<VillaNumberDTO>>(villaNumbers);

            _response.Result = vaillasDTO;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        // GET: api/Villa/5
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVillaNumber(int id)
        {

            var villaNumber = await _unitOfWork.VillaNumbers.FindAsync(id);

            if (villaNumber == null)
            {
                return NotFound();
            }

            var villaNumberDTO = _mapper.Map<VillaNumberDTO>(villaNumber);

            _response.Result = villaNumberDTO;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        // PUT: api/Villa/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int id, VillaNumberUpdateDTO villaNumbeUpdaterDTO)
        {
            if (!await _unitOfWork.VillaNumbers.ContainsAsync(id))
            {
                return NotFound();
            }

            if (id != villaNumbeUpdaterDTO.VillaNo)
            {
                return BadRequest();
            }

            var villaNumber = _mapper.Map<VillaNumber>(villaNumbeUpdaterDTO);
            _unitOfWork.VillaNumbers.Update(villaNumber);
            await _unitOfWork.SaveAsync();

            _response.StatusCode = HttpStatusCode.NoContent;

            return _response;
        }

        // POST: api/Villa
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CreateVillaNumber(VillaNumberCreateDTO villaNumbercreateDTO)
        {
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;

            bool villaNumberExist = await _unitOfWork.VillaNumbers.ContainsAsync(villaNumbercreateDTO.VillaNo);
            if (villaNumberExist)
            {
                _response.ErrorMessages = new List<string>() { "The Villa Number Already Exsit" };
                return BadRequest(_response);
            }

            bool villaExist = await _unitOfWork.Villas.ContainsAsync(villaNumbercreateDTO.VillaId);
            if (!villaExist)
            {
                _response.ErrorMessages = new List<string>() { "The Villa Dosen't Exsit" };
                return BadRequest(_response);
            }
            var villaNumber = _mapper.Map<VillaNumber>(villaNumbercreateDTO);
            await _unitOfWork.VillaNumbers.AddAsync(villaNumber);
            await _unitOfWork.SaveAsync();

            _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
            _response.StatusCode = HttpStatusCode.Created;
            _response.IsSuccess = true;

            return CreatedAtAction("GetVillaNumber", new { id = villaNumber.VillaNo }, _response);


        }

        // DELETE: api/Villa/5
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int id)
        {

            var villaNumber = await _unitOfWork.VillaNumbers.FindAsync(id);
            if (villaNumber == null)
            {
                return NotFound();
            }

            _unitOfWork.VillaNumbers.Remove(villaNumber);
            await _unitOfWork.SaveAsync();

            _response.StatusCode = HttpStatusCode.NoContent;
            return _response;
        }
    }
}
