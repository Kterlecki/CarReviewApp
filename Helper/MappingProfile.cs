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
            CreateMap<CategoryDto, Category>();
            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();
            CreateMap<Owner, OwnerDto>();
            CreateMap<OwnerDto, Owner>();
            CreateMap<Review, ReviewDto>();
            CreateMap<Reviewer, ReviewerDto>();
        }
    }
}
