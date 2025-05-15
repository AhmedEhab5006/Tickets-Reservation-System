using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TicketsReservationSystem.DAL.Database;
using TicketsReservationSystem.DAL.Models;

namespace TicketsReservationSystem.DAL.Repository
{
        public class UserRepository : IUserRepository
        {
            private UserManager<ApplicationUser> _userManager;
            private ProgramContext _context;

        public UserRepository(UserManager<ApplicationUser> userManager , ProgramContext context)
            {
                _userManager = userManager;
                _context = context;
            }

            public async Task<string> AddAsync(string password, ApplicationUser user)
            {
                var done = await _userManager.CreateAsync(user, password);
                if (done.Succeeded)
                {
                    return "done";
                }

                return null;
            }

            public async Task<string> CheckPassword(string password, ApplicationUser user)
            {
                var check = await _userManager.CheckPasswordAsync(user, password);

                if (check != null)
                {
                    return "done";
                }

                return null;
            }

        public async Task<string> CreateVendor(Vendor vendor)
        { 
              _context.vendors.Add(vendor);
            var done = await _context.SaveChangesAsync();
            if (done > 0)
            {
                return "done";
            }

            return null;
        }

        public async Task<string> DeleteAsync(ApplicationUser user)
            {
                var done = await _userManager.DeleteAsync(user);
                if (done.Succeeded)
                {
                    return "done";
                }
                return null;
            }

        public async Task<IQueryable<ApplicationUser>> GetAllAsync()
            {
                return await Task.FromResult(_userManager.Users);
            }

            public async Task<ApplicationUser> GetByEmail(string email)
            {
                var found = await _userManager.FindByEmailAsync(email);
                if (found != null)
                {
                    return found;
                }
                return null;
            }

            public async Task<ApplicationUser?> GetByIdAsync(string userId)
            {
                var found = await _userManager.FindByIdAsync(userId);
                if (found != null)
                {
                    return found;
                }
                return null;
            }

            public async Task<string> GetByUserName(string userName)
            {
                var found = await _userManager.FindByNameAsync(userName);
                if (found != null)
                {
                    return "done";
                }
                return null;
            }

            public async Task<IList<Claim>> GetClaims(ApplicationUser user)
            {
                var found = await _userManager.GetClaimsAsync(user);

                if (found != null)
                {
                    return found.ToList();
                }

                return null;
            }

        public async Task UntrackUser(string id)
        {
            var baseEntity = await _userManager.FindByIdAsync(id);
            if (baseEntity != null)
            {
                _context.Entry(baseEntity).State = EntityState.Detached;
            }
        }

        public async Task<string> UpdateAsync(ApplicationUser user)
            {
                var done = await _userManager.UpdateAsync(user);
                if (done.Succeeded)
                {
                    return "done";
                }
                return null;
            }
        }
    }
