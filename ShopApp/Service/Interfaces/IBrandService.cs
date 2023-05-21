using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Service.Dtos.BrandDtos;

namespace Service.Interfaces
{
    public interface IBrandService
    {
        public Task<BrandGetDto> GetById(int id);
        //public Task<BrandGetDto> GetAsync(Expression<Func<Brand,bool>> exp);
        public Task<BrandCreateResponseDto> CreateAsync(BrandDto brandDto);
        public Task<List<BrandGetAllItemDto>> GetAllAsync();
        public Task UpdateAsync(int id, BrandDto brandDto);
        public Task DeleteAsync(int id);
    }
}
