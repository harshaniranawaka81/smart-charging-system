using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCS.Domain
{
    public class Connector : IConnector
    {
        [Key]
        [Range(1, 5)]
        public int ConnectorId { get; set; }

        [Range(1, int.MaxValue)]
        public int MaxCurrentAmps { get; set; }

        [Required]
        public int RefChargeStationId { get; set; }
    }
}
