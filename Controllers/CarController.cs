using AutoMapper;
using CarReviewApp.Dto;
using CarReviewApp.Interfaces;
using CarReviewApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public CarController(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Car>))]
        public IActionResult GetCars()
        {
            var cars = _mapper.Map<List<CarDto>>(_carRepository.GetCars());

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(cars);

        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Car))]
        [ProducesResponseType(400)]
        public IActionResult GetCar(int id)
        {
            if(!_carRepository.CarExists(id))
            {
                return NotFound();
            }

            var car = _mapper.Map<CarDto>(_carRepository.GetCar(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(car);
        }
        [HttpGet("{id}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetCarRating (int id)
        {
            if (!_carRepository.CarExists(id))
            {
                return NotFound();
            }
            var rating = _carRepository.GetCarRating(id);
            if (!ModelState.IsValid)
            {
                return BadRequest(rating);
            }
            return Ok(rating);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCar([FromQuery] int ownerId,[FromQuery] int catId, [FromBody] CarDto carCreate)
        {
            if (carCreate == null)
            {
                return BadRequest(ModelState);
            }
            var car = _carRepository.GetCars()
                .Where(c => c.Model.Trim().ToUpper() == carCreate.Model.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (car != null)
            {
                ModelState.AddModelError("", "Car already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var carMap = _mapper.Map<Car>(carCreate);

            if (!_carRepository.CreateCar(ownerId, catId, carMap))
            {
                ModelState.AddModelError("", "Save did not complete, Error");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");
        }

    } 
}
