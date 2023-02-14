using BookShopping.Application.Models.Core;
using BookShopping.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopping.Application.Interfaces
{
    public interface IFeatureService
    {
        Task<ServiceResult<List<FeatureDto>>> GetFeatures();
    }
}
