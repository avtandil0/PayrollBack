﻿using PayrollServer.Models.DTOs;
using AutoMapper;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PayrollServer.Models;
using Entities.HelperModels;

namespace PayrollServer.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, DepartmentDTO>()
                 .ForMember(dest => dest.EmployeeCount, act => act.MapFrom(src => src.Employees.Count));
            CreateMap<DepartmentDTO, Department>();


            CreateMap<Coefficient, CoefficientDTO>();
            CreateMap<CoefficientDTO, Coefficient>();

            CreateMap<Project, ProjectDTO>();
            CreateMap<ProjectDTO, Project>();

            CreateMap<AccountsReportChartType, AccountsReportChartTypeDTO>();
            CreateMap<AccountsReportChartTypeDTO, AccountsReportChartType>();

            CreateMap<CostCenter, CostCenterDTO>();
            CreateMap<CostCenterDTO, CostCenter>();

            CreateMap<AccountsReportChart, AccountsReportChartDTO>()
                .ForMember(dest => dest.AccountsReportChartTypeName, act => act.MapFrom(src => src.AccountsReportChartType.Name));
            CreateMap<AccountsReportChartDTO, AccountsReportChart>();

            CreateMap<Component, ComponentDTO>()
                 .ForMember(dest => dest.CreditAccountName, act => act.MapFrom(src => src.CreditAccount.Description))
                 .ForMember(dest => dest.DebitAccountName, act => act.MapFrom(src => src.DebitAccount.Description))
                 .ForMember(dest => dest.CoefficientName, act => act.MapFrom(src => src.Coefficient.Description));
            CreateMap<ComponentDTO, Component>();

            CreateMap<EmployeeComponent, EmployeeComponentDTO>()
              .ForMember(dest => dest.ComponentName, act => act.MapFrom(src => src.Component.Name))
              .ForMember(dest => dest.ProjectCode, act => act.MapFrom(src => src.Project.Code))
              .ForMember(dest => dest.CostCenterCode, act => act.MapFrom(src => src.CostCenter.Code));
            CreateMap<EmployeeComponentDTO, EmployeeComponent>();


            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.DepartmentName, act => act.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.SchemeTypeName, act => act.MapFrom(src => src.SchemeType.Name));
            CreateMap<EmployeeDTO, Employee>();

         

            CreateMap<Humre, HumreDTO>();
            CreateMap<HumreDTO, Humre>();

            CreateMap<Calculation, CalculationDTO>()
                .ForMember(dest => dest.FirstName, act => act.MapFrom(src => src.Employee.FirstName))
                .ForMember(dest => dest.LastName, act => act.MapFrom(src => src.Employee.LastName))
                .ForMember(dest => dest.Gross, act => act.MapFrom(src => src.Gross))
                .ForMember(dest => dest.IncomeTax, act => act.MapFrom(src => src.IncomeTax))
                .ForMember(dest => dest.PensionTax, act => act.MapFrom(src => src.PensionTax))
                .ForMember(dest => dest.Paid, act => act.MapFrom(src => src.Paid))
                .ForMember(dest => dest.TotalBalance, act => act.MapFrom(src => src.TotalBalance));
            CreateMap<CalculationDTO, Calculation>();

            //CreateMap<Employee, EmployeeCalculationDTO>()
            //    .ForMember(dest => dest.PayrollYear, act => act.MapFrom(src => src.Calculations.Where(r => r.PayrollYear).FirstName))

            //CreateMap<CalculationDTO, Calculation>();

            CreateMap<ApplicationUser, UserModel>();
            CreateMap<UserDTO, ApplicationUser>()
                   .ForMember(dest => dest.Id, act => act.MapFrom(src => src.userId));
            CreateMap<ApplicationUser, UserDTO>();
        }
    }
}
