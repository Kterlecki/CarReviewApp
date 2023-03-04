using AutoMapper;
using CarReviewApp.Dto;
using CarReviewApp.Models;

namespace CarReviewApp.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Car, CarDto>();
            CreateMap<Category, CategoryDto>();
        }
    }
}
