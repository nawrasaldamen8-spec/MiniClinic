/*
 * [NEW SERVICE] IdentityService
 * This service provides an abstraction layer over ASP.NET Identity's UserManager.
 * It ensures that the Business Logic layer doesn't depend directly on Identity Framework details.
 */
using Microsoft.AspNetCore.Identity;
using MiniClinicManagementSystem.Core.Entities;
using MiniClinicManagementSystem.Core.Enums;
using MiniClinicManagementSystem.Core.Exceptions;
using MiniClinicManagementSystem.Core.Interfaces.IServices;
using System.Net;

namespace MiniClinicManagementSystem.Services
{
// ... (rest of implementation)
    public class IdentityService(UserManager<ApplicationUser> userManager) : IIdentityService
    {
        public async Task<Result> AddToRoleAsync(ApplicationUser user, Role role)
        {
            var result = await userManager.AddToRoleAsync(user, role.ToString());
            if(!result.Succeeded)
                return Result.Failure(result.Errors.First().Description, HttpStatusCode.InternalServerError);
            return Result.Success("Role assigned.", HttpStatusCode.OK);
        }

        public async Task<Result<ApplicationUser>> CreateUserAsync(string email, string password, string Phone)
        {
            var user = new ApplicationUser
            { 
                UserName = email,
                Email = email,
                PhoneNumber = Phone
            };

            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                return Result<ApplicationUser>.Failure(result.Errors.First().Description, HttpStatusCode.InternalServerError);

            return Result<ApplicationUser>.Success(user, "User created successfully.", HttpStatusCode.Created);
        }

        public  async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            var result = await userManager.DeleteAsync(user);

            if(!result.Succeeded)
                return Result.Failure(result.Errors.First().Description, HttpStatusCode.InternalServerError);

            return Result.Success("User deleted successfully.", HttpStatusCode.OK);
        }
    }
}
