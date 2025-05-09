﻿using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Demo.BusinessLogic.DataTransferObjects.EmployeeDtos;
using Demo.DataAccess.Models.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Profiles
{
    public class MappingProfiles : Profile
    {
       public MappingProfiles()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Gender ,  Options => Options.MapFrom(src => src.Gender))
                .ForMember(dest => dest.EmployeeType ,  Options => Options.MapFrom(src => src.EmployeeType))
                .ForMember(dest=>dest.Department , Options => Options.MapFrom(src => src.Department !=null ? src.Department.Name : null));

            CreateMap<Employee, EmployeeDetailsDto>()
                .ForMember(dest => dest.Gender, Options => Options.MapFrom(src => src.Gender))
                .ForMember(dest => dest.EmployeeType, Options => Options.MapFrom(src => src.EmployeeType))
                .ForMember(dest => dest.HiringDate, Options => Options.MapFrom(src => DateOnly.FromDateTime(src.HiringDate)))
                .ForMember(dest => dest.Department, Options => Options.MapFrom(src => src.Department != null ? src.Department.Name : null));

            CreateMap<CreatedEmployeeDto,Employee>()
                .ForMember(dest=> dest.HiringDate , options=> options.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)) ).ReverseMap();

            CreateMap<UpdatedEmployeeDto , Employee>()
                .ForMember(dest => dest.HiringDate, options => options.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)));
        }
    }
}
