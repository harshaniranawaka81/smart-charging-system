using SCS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCS.BLL
{
    public interface IConnectorService
    {
        Task<int> SaveConnectorAsync(Connector connector);
        Task<int> UpdateConnectorAsync(int id, Connector connector);
        Task<int> DeleteConnectorAsync(int id);

        Task<IConnector?> GetConnectorAsync(int id);
        Task<List<IConnector>> GetAllConnectorsAsync();

        Task<List<IConnector>> GetConnectorsForChargeStationAsync(int chargeStationId);
    }
}
