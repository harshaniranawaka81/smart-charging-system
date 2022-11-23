using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCS.Domain
{
    public interface IGroup
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public int CapacityAmps { get; set; }

    }
}
