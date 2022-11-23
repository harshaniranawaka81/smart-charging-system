using SCS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCS.DAL
{
    public interface IConnectorRepository
    {
        Task<int> SaveConnectorAsync(Connector Connector);
        Task<int> UpdateConnectorAsync(int id, Connector Connector);
        Task<int> DeleteConnectorAsync(int id);

        Task<IConnector?> GetConnectorAsync(int id);
        Task<List<IConnector>> GetAllConnectorsAsync();
        Task<List<IConnector>> GetConnectorsForChargeStationsAsync(int chargeStationId);
    }
}
