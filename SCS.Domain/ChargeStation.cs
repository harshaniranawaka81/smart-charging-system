using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCS.Domain
{
    public class ChargeStation : IChargeStation
    {
        [Key]
        public int ChargeStationId { get; set; }

        [Required]
        public string ChargeStationName { get; set; }

        [Required]
        public int RefGroupId { get; set; }
    }
}
