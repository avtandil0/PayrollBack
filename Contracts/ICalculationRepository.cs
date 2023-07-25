using Entities.FilterModels;
using Entities.HelperModels;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface ICalculationRepository : IRepositoryBase<Calculation>
    {
        IEnumerable<Calculation> GetAllCalculation();
        IEnumerable<Calculation> GetCalculationByFilter(CalculationFilter calculationFilter);
        void CreateCalculation(List<Guid> employeeIds, DateTime calculationDate);
        void CreateCalculationWithComponent(List<Guid> employeeIds, DateTime calculationDate, Guid compponentId,
            decimal amount, int currency);
        void Paid(PaidHelper paidHelper);
        void UpdateCalculation(Calculation calculation);
        void DeleteCalculation(Calculation calculation);
    }
}
