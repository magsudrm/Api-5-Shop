﻿using Service.Dtos.ProductDtos;
using AutoMapper;
using Core.Entities;
using Core.Repositories;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Apps.AdminApi.Controllers
{
	[ApiExplorerSettings(GroupName = "admin_v1")]
	[Route("admin/api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IProductRepository _productRepository;
		private readonly IBrandRepository _brandRepository;

		public ProductsController(IMapper mapper,IProductRepository productRepository,IBrandRepository brandRepository)
		{
			_mapper = mapper;
			_productRepository = productRepository;
			_brandRepository = brandRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var data = await _productRepository.GetAllAsync("Brand");
			return Ok(data);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var data = await _productRepository.GetAsync(x=>x.Id == id,"Brand");
			if (data == null)
			{
				return NotFound();
			}
			return Ok(data);
		}
		[HttpPost("")]
		public async Task<IActionResult> Create(ProductDto dto)
		{
			if (!await _brandRepository.IsExistAsync(x => x.Id == dto.BrandId))
			{
				ModelState.AddModelError("BrandId", "BrandId not correct");
				return BadRequest(ModelState);
			}

			Product product = _mapper.Map<Product>(dto);
			await _productRepository.AddAsync(product);
			await _productRepository.SaveChangesAsync();
			return Ok(product);
		}

		[HttpPut("{id}")]

		public async Task<IActionResult> Edit(int id,ProductDto dto)
		{
			var existData = await _productRepository.GetAsync(x=>x.Id==id);
			if(existData==null)
				return NotFound();

			if (existData.BrandId != dto.BrandId && !await _brandRepository.IsExistAsync(x => x.Id ==dto.BrandId))
			{
				ModelState.AddModelError("BrandId", "BrandId not correct");
				return BadRequest(ModelState);
			}
			existData.Name = dto.Name;
			existData.CostPrice = dto.CostPrice;
			existData.SalePrice = dto.SalePrice;
			existData.DiscountPercent = dto.DiscountPercent;
			existData.BrandId=dto.BrandId;

			await _productRepository.SaveChangesAsync();
			return NoContent();
		}

	}
}
