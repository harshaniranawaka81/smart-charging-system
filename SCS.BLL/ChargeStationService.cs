using SCS.DAL;
using SCS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace SCS.BLL
{
    public class ChargeStationService : IChargeStationService
    {
        private readonly IChargeStationRepository _ChargeStationRepository;
        public ChargeStationService(IChargeStationRepository chargeStationRepository)
        {
            _ChargeStationRepository = chargeStationRepository;
        }

        public async Task<int> DeleteChargeStationAsync(int id)
        {
            return await _ChargeStationRepository.DeleteChargeStationAsync(id);
        }

        public async Task<List<IChargeStation>> GetAllChargeStationsAsync()
        {
            return await _ChargeStationRepository.GetAllChargeStationsAsync();
        }

        public async Task<IChargeStation?> GetChargeStationAsync(int id)
        {
            return await _ChargeStationRepository.GetChargeStationAsync(id);
        }

        public async Task<List<IChargeStation>> GetChargeStationsForGroupAsync(int chargeStationId)
        {
            return await _ChargeStationRepository.GetChargeStationsForGroupAsync(chargeStationId);
        }

        public async Task<int> SaveChargeStationAsync(ChargeStation chargeStation)
        {
            return await _ChargeStationRepository.SaveChargeStationAsync(chargeStation);
        }

        public async Task<int> UpdateChargeStationAsync(int id, ChargeStation chargeStation)
        {
            return await _ChargeStationRepository.UpdateChargeStationAsync(id, chargeStation);
        }
    }
}
