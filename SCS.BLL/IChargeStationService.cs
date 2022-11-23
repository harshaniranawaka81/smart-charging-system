using SCS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCS.BLL
{
    public interface IChargeStationService
    {
        Task<int> SaveChargeStationAsync(ChargeStation ChargeStation);
        Task<int> UpdateChargeStationAsync(int id, ChargeStation ChargeStation);
        Task<int> DeleteChargeStationAsync(int id);

        Task<IChargeStation?> GetChargeStationAsync(int id);
        Task<List<IChargeStation>> GetAllChargeStationsAsync();
        Task<List<IChargeStation>> GetChargeStationsForGroupAsync(int groupId);
    }
}
