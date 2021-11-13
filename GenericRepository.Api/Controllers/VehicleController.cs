using AutoMapper;
using GenericRepository.Api.Models;
using GenericRepository.Dal;
using GenericRepository.Dal.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GenericRepository.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;

        public VehicleController(IVehicleRepository vehicleRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }

        // GET api/vehicle
        [HttpGet]
        public async Task<IActionResult> Get()
        => Ok(_mapper.Map<IEnumerable<VehicleDto>>(await _vehicleRepository.AllAsync()));

        // GET api/vehicle/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var foundVehicle = await _vehicleRepository.GetByIdAsync(id);
            return foundVehicle != null ? Ok(_mapper.Map<VehicleDto>(foundVehicle))
                                        : NotFound("Vehicle doesn't exists");
        }

        // POST api/vehicle
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] VehicleDto vehicle)
        {
            var savedVehicle = await _vehicleRepository.CreateAsync(_mapper.Map<Vehicle>(vehicle));
            return Ok(_mapper.Map<VehicleDto>(savedVehicle));
        }

        // PUT api/vehicle/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] VehicleDto vehicle)
        {
            if (id != vehicle.Id)
                return BadRequest("Ids don't match");

            try
            {
                var updatedVehicle = await _vehicleRepository.UpdateAsync(_mapper.Map<Vehicle>(vehicle));
                return Ok(_mapper.Map<VehicleDto>(updatedVehicle));
            }
            catch (Exception e) when (e is ArgumentNullException
                                      || e is DbUpdateConcurrencyException)
            {
                return NotFound("Vehicle doesn't exists");
            }
        }

        // DELETE api/vehicle/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                return Ok(await _vehicleRepository.DeleteAsync(id));
            }
            catch (Exception e) when (e is ArgumentNullException
                                      || e is DbUpdateConcurrencyException)
            {
                return NotFound("Vehicle doesn't exists");
            }
        }
    }

}
