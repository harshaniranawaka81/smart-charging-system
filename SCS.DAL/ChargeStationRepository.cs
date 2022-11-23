using Microsoft.EntityFrameworkCore;
using SCS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SCS.DAL
{
    public class ChargeStationRepository : IChargeStationRepository
    {
        private readonly SmartChargingContext _dbContext;
        public ChargeStationRepository(SmartChargingContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> DeleteChargeStationAsync(int id)
        {
            var chargeStation = await _dbContext.ChargeStations.FirstOrDefaultAsync(x => x.ChargeStationId == id);

            if (chargeStation != null)
            {
                _dbContext.ChargeStations.Remove(chargeStation);
                return await _dbContext.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<List<IChargeStation>> GetAllChargeStationsAsync()
        {
            return await _dbContext.ChargeStations.ToListAsync<IChargeStation>();
        }

        public async Task<IChargeStation?> GetChargeStationAsync(int id)
        {
            return await _dbContext.ChargeStations.SingleOrDefaultAsync(x => x.ChargeStationId == id);
        }

        public async Task<List<IChargeStation>> GetChargeStationsForGroupAsync(int groupId)
        {
            return await _dbContext.ChargeStations.Where(x => x.RefGroupId == groupId).ToListAsync<IChargeStation>();
        }

        public async Task<int> SaveChargeStationAsync(ChargeStation chargeStation)
        {
            await ValidateChargeStation(chargeStation);

            await _dbContext.ChargeStations.AddAsync(chargeStation);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateChargeStationAsync(int id, ChargeStation chargeStation)
        {           
            var matchChargeStation = await _dbContext.ChargeStations.FirstOrDefaultAsync(x => x.ChargeStationId == id);

            if (matchChargeStation != null)
            {
                chargeStation.RefGroupId = matchChargeStation.RefGroupId;
                await ValidateChargeStation(chargeStation, id);

                _dbContext.ChargeStations.Update(chargeStation);
                return await _dbContext.SaveChangesAsync();
            }

            return 0;
        }

        private async Task ValidateChargeStation(ChargeStation chargeStation, int? id = null)
        {
            var existingGroup = await _dbContext.Groups.FirstOrDefaultAsync(x => x.GroupId == chargeStation.RefGroupId);

            if (existingGroup == null)
            {
                throw new InvalidDataException("Group Id for the charge station should be valid");
            }

            var chargeStationId = id != null ? id : chargeStation.ChargeStationId;

            var existInOtherGroup = await _dbContext.ChargeStations.FirstOrDefaultAsync(x => x.ChargeStationId == chargeStationId);

            if (existInOtherGroup != null && existInOtherGroup.RefGroupId != chargeStation.RefGroupId)
            {
                throw new InvalidDataException("Charge station cannot exist in multiple groups");
            }
        }
    }
}
