using SCS.DAL;
using SCS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCS.BLL
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<int> DeleteGroupAsync(int id)
        {
            return await _groupRepository.DeleteGroupAsync(id);
        }

        public async Task<List<IGroup>> GetAllGroupsAsync()
        {
            return await _groupRepository.GetAllGroupsAsync();
        }

        public async Task<IGroup?> GetGroupAsync(int id)
        {
            return await _groupRepository.GetGroupAsync(id);
        }

        public async Task<int> SaveGroupAsync(Group group)
        {
            return await _groupRepository.SaveGroupAsync(group);
        }

        public async Task<int> UpdateGroupAsync(int id, Group group)
        {
            return await _groupRepository.UpdateGroupAsync(id, group);
        }
    }
}
