using AutoMapper;
using CarReviewApp.Dto;
using CarReviewApp.Interfaces;
using CarReviewApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(categories);

        }

        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int Id)
        {
            if (!_categoryRepository.CategoryExists(Id))
            {
                return NotFound();
            }

            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(Id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(category);
        }

        [HttpGet("car/{Id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Car>))]
        [ProducesResponseType(400)]
        public IActionResult GetCarByCategoryId(int Id)
        {
            var cars = _mapper.Map<List<Car>>(
                _categoryRepository.GetCarByCategory(Id));
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(cars);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
        {
            if(categoryCreate == null)
            {
                return BadRequest(ModelState);
            }
            var category = _categoryRepository.GetCategories()
                .Where(c => c.Name.Trim().ToUpper() == categoryCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
            if(category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryMap = _mapper.Map<Category>(categoryCreate);

            if(!_categoryRepository.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Save did not complete, Error");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");
        }
    }
}
