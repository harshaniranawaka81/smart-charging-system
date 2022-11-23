using Microsoft.EntityFrameworkCore;
using SCS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCS.DAL
{
    public class ConnectorRepository : IConnectorRepository
    {
        private readonly SmartChargingContext _dbContext;
        public ConnectorRepository(SmartChargingContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> DeleteConnectorAsync(int id)
        {
            var connector = await _dbContext.Connectors.FirstOrDefaultAsync(x => x.ConnectorId == id);

            if (connector != null)
            {
                _dbContext.Connectors.Remove(connector);
                return await _dbContext.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<List<IConnector>> GetAllConnectorsAsync()
        {
            return await _dbContext.Connectors.ToListAsync<IConnector>();
        }

        public async Task<IConnector?> GetConnectorAsync(int id)
        {
            return await _dbContext.Connectors.SingleOrDefaultAsync(x => x.ConnectorId == id);
        }

        public async Task<List<IConnector>> GetConnectorsForChargeStationsAsync(int chargeStationId)
        {
            return await _dbContext.Connectors.Where(x => x.RefChargeStationId == chargeStationId).ToListAsync<IConnector>();
        }

        public async Task<int> SaveConnectorAsync(Connector connector)
        {
            await ValidateConnector(connector);

            await _dbContext.Connectors.AddAsync(connector);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateConnectorAsync(int id, Connector connector)
        {    
            var match = await _dbContext.Connectors.FirstOrDefaultAsync(x => x.ConnectorId == id);

            if (match != null)
            {
                connector.RefChargeStationId = match.RefChargeStationId;
                await ValidateConnector(connector, id);

                _dbContext.Connectors.Update(connector);
                return await _dbContext.SaveChangesAsync();
            }

            return 0;
        }

        private async Task ValidateConnector(Connector connector, int? id = null)
        {
            if (connector.MaxCurrentAmps <= 0)
            {
                throw new InvalidDataException("MaxCurrentAmps should be greater than zero");
            }

            var existingChargeStation = await _dbContext.ChargeStations.FirstOrDefaultAsync(x => x.ChargeStationId == connector.RefChargeStationId);

            if (existingChargeStation == null)
            {
                throw new InvalidDataException("Charge Station Id for the connector should be valid");
            }

            var connectorCountPerGroup = await _dbContext.Connectors.CountAsync(x => x.RefChargeStationId == connector.RefChargeStationId);

            if (id == null && connectorCountPerGroup == 5)
            {
                throw new InvalidDataException($"There cannot be more than 5 connectors per the charge station : {connector.RefChargeStationId}");
            }
            else if (id != null && connectorCountPerGroup > 5)
            {
                throw new InvalidDataException($"There cannot be more than 5 connectors per the charge station : {connector.RefChargeStationId}");
            }

            var group = await _dbContext.Groups.FirstOrDefaultAsync(x => x.GroupId == existingChargeStation.RefGroupId);
            var sumMaxAmps = 0;

            if (group != null)
            {
                var chargeStations = await _dbContext.ChargeStations.Where(x => x.RefGroupId == group.GroupId).ToListAsync();

                foreach (var cs in chargeStations)
                {
                    var sum = await _dbContext.Connectors.Where(x => x.RefChargeStationId == cs.ChargeStationId).SumAsync(c => c.MaxCurrentAmps);
                    sumMaxAmps += sum;
                }

                var existingAmpsForConnector = 0;
                if (id != null)
                {
                    existingAmpsForConnector = await _dbContext.Connectors.Where(x => x.ConnectorId == id).SumAsync(c => c.MaxCurrentAmps); 
                }

                sumMaxAmps += id == null ? connector.MaxCurrentAmps : connector.MaxCurrentAmps - existingAmpsForConnector;

                if (sumMaxAmps > group.CapacityAmps)
                {
                    throw new InvalidDataException($"The capacity in Amps of a Group: {group.GroupId} - {group.CapacityAmps} should always be great or equal to the sum of the Max current in Amps of the Connectors of all Charge Stations in the Group : {sumMaxAmps} ");
                }
            } 
            
        }
    }
}
