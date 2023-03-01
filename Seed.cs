using CarReviewApp.Data;
using CarReviewApp.Models;

namespace CarReviewApp
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.CarOwners.Any())
            {
                var carOwners = new List<CarOwner>()
                {
                    new CarOwner()
                    {
                        Car = new Car()
                        {
                            Make = "BMW",
                            Model = "3-series",
                            YearBuilt = 2000,
                            CarCategories = new List<CarCategory>()
                            {
                                new CarCategory { Category = new Category() { Name = "M sport"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="BmW",Description = "Pretty good", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title="Audi", Description = "good", Rating = 4,
                                Reviewer = new Reviewer(){ FirstName = "John", LastName = "Smith" } },
                                new Review { Title="ImagineCar",Description = "awesome, NOt", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "Joe" } },
                            }
                        },
                        Owner = new Owner()
                        {
                            Name = "Jack",
                            Surname= "London",

                            Country = new Country()
                            {
                                Name = "Germany"
                            }
                        }
                    },
                    new CarOwner()
                    {
                        Car = new Car()
                        {
                            Make = "BMW",
                            Model = "5-series",
                            YearBuilt = 2010,
                            CarCategories = new List<CarCategory>()
                            {
                                new CarCategory { Category = new Category() { Name = "M sport"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="BmW",Description = "Pretty good good", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title="Audi", Description = "good good", Rating = 4,
                                Reviewer = new Reviewer(){ FirstName = "John", LastName = "Smith" } },
                                new Review { Title="ImagineCar",Description = "awesome, NOt good", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "Joe" } },
                            }
                        },
                        Owner = new Owner()
                        {
                            Name = "Jack",
                            Surname= "London",

                            Country = new Country()
                            {
                                Name = "Germany good"
                            }
                        }
                    },
                    new CarOwner()
                    {
                        Car = new Car()
                        {
                            Make = "BMW",
                            Model = "6-series",
                            YearBuilt = 2020,
                            CarCategories = new List<CarCategory>()
                            {
                                new CarCategory { Category = new Category() { Name = "M sport"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="BmW",Description = "Pretty good goodgood", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title="Audi", Description = "good good good", Rating = 4,
                                Reviewer = new Reviewer(){ FirstName = "John", LastName = "Smith" } },
                                new Review { Title="ImagineCar",Description = "awesome, NOt good good", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "Joe" } },
                            }
                        },
                        Owner = new Owner()
                        {
                            Name = "Jack",
                            Surname= "London",

                            Country = new Country()
                            {
                                Name = "Germany"
                            }
                        }
                    }
                    };
                dataContext.CarOwners.AddRange(carOwners);
                dataContext.SaveChanges();
            }
        }
    }
}
