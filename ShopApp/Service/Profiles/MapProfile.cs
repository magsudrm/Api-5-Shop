using Service.Dtos.BrandDtos;
using Service.Dtos.ProductDtos;
using AutoMapper;
using Core.Entities;

namespace Api.Profiles
{
	public class MapProfile:Profile
	{
		public MapProfile()
		{
			CreateMap<Brand, BrandGetAllItemDto>();
			CreateMap<Brand,BrandGetDto>()/*.ForMember(d=>d.ProductsCount,s=>s.MapFrom(x=>x.Products.Count))*/;
			CreateMap<BrandDto, Brand>();
		}
	}
}
