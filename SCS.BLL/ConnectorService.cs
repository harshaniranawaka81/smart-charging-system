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
    public class ConnectorService : IConnectorService
    {   
        private readonly IConnectorRepository _connectorRepository;
        public ConnectorService(IConnectorRepository connectorRepository)
        {
            _connectorRepository = connectorRepository;
        }

        public async Task<int> DeleteConnectorAsync(int id)
        {
            return await _connectorRepository.DeleteConnectorAsync(id);
        }

        public async Task<List<IConnector>> GetAllConnectorsAsync()
        {
            return await _connectorRepository.GetAllConnectorsAsync();
        }

        public async Task<IConnector?> GetConnectorAsync(int id)
        {
            return await _connectorRepository.GetConnectorAsync(id);
        }

        public async Task<List<IConnector>> GetConnectorsForChargeStationAsync(int chargeStationId)
        {
            return await _connectorRepository.GetConnectorsForChargeStationsAsync(chargeStationId);
        }

        public async Task<int> SaveConnectorAsync(Connector connector)
        {
            return await _connectorRepository.SaveConnectorAsync(connector);
        }

        public async Task<int> UpdateConnectorAsync(int id, Connector connector)
        {
            return await _connectorRepository.UpdateConnectorAsync(id, connector);
        }
    }
}
