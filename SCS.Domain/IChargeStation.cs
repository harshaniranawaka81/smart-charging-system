using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCS.Domain
{
    public interface IChargeStation
    {
        public int ChargeStationId { get; set; }

        public string ChargeStationName { get; set; }

        public int RefGroupId { get; set; }
    }
}
