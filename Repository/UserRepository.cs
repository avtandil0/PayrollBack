using Contracts;
using Entities;
using Entities.HelperModels;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository :  IUserRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RepositoryContext db;

        public UserRepository(RepositoryContext repositoryContext,
            UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.db = repositoryContext;
        }

        public IEnumerable<ApplicationUser> Filter()
        {
            return db.AspNetUsers.Where(r => r.DateDeleted == null)
               .OrderByDescending(r => r.DateCreated);
        }

        public IEnumerable<UserModel> GetAllUser()
        {
            var query = db.AspNetUsers.Where(r => r.DateDeleted == null)
                                                .OrderByDescending(r => r.DateCreated);
            var result = from x in query
                         select new UserModel
                         {
                             UserId = x.Id,
                             UserName = x.UserName,
                             FirstName = x.FirstName,
                             LastName = x.LastName,
                             Email = x.Email,
                             PhoneNumber = x.PhoneNumber,
                             DateCreated = x.DateCreated,
                             UserClaimTypes = (from claim in db.UserClaims
                                               where claim.UserId == x.Id
                                               select Guid.Parse(claim.ClaimType)).ToList(),
                         };
            return result;
        }
        public ApplicationUser GetApplicationUser(string userName)
        {
            return db.AspNetUsers.FirstOrDefault(r=> r.UserName == userName);
        }

        public ApplicationUser GetUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(ApplicationUser userEntity, Guid role)
        {
            var user = db.AspNetUsers.FirstOrDefault(x => x.Id == userEntity.Id);

            var oldClaims = await userManager.GetClaimsAsync(user);
            await userManager.RemoveClaimsAsync(user, oldClaims);

            var newClaims = new List<Claim>();
            newClaims.Add(new Claim(role.ToString().ToLower(), "true"));
            

            try
            {
                var claimResult = await userManager.AddClaimsAsync(user, newClaims);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            user.UserName = userEntity.UserName;
            user.NormalizedUserName = userEntity.UserName;
            user.FirstName = userEntity.FirstName;
            user.LastName = userEntity.LastName;
            user.PhoneNumber = userEntity.PhoneNumber;
            user.Email = userEntity.Email;
            user.DateChanged = DateTime.Now;

            db.SaveChanges();

        }


    }
}
