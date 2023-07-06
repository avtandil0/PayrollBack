using AutoMapper;
//using AutoMapper.Configuration;
using Microsoft.Extensions.Configuration;

using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PayrollServer.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Entities.Enumerations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PayrollServer.Models.DTOs;
using Entities.HelperModels;

namespace PayrollServer.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        //private readonly IUserRepository userRepository;
        private IRepositoryWrapper _repository;
        private readonly IUserRepository userRepository;

        private readonly IConfiguration configuration;
        private readonly IMapper _mapper;


        public AccountController(
           UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager,
           //IUserRepository userRepository,
            IRepositoryWrapper repository,
           IConfiguration configuration,
           IMapper mapper,
           IUserRepository userRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            //this.userRepository = userRepository;
            _repository = repository;
            this.configuration = configuration;
            this._mapper = mapper;
            this.userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("seedAdmin")]
        public async Task<Result> SeedAdmin()
        {
            string ADMIN_ID = "772848a0-3b20-46bd-8fd3-d83d110aecf0";
            string ROLE_ID = "036f0246-b844-4471-b450-4c4eef779302";
            var admin = new ApplicationUser
            {
                Id = ADMIN_ID,
                Email = "aa@aa.ge",
                EmailConfirmed = true,
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "admin",
                
            };

            var result = await userManager.CreateAsync(admin, "Admin123#");

            return new Result(true, 1, "warmatebit sheiqmna");
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<Result> Login(string userName, string password)
        {

            
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                return new Result(false, -1, "ჩაწერეთ მომხმარებელი და პაროლი");
            }

            //var user = _repository.User.GetApplicationUser(userName);

            var result = await signInManager.PasswordSignInAsync(userName, password, false, false);
            if (result.Succeeded)
            {
                return await GetToken(userName, password);
            }

            return new Result(false, -1, "მომხმარებელი ან პაროლი არასწორია");
        }

        [HttpPost]
        [Route("logout")]
        public async Task<Result> Logout()
        {
            await signInManager.SignOutAsync();

           // HttpContext.Session.Remove("UserSession");

            return new Result(true, 0, "OK");
        }

        [HttpGet]
        [Route("currentUser")]
        public UserModel GetCurrentUser()
        {

            var us = User;
            var usr = us.FindFirst(ClaimTypes.NameIdentifier);
            var username = usr.Value;
            var cc = User.Claims;

            var dd = User.HasClaim(c =>
               c.Type == UserClaimTypeEnum.Admin);
            var td = User.Identities;
            var td1 = User.Identity;
            var td21 = User.IsInRole(UserClaimTypeEnum.Admin);

            var appUser = userRepository.GetApplicationUser(username);

            UserModel userModel = _mapper.Map<UserModel>(appUser);


            //var medicalOrg = userRepository.GetMedicalOrganizationByUserId(userModel.UserId).Result.FirstOrDefault();
            //    userModel.MedicalOrganizationId = medicalOrg?.Id;

            userModel.Roles = new UserRuleModel
            {
                ViewPersonsList = User.HasClaim(c => c.Type == UserClaimTypeEnum.Admin 
                || c.Type == UserClaimTypeEnum.SuperAdmin || c.Type == UserClaimTypeEnum.Operator ||
                c.Type == UserClaimTypeEnum.Analyst || c.Type == UserClaimTypeEnum.Inspector 
                || c.Type == UserClaimTypeEnum.Monitor),



                AddEditUser = User.HasClaim(c =>
                c.Type == UserClaimTypeEnum.Admin ),


                Admin = User.HasClaim(c =>
                c.Type == UserClaimTypeEnum.Admin),

                Operator = User.HasClaim(c =>
                 c.Type == UserClaimTypeEnum.Operator),

                Analyst = User.HasClaim(c =>
                 c.Type == UserClaimTypeEnum.Analyst),
            };

            return userModel;
        }

        private async Task<Result> GetToken(string userName, string password)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user != null)
            {

                var result = await signInManager.CheckPasswordSignInAsync
                                (user, password, lockoutOnFailure: false);

                if (!result.Succeeded)
                {
                    return new Result(false, -1, "მომხმარებელი ან პაროლი არასწორია");
                }

                //HttpContext.Session.SetString("UserSession", user.Id);

                var userClaims = await userManager.GetClaimsAsync(user);
                userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, userName));
                userClaims.Add(new Claim("UserId", user.Id));
                userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                var token = new JwtSecurityToken
                (
                    issuer: configuration["Token:Issuer"],
                    audience: configuration["Token:Audience"],
                    claims: userClaims,
                    expires: DateTime.UtcNow.AddHours(6),
                    notBefore: DateTime.UtcNow,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey
                                (Encoding.UTF8.GetBytes(configuration["Token:Key"])),
                            SecurityAlgorithms.HmacSha256)
                );

                return new Result(true, 0, new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
            {
                return new Result(false, -1, "მომხმარებელი ვერ მოიძებნა");
            }
        }

        [HttpPost]
        [Route("registerUser")]
        public async Task<Result> Register(UserDTO model)
        {


            model.DateCreated = DateTime.Now;

            var newUser = _mapper.Map<UserDTO, ApplicationUser>(model);

            const string ressetPass = "Resetpass1!";

            newUser.Id = Guid.NewGuid().ToString();
            var result = await userManager.CreateAsync(newUser, ressetPass);
            if (!result.Succeeded)
            {
                return new Result(false, -1, "დაფიქსირდა შეცდომა");
            }

            var user = await userManager.FindByNameAsync(newUser.UserName);

            var claims = new List<Claim>();
            claims.Add(new Claim(model.Role.ToString().ToLower(), "true"));
            //foreach (var item in model.Role)
            //{
            //    claims.Add(new Claim(item.ToString().ToLower(), "true"));
            //}

            await userManager.AddClaimsAsync(user, claims);

            return Result.SuccessInstance();
        }

        [HttpPost]
        [Route("update")]
        public async Task<Result> Update(UserDTO model)
        {


            var user = _mapper.Map<UserDTO, ApplicationUser>(model);

            await userRepository.Update(user, model.Role);

            //_logger.LogInfo($"Update Department id = &{department.Id}");

            return new Result(true, 1, "წარმატებით დასრულდა");
        }


        public class ChangePassWordObject
        {
            public string UserName { get; set; }
            public string CurrentPassword { get; set; }
            public string NewPassword { get; set; }
        }
         [HttpPost]
        [Route("ChangePassword")]
        public async Task<Result> ChangePassword(ChangePassWordObject model)
        {
           

            var username = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await userManager.FindByNameAsync(model.UserName);

            var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                return await GetToken(user.UserName, model.NewPassword);
            }

            return new Result(false, -1, "დაფიქსირდა შეცდომა");

        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<Result> Delete([FromBody] UserDTO model)
        {


            var user = _mapper.Map<UserDTO, ApplicationUser>(model);


             userRepository.Delete(user);

            //_logger.LogInfo($"Update Department id = &{department.Id}");

            return new Result(true, 1, "წარმატებით დასრულდა");
        }


        [HttpGet]
        [Route("getAllUser")]
        public IEnumerable<UserModel> GetAllUser()
        {
            var users = userRepository.GetAllUser();
            //IEnumerable<UserDTO> result = _mapper.Map< IEnumerable<UserDTO> >(users);

            
            return users;
        }

    }
}
