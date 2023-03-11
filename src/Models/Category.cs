﻿namespace CarReviewApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<CarCategory> CarCategories{ get; set; }
    }
}
