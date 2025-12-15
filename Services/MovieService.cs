using Microsoft.EntityFrameworkCore;
using MyFirstApp.Models;
using MyFirstApp.Repositories;

namespace MyFirstApp.Services
{
    public class MovieService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MovieService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        
        public async Task<List<Movie>> GetActionMoviesInStock()
        {
            var movies = await _unitOfWork.Movies.GetActionMoviesInStockAsync();
            return movies.ToList();
        }

        
        public async Task<List<Movie>> GetMoviesOrderedByDateAndTitle()
        {
            var movies = await _unitOfWork.Movies.GetAllWithGenreAsync();
            return movies.OrderBy(m => m.DateAjoutMovie).ThenBy(m => m.Name).ToList();
        }

        
        public async Task<int> GetTotalMoviesCount()
        {
            return await _unitOfWork.Movies.CountAsync();
        }

        
        public async Task<List<Customer>> GetSubscribedCustomersWithHighDiscount()
        {
            var customers = await _unitOfWork.Customers.GetCustomersWithHighDiscountAsync(10);
            return customers.ToList();
        }

       
        public async Task<List<object>> GetMoviesWithGenres()
        {
            var movies = await _unitOfWork.Movies.GetAllWithGenreAsync();
            return movies.Select(m => new
            {
                MovieTitle = m.Name,
                GenreName = m.Genre?.Name
            }).ToList<object>();
        }

        
        public async Task<List<object>> GetTop3PopularGenres()
        {
            var genres = await _unitOfWork.Genres.GetTop3PopularGenresAsync();
            return genres.ToList();
        }
    }
}
