using Entities.FilterModels;
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
        void UpdateCalculation(Calculation calculation);
        void DeleteCalculation(Calculation calculation);
    }
}
