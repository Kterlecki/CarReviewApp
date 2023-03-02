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

        public CarController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Car>))]
        public IActionResult GetCars()
        {
            var cars = _carRepository.GetCars();

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

            var car = _carRepository.GetCar(id);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(car);
        }
    } 
}
