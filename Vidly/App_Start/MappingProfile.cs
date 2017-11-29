﻿using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.App_Start
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			Mapper.CreateMap<Customer, CustomerDto>();
			Mapper.CreateMap<Movie, MovieDto>();
			Mapper.CreateMap<MembershipType, MembershipTypeDto>();
			Mapper.CreateMap<Genre, GenreDto>();

			// Dto to Domain
			Mapper.CreateMap<CustomerDto, Customer>()
				.ForMember(c => c.Id, opt => opt.Ignore());

			Mapper.CreateMap<MovieDto, Movie>()
				.ForMember(m => m.Id, opt => opt.Ignore());

			Mapper.CreateMap<MembershipTypeDto, MembershipType>()
				.ForMember(mt => mt.Id, opt => opt.Ignore());

			Mapper.CreateMap<GenreDto, Genre>()
				.ForMember(g => g.Id, opt => opt.Ignore());
		}
	}
}