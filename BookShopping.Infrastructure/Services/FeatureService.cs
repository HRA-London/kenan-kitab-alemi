using BookShopping.Application.Interfaces;
using BookShopping.Domain.DTOs;
using BookShopping.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopping.Infrastructure.Services
{
    public class FeatureService : IFeatureService
    {
        private readonly ApplicationDbContext _context;

        public FeatureService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<FeatureDto> GetFeatures()
        {
            var features = _context.Features
                                .Where(c=>c.IsActive == true)
                                .Select(c => new FeatureDto
                                 {
                                     FeatureId = c.Id,
                                     Description = c.ShortDesc,
                                     Name = c.Name,
                                     Icon = c.Icon
                                 })
                                .ToList();

            return features;
        }
    }
}
