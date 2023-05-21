﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Repositories;
using Service.Dtos.BrandDtos;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Implementation
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }
        public async Task<BrandCreateResponseDto> CreateAsync(BrandDto brandDto)
        {
            if (await _brandRepository.IsExistAsync(x => x.Name == brandDto.Name))
                throw new EntityDublicateException("Name already taken");

            Brand brand = _mapper.Map<Brand>(brandDto);
            await _brandRepository.AddAsync(brand);
            await _brandRepository.SaveChangesAsync();
            return _mapper.Map<BrandCreateResponseDto>(brand);
        }

        public async Task DeleteAsync(int id)
        {
            Brand brand = await _brandRepository.GetAsync(x => x.Id == id);
            if (brand == null)
                throw new NotFoundException("Item not found");
            _brandRepository.Remove(brand);
            await _brandRepository.SaveChangesAsync();
        }

        public async Task<List<BrandGetAllItemDto>> GetAllAsync()
        {
            var data = await _brandRepository.GetAllAsync();
            List<BrandGetAllItemDto> items = _mapper.Map<List<BrandGetAllItemDto>>(data);
            return items;
        }

        public async Task<BrandGetDto> GetById(int id)
        {
            var data = await _brandRepository.GetAsync(x => x.Id == id, "Products");
            if (data == null) throw new NotFoundException("Item not found");
            BrandGetDto dto = _mapper.Map<BrandGetDto>(data);
            return dto;
        }

        public async Task UpdateAsync(int id, BrandDto brandDto)
        {
            var existData = await _brandRepository.GetAsync(x => x.Id == id);
            if (existData == null) throw new NotFoundException("Item not found");
            if (existData.Name != brandDto.Name && await _brandRepository.IsExistAsync(x => x.Name == brandDto.Name))
                throw new EntityDublicateException("Name already taken");
            existData.Name = brandDto.Name;
            await _brandRepository.SaveChangesAsync();
        }
    }
}
