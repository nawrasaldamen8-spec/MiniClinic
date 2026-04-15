using Microsoft.EntityFrameworkCore.Migrations.Operations;
using MiniClinicManagementSystem.Core.Entities;
using MiniClinicManagementSystem.Core.Enums;
using MiniClinicManagementSystem.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniClinicManagementSystem.Core.Interfaces.IServices
{
    public interface IIdentityService
    {
        Task<Result<ApplicationUser>> CreateUserAsync(string email, string password, string phone);
        Task<Result> AddToRoleAsync(ApplicationUser user, Role role);
        Task<Result> DeleteUserAsync(ApplicationUser user);
    }
}
