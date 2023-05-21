using Service.Dtos.BrandDtos;
using AutoMapper;
using Core.Entities;
using Core.Repositories;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Api.Apps.AdminApi.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    [ApiExplorerSettings(GroupName = "admin_v1")]
    [Route("admin/api/[controller]")]
	[ApiController]
	public class BrandsController : ControllerBase
	{
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService) 
		{
            _brandService = brandService;
        }

		[HttpGet("")]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await _brandService.GetAllAsync());
		}

		[HttpPost("")]
		public async  Task<IActionResult> Create(BrandDto brandDto)
		{
            var data = await _brandService.CreateAsync(brandDto);
            return StatusCode(StatusCodes.Status201Created, data);
        }

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
            return Ok(await _brandService.GetById(id));
        }

		[HttpPost("{id}")]
		public async Task<IActionResult> Update(int id,BrandDto brandDto)
		{
            await _brandService.UpdateAsync(id, brandDto);
            return NoContent();
        }

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
            await _brandService.DeleteAsync(id);
            return NoContent();
        }
	}
}
