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
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public CarController(ICarRepository carRepository,
            IReviewRepository reviewerRepository,
            IMapper mapper)
        {
            _carRepository = carRepository;
            _reviewRepository = reviewerRepository;
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
            var car = _carRepository.GetCarTrimToUpper(carCreate);

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

        [HttpPut("{carId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCar(int carId,
            [FromQuery] int catId,
            [FromQuery] int ownerId,
            [FromBody] CarDto updateCar)
        {
            if (updateCar == null)
            {
                return BadRequest(ModelState);
            }
            if (carId != updateCar.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_carRepository.CarExists(carId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var carMap = _mapper.Map<Car>(updateCar);
            if (!_carRepository.UpdateCar(ownerId, catId, carMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Car");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{carId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCar(int carId)
        {
            if (!_carRepository.CarExists(carId))
            {
                return NotFound();
            }

            var reviewsToDelete = _reviewRepository.GetReviewsOfACar(carId);
            var carToDelete = _carRepository.GetCar(carId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_reviewRepository.DeleteReviews(reviewsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong with Delete of Reviewer");
            }
            if (!_carRepository.DeleteCar(carToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting Car");
            }

            return NoContent();
        }
    }
}
