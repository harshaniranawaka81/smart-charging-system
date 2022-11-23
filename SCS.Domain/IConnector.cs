using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCS.Domain
{
    public interface IConnector
    {
        public int ConnectorId { get; set; }
        public int MaxCurrentAmps { get; set; }
        public int RefChargeStationId { get; set; }
    }
}
