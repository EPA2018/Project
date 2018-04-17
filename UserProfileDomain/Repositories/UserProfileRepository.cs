﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Project.Account.Models;
using Project.Core.Account;
using Project.Core.DbContext;
using Project.Core.Repositories;
using Project.UserProfileDomain.DAL;
using Project.UserProfileDomain.Models;

namespace Project.UserProfileDomain.Repositories {

    public interface IUserProfileRepository : IEntityRepository<UserProfile, int> {

        UserProfile GetUserProfile(string userId);
        Task<UserProfile> GetUserProfileAsync(string userId);

        void CreateProfile(UserInfo user);
        Task CreateProfileAsync(UserInfo user);

        Task<IList<UserProfile>> GetUsersInRoleProfileAsync(string roleName);
        Task<IList<UserProfile>> GetUsersInRoleProfileAsync(StandardRoles role);

        /// <summary>
        /// Gets the <see cref="UserProfile"/> for users that exactly matches the role. e.g. if a request for <see cref="StandardRoles.Coach"/> is made, users in role <see cref="StandardRoles.Admin"/> will not be retrieved.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<IList<UserProfile>> GetStrictInRoleUserProfilesAsync(StandardRoles role);

    }

    public class UserProfileRepository : EntityRepository<UserProfile, int>, IUserProfileRepository {

        readonly IUserProfileContext upContext;

        public UserProfileRepository(IUserProfileContext userProfileContext) : base(userProfileContext) {
            upContext = userProfileContext ?? throw new ArgumentNullException(nameof(userProfileContext));
        }

        public UserProfile GetUserProfile(string userId) {
            return GetQ(profile => profile.UserId == userId).FirstOrDefault();
        }

        public async Task<UserProfile> GetUserProfileAsync(string userId) {
            return await GetQ(profile => profile.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<IList<UserProfile>> GetStrictInRoleUserProfilesAsync(StandardRoles role) {
            var query = GetUsersInRoleProfileQAsync(role.ToString());

            if (role < StandardRoles.Admin)
                query = FilterNotInRoleQAsync(query, StandardRoles.Admin.ToString());

            if(role < StandardRoles.Coach)
                query = FilterNotInRoleQAsync(query, StandardRoles.Coach.ToString());

            return await query.ToListAsync();
        }

        public async Task<IList<UserProfile>> GetUsersInRoleProfileAsync(StandardRoles role) {
            return await GetUsersInRoleProfileAsync(role.ToString());
        }

        public async Task<IList<UserProfile>> GetUsersInRoleProfileAsync(string roleName) {
            return await GetUsersInRoleProfileQAsync(roleName).ToListAsync();

            //var sql = (query as System.Data.Entity.Infrastructure.DbQuery<UserProfile>).Sql;
        }

        public async Task CreateProfileAsync(UserInfo user) {
            if (await GetUserProfileAsync(user.Id) != null)
                return;

            var profile = new UserProfile {
                UserId = user.Id
            };

            InsertOrUpdate(profile);
            await SaveAsync();
        }

        public void CreateProfile(UserInfo user) {

            if (GetUserProfile(user.Id) != null)
                return;

            var profile = new UserProfile {
                UserId = user.Id
            };

            InsertOrUpdate(profile);
            Save();
        }

        private IQueryable<UserProfile> GetUsersInRoleProfileQAsync(string roleName) {
            return upContext.UserProfiles.Where(p => p.User.Roles.Any(r => r.Role.Name == roleName));
        }

        private IQueryable<UserProfile> FilterNotInRoleQAsync(IQueryable<UserProfile> query, string roleName) {
            return query.Where(p => !p.User.Roles.Any(r => r.Role.Name == roleName));
        }
    }
}
