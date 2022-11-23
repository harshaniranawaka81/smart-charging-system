using SCS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCS.BLL
{
    public interface IGroupService
    {
        Task<int> SaveGroupAsync(Group group);
        Task<int> UpdateGroupAsync(int id, Group group);
        Task<int> DeleteGroupAsync(int id);

        Task<IGroup?> GetGroupAsync(int id);
        Task<List<IGroup>> GetAllGroupsAsync();
    }
}
