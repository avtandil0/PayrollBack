using PayrollServer.Models.DTOs;
using AutoMapper;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollServer.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, DepartmentDTO>();
            CreateMap<DepartmentDTO, Department>();


            CreateMap<Coefficient, CoefficientDTO>();
            CreateMap<CoefficientDTO, Coefficient>();

            CreateMap<Project, ProjectDTO>();
            CreateMap<ProjectDTO, Project>();

            CreateMap<CostCenter, CostCenterDTO>();
            CreateMap<CostCenterDTO, CostCenter>();

            CreateMap<AccountsReportChart, AccountsReportChartDTO>()
                .ForMember(dest => dest.AccountsReportChartTypeName, act => act.MapFrom(src => src.AccountsReportChartType.Description));
            CreateMap<AccountsReportChartDTO, AccountsReportChart>();

            CreateMap<AccountsReportChartType, AccountsReportChartTypeDTO>();
            CreateMap<AccountsReportChartTypeDTO, AccountsReportChartType>();
        }
    }
}
