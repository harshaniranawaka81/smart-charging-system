using Microsoft.Extensions.Hosting;
using SCS.DAL;
using SCS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCS.Tests
{
    public class DummyDataDBInitializer
    {
        public DummyDataDBInitializer()
        {
        }

        public void Seed(SmartChargingContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Groups.AddRange(
                new Group() { GroupId = 1, GroupName = "Group 1", CapacityAmps = 1200 },
                new Group() { GroupId = 2, GroupName = "Group 2", CapacityAmps = 2500 },
                new Group() { GroupId = 3, GroupName = "Group 3", CapacityAmps = 1400 },
                new Group() { GroupId = 4, GroupName = "Group 4", CapacityAmps = 1600 },
                new Group() { GroupId = 5, GroupName = "Group 5", CapacityAmps = 1800 },
                new Group() { GroupId = 6, GroupName = "Group 6", CapacityAmps = 2000 }
            );

            context.ChargeStations.AddRange(
                new ChargeStation() { ChargeStationId = 1, ChargeStationName = "Charge Station 1", RefGroupId = 1 },
                new ChargeStation() { ChargeStationId = 2, ChargeStationName = "Charge Station 2", RefGroupId = 1 },

                new ChargeStation() { ChargeStationId = 3, ChargeStationName = "Charge Station 3", RefGroupId = 2 },
                new ChargeStation() { ChargeStationId = 4, ChargeStationName = "Charge Station 4", RefGroupId = 2 },

                new ChargeStation() { ChargeStationId = 5, ChargeStationName = "Charge Station 5", RefGroupId = 3 },
                new ChargeStation() { ChargeStationId = 6, ChargeStationName = "Charge Station 6", RefGroupId = 3 },

                new ChargeStation() { ChargeStationId = 7, ChargeStationName = "Charge Station 7", RefGroupId = 4 },
                new ChargeStation() { ChargeStationId = 8, ChargeStationName = "Charge Station 8", RefGroupId = 4 },

                new ChargeStation() { ChargeStationId = 9, ChargeStationName = "Charge Station 9", RefGroupId = 5 },
                new ChargeStation() { ChargeStationId = 10, ChargeStationName = "Charge Station 10", RefGroupId = 5 }
                );

            
            context.Connectors.AddRange(
                new Connector() { ConnectorId = 1, MaxCurrentAmps = 100, RefChargeStationId = 1 },
                new Connector() { ConnectorId = 2, MaxCurrentAmps = 100, RefChargeStationId = 1 },
                new Connector() { ConnectorId = 3, MaxCurrentAmps = 100, RefChargeStationId = 1 },

                new Connector() { ConnectorId = 4, MaxCurrentAmps = 200, RefChargeStationId = 2 },
                new Connector() { ConnectorId = 5, MaxCurrentAmps = 200, RefChargeStationId = 2 },
                new Connector() { ConnectorId = 6, MaxCurrentAmps = 200, RefChargeStationId = 2 },
                new Connector() { ConnectorId = 7, MaxCurrentAmps = 200, RefChargeStationId = 2 },

                new Connector() { ConnectorId = 8, MaxCurrentAmps = 300, RefChargeStationId = 3 },
                new Connector() { ConnectorId = 9, MaxCurrentAmps = 300, RefChargeStationId = 3 },

                new Connector() { ConnectorId = 10, MaxCurrentAmps = 500, RefChargeStationId = 4 },
                new Connector() { ConnectorId = 11,MaxCurrentAmps = 500, RefChargeStationId = 4 },

                new Connector() { ConnectorId = 12, MaxCurrentAmps = 550, RefChargeStationId = 5 },
                new Connector() { ConnectorId = 13, MaxCurrentAmps = 550, RefChargeStationId = 5 },

                new Connector() { ConnectorId = 14, MaxCurrentAmps = 600, RefChargeStationId = 6 },
                new Connector() { ConnectorId = 15, MaxCurrentAmps = 600, RefChargeStationId = 6 },

                new Connector() { ConnectorId = 16, MaxCurrentAmps = 700, RefChargeStationId = 7 },
                new Connector() { ConnectorId = 17, MaxCurrentAmps = 700, RefChargeStationId = 7 },

                new Connector() { ConnectorId = 18, MaxCurrentAmps = 800, RefChargeStationId = 8 },
                new Connector() { ConnectorId = 19, MaxCurrentAmps = 800, RefChargeStationId = 8 },

                new Connector() { ConnectorId = 20, MaxCurrentAmps = 900, RefChargeStationId = 9 },
                new Connector() { ConnectorId = 21, MaxCurrentAmps = 900, RefChargeStationId = 9 },

                new Connector() { ConnectorId = 22, MaxCurrentAmps = 1000, RefChargeStationId = 10 },
                new Connector() { ConnectorId = 24, MaxCurrentAmps = 1000, RefChargeStationId = 10 },
                new Connector() { ConnectorId = 25, MaxCurrentAmps = 1000, RefChargeStationId = 10 },
                new Connector() { ConnectorId = 26, MaxCurrentAmps = 1000, RefChargeStationId = 10 },
                new Connector() { ConnectorId = 27, MaxCurrentAmps = 1000, RefChargeStationId = 10 }

            );

            context.SaveChanges();
        }
    }
}
