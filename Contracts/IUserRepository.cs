using Entities.HelperModels;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserRepository
    {
        IEnumerable<ApplicationUser> Filter();
        IEnumerable<UserModel> GetAllUser();

        //List<UserExcelModel> UsersExcel(UserFilterModel filter);

        ApplicationUser GetUser(Guid id);
        Task Update(ApplicationUser applicationUser, Guid Role);
        ApplicationUser GetApplicationUser(string userName);



        //void UpdateAsync(ApplicationUser user, List<Guid> claimTypes, string userId);

        //void ResetPasswordAsync(string id);

        //IQueryable<ApplicationUser> GetApplicationUserAsync(string userName);
        //IQueryable<PersonStateType> GetPersonStateTypebyAspNetUserTypeId(Guid aspNetUserTypeId);
        //Task<AspNetUserClaims> GetClaimsByUserId(string usersId);

        //Task<List<MedicalOrganization>> GetMedicalOrganizationByUserId(string UserIds);
    }
}
