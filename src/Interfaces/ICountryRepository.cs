﻿using CarReviewApp.Dto;
using CarReviewApp.Models;

namespace CarReviewApp.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();
        Country GetCountry(int id);
        Country GetCountryByOwner(int ownerId);
        ICollection<Owner> GetOwnersFromACountry(int countryId);
        bool CountryExists(int id);
        bool CreateCountry(Country country);
        bool UpdateCountry(Country country);
        bool DeleteCountry(Country country);
        Country CountryGetTrimToUpper(CountryDto countryCreate);
        bool Save();
    }
}
