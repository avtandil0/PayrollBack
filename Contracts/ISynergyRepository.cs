using Entities.HelperModels;
using Entities.Models;
using PayrollServer.Models.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface ISynergyRepository
    {
        IEnumerable<Humre> GetHumres();
        IEnumerable<Humre> GetHumresByFilter(HumreFilter humre);
        IEnumerable<HumreHelper> GetHumresByIds(List<int?> ids);
    }
}
