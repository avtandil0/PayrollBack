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
        void CreateCalculation(CalculationFilter calculation, DateTime calculationDate);
        void CreateCalculationWithComponent(CalculationFilter calculation, DateTime calculationDate, Guid compponentId,
            decimal amount, int currency);
        void Paid(PaidHelper paidHelper);
        void UpdateCalculation(Calculation calculation);
        void DeleteCalculation(Calculation calculation);
    }
}
