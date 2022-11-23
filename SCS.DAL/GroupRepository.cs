using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SCS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SCS.DAL
{
    public class GroupRepository : IGroupRepository
    {
        private readonly SmartChargingContext _dbContext;
        public GroupRepository(SmartChargingContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> DeleteGroupAsync(int id)
        {
            var group = await _dbContext.Groups.FirstOrDefaultAsync(x => x.GroupId == id);

            if (group != null)
            {
                _dbContext.Groups.Remove(group);
                return await _dbContext.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<List<IGroup>> GetAllGroupsAsync()
        {
            return await _dbContext.Groups.ToListAsync<IGroup>();
        }

        public async Task<IGroup?> GetGroupAsync(int id)
        {
            return await _dbContext.Groups.SingleOrDefaultAsync(x => x.GroupId == id);
        }

        public async Task<int> SaveGroupAsync(Group group)
        {
            ValidateGroup(group);

            await _dbContext.Groups.AddAsync(group);

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateGroupAsync(int id, Group group)
        {
            var match = await _dbContext.Groups.FirstOrDefaultAsync(x => x.GroupId == id);

            if (match != null)
            {
                ValidateGroup(group);

                _dbContext.Groups.Update(group);
                return await _dbContext.SaveChangesAsync();
            }

            return 0;
        }

        private void ValidateGroup(Group group, int? id = null)
        {
            if (group.CapacityAmps <= 0)
            {
                throw new InvalidDataException("CapacityAmps should be greater than zero");
            }
        }
    }
}
