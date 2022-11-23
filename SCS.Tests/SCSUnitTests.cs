using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SCS.API.Controllers;
using SCS.BLL;
using SCS.DAL;
using SCS.Domain;

namespace SCS.Tests
{
    public class SCSUnitTests
    {
        private static GroupRepository groupRepository;
        private static ChargeStationRepository chargeStationRepository;
        private static ConnectorRepository connectorRepository;

        private static GroupService groupService;
        private static ChargeStationService chargeStationService;
        private static ConnectorService connectorService;

        public SCSUnitTests()
        {
            DbContextOptions<SmartChargingContext> dbContextOptions = new DbContextOptionsBuilder<SmartChargingContext>()
                .UseInMemoryDatabase("SCSTestContext")
                .Options;

            var testDbContext = new SmartChargingContext(dbContextOptions);
            DummyDataDBInitializer db = new();
            db.Seed(testDbContext);

            groupRepository = new GroupRepository(testDbContext);
            chargeStationRepository = new ChargeStationRepository(testDbContext);
            connectorRepository = new ConnectorRepository(testDbContext);

            groupService = new GroupService(groupRepository);
            chargeStationService = new ChargeStationService(chargeStationRepository);
            connectorService = new ConnectorService(connectorRepository);
        }

        #region Group   


        [Fact]
        public async void Task_GetAllGroups_Ok()
        {
            var controller = new GroupsController(groupService);

            var data = await controller.GetAllGroups();

            Assert.IsType<OkObjectResult>(data);
        }


        [Fact]
        public async void Task_GetAllGroups_MatchResults()
        {
            var controller = new GroupsController(groupService);

            var data = await controller.GetAllGroups();

            Assert.IsType<OkObjectResult>(data);

            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var groups = okResult.Value.Should().BeAssignableTo<List<IGroup>>().Subject;

            Assert.Equal("Group 1", groups[0].GroupName);
            Assert.Equal(1200, groups[0].CapacityAmps);

            Assert.Equal("Group 2", groups[1].GroupName);
            Assert.Equal(2500, groups[1].CapacityAmps);

            Assert.Equal("Group 3", groups[2].GroupName);
            Assert.Equal(1400, groups[2].CapacityAmps);

            Assert.Equal("Group 4", groups[3].GroupName);
            Assert.Equal(1600, groups[3].CapacityAmps);

            Assert.Equal("Group 5", groups[4].GroupName);
            Assert.Equal(1800, groups[4].CapacityAmps);
        }

        [Fact]
        public async void Task_GetGroupById_Ok()
        {
            var controller = new GroupsController(groupService);
            var groupId = 2;

            var data = await controller.GetGroup(groupId);

            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async void Task_GetGroupById_NotFound()
        {
            var controller = new GroupsController(groupService);
            var groupId = 100;

            var data = await controller.GetGroup(groupId);

            Assert.IsType<NotFoundResult>(data);
        }

      
        [Fact]
        public async void Task_GetGroupById_MatchResult()
        {
            var controller = new GroupsController(groupService);
            int groupId = 1;

            var data = await controller.GetGroup(groupId);

            Assert.IsType<OkObjectResult>(data);

            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var group = okResult.Value.Should().BeAssignableTo<Group>().Subject;

            Assert.Equal("Group 1", group.GroupName);
            Assert.Equal(1200, group.CapacityAmps);
        }


        [Fact]
        public async Task Task_SaveGroup_Ok()
        {
            var controller = new GroupsController(groupService);
            var group = new Group() { GroupId = 10, GroupName = "Test Group 10", CapacityAmps = 3000 };

            var data = await controller.SaveGroup(group);

            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async Task Task_SaveGroup_NoGroupName_BadRequest()
        {
            var controller = new GroupsController(groupService);
            var group = new Group() { GroupId = 11, CapacityAmps = 3000 };

            var data = await controller.SaveGroup(group);

            Assert.IsType<BadRequestObjectResult>(data);
        }

        [Fact]
        public async Task Task_SaveGroup_ZeroCapacityAmps_BadRequest()
        {
            var controller = new GroupsController(groupService);
            var group = new Group() { GroupId = 11, GroupName = "Test Group 11", CapacityAmps = 0 };

            var data = await controller.SaveGroup(group);

            Assert.IsType<BadRequestObjectResult>(data);
        }

        [Fact]
        public async void Task_UpdateGroup_Ok()
        {
            var controller = new GroupsController(groupService);
            var groupId = 2;

            var groupName = "Group 2 Updated";
            var capacityAmps = 2000;

            var updatedData = await controller.UpdateGroup(groupId, groupName, capacityAmps);

            Assert.IsType<OkObjectResult>(updatedData);
        }

        [Fact]
        public async void Task_UpdateGroup_NotFound()
        {
            var controller = new GroupsController(groupService);
            var groupId = 500;

            var groupName = "Group Updated";
            var capacityAmps = 2000;

            var updatedData = await controller.UpdateGroup(groupId, groupName, capacityAmps);

            Assert.IsType<NotFoundResult>(updatedData);
        }

        [Fact]
        public async void Task_UpdateGroup_NoGroupName_BadRequest()
        {
            var controller = new GroupsController(groupService);
            var groupId = 2;

            string groupName = null;
            var capacityAmps = 2000;

            var updatedData = await controller.UpdateGroup(groupId, groupName, capacityAmps);

            Assert.IsType<BadRequestObjectResult>(updatedData);
        }

        [Fact]
        public async void Task_UpdateGroup_ZeroCapacityAmps_BadRequest()
        {
            var controller = new GroupsController(groupService);
            var groupId = 2;

            var groupName = "Group 2 Updated";
            var capacityAmps = 0;

            var updatedData = await controller.UpdateGroup(groupId, groupName, capacityAmps);

            Assert.IsType<BadRequestObjectResult>(updatedData);
        }


        [Fact]
        public async void Task_DeleteGroup_Ok()
        {
            var controller = new GroupsController(groupService);
            var groupId = 6;

            var data = await controller.DeleteGroup(groupId);

            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async void Task_DeleteGroup_NotFound()
        {
            var controller = new GroupsController(groupService);
            var groupId = 500;

            var data = await controller.DeleteGroup(groupId);

            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public async void Task_DeleteGroup_WithRelatedChargeStations_Ok()
        {
            var controller = new GroupsController(groupService);
            var groupId = 5;

            var data = await controller.DeleteGroup(groupId);

            Assert.IsType<OkObjectResult>(data);

            var chargeStations = chargeStationService.GetChargeStationsForGroupAsync(groupId);

            Assert.Empty(chargeStations.Result);
        }

        #endregion

        #region  ChargeStation

        [Fact]
        public async void Task_GetAllChargeStations_Ok()
        {
            var controller = new ChargeStationsController(chargeStationService);

            var data = await controller.GetAllChargeStations();

            Assert.IsType<OkObjectResult>(data);
        }


        [Fact]
        public async void Task_GetAllChargeStations_MatchResults()
        {
            var controller = new ChargeStationsController(chargeStationService);

            var data = await controller.GetAllChargeStations();

            Assert.IsType<OkObjectResult>(data);

            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var chargeStations = okResult.Value.Should().BeAssignableTo<List<IChargeStation>>().Subject;

            Assert.Equal("Charge Station 1", chargeStations[0].ChargeStationName);
            Assert.Equal(1, chargeStations[0].RefGroupId);

            Assert.Equal("Charge Station 2", chargeStations[1].ChargeStationName);
            Assert.Equal(1, chargeStations[1].RefGroupId);

            Assert.Equal("Charge Station 3", chargeStations[2].ChargeStationName);
            Assert.Equal(2, chargeStations[2].RefGroupId);

            Assert.Equal("Charge Station 4", chargeStations[3].ChargeStationName);
            Assert.Equal(2, chargeStations[3].RefGroupId);

            Assert.Equal("Charge Station 5", chargeStations[4].ChargeStationName);
            Assert.Equal(3, chargeStations[4].RefGroupId);
        }

        [Fact]
        public async void Task_GetChargeStationById_Ok()
        {
            var controller = new ChargeStationsController(chargeStationService);
            var chargeStationId = 2;

            var data = await controller.GetChargeStation(chargeStationId);

            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async void Task_GetChargeStationById_NotFound()
        {
            var controller = new ChargeStationsController(chargeStationService);
            var chargeStationId = 100;

            var data = await controller.GetChargeStation(chargeStationId);

            Assert.IsType<NotFoundResult>(data);
        }


        [Fact]
        public async void Task_GetChargeStationById_MatchResult()
        {
            var controller = new ChargeStationsController(chargeStationService);
            int chargeStationId = 1;

            var data = await controller.GetChargeStation(chargeStationId);

            Assert.IsType<OkObjectResult>(data);

            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var chargeStation = okResult.Value.Should().BeAssignableTo<ChargeStation>().Subject;

            Assert.Equal("Charge Station 1", chargeStation.ChargeStationName);
            Assert.Equal(1, chargeStation.RefGroupId);
        }


        [Fact]
        public async Task Task_SaveChargeStation_Ok()
        {
            var controller = new ChargeStationsController(chargeStationService);
            var chargeStation = new ChargeStation() { ChargeStationId = 11, ChargeStationName = "Test ChargeStation 11", RefGroupId = 1 };

            var data = await controller.SaveChargeStation(chargeStation);

            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async Task Task_SaveChargeStation_NoChargeStationName_BadRequest()
        {
            var controller = new ChargeStationsController(chargeStationService);
            var chargeStation = new ChargeStation() { ChargeStationId = 11, ChargeStationName = null, RefGroupId = 1 };

            var data = await controller.SaveChargeStation(chargeStation);
            Assert.IsType<BadRequestObjectResult>(data);
        }

        [Fact]
        public async Task Task_SaveChargeStation_SameChargeStationMultipleGroups_BadRequest()
        {
            var controller = new ChargeStationsController(chargeStationService);
            var chargeStation = new ChargeStation() { ChargeStationId = 1, ChargeStationName = "Charge Station 1", RefGroupId = 2 };

            var data = await controller.SaveChargeStation(chargeStation);
            Assert.IsType<BadRequestObjectResult>(data);
        }

        [Fact]
        public async Task Task_SaveChargeStation_NoGroup_BadRequest()
        {
            var controller = new ChargeStationsController(chargeStationService);
            var chargeStation = new ChargeStation() { ChargeStationId = 12, ChargeStationName = "Charge Station 12", RefGroupId = 0 };

            var data = await controller.SaveChargeStation(chargeStation);
            Assert.IsType<BadRequestObjectResult>(data);
        }

        [Fact]
        public async void Task_UpdateChargeStation_Ok()
        {
            var controller = new ChargeStationsController(chargeStationService);
            var chargeStationId = 2; 

            var chargeStationName = "Charge Station 2 Updated";

            var updatedData = await controller.UpdateChargeStation(chargeStationId, chargeStationName);

            Assert.IsType<OkObjectResult>(updatedData);
        }

        [Fact]
        public async void Task_UpdateChargeStation_NotFound()
        {
            var controller = new ChargeStationsController(chargeStationService);
            var chargeStationId = 500;

            var chargeStationName = "Charge Station Updated";

            var updatedData = await controller.UpdateChargeStation(chargeStationId, chargeStationName);

            Assert.IsType<NotFoundResult>(updatedData);
        }

        [Fact]
        public async void Task_UpdateChargeStation_NoChargeStationName_BadRequest()
        {
            var controller = new ChargeStationsController(chargeStationService);
            var chargeStationId = 2;

            string chargeStationName = null;

            var updatedData = await controller.UpdateChargeStation(chargeStationId, chargeStationName);

            Assert.IsType<BadRequestObjectResult>(updatedData);
        }

        [Fact]
        public async void Task_DeleteChargeStation_Ok()
        {
            var controller = new ChargeStationsController(chargeStationService);
            var chargeStationId = 5;

            var data = await controller.DeleteChargeStation(chargeStationId);

            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async void Task_DeleteChargeStation_NotFound()
        {
            var controller = new ChargeStationsController(chargeStationService);
            var chargeStationId = 500;

            var data = await controller.DeleteChargeStation(chargeStationId);

            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public async void Task_DeleteChargeStation_WithRelatedConnectors_Ok()
        {
            var controller = new ChargeStationsController(chargeStationService);
            var chargeStationId = 10;

            var data = await controller.DeleteChargeStation(chargeStationId);

            Assert.IsType<OkObjectResult>(data);

            var connectors = connectorService.GetConnectorsForChargeStationAsync(chargeStationId);

            Assert.Empty(connectors.Result);
        }


        #endregion

        #region Connector 

        [Fact]
        public async void Task_GetAllConnectors_Ok()
        {
            var controller = new ConnectorsController(connectorService);

            var data = await controller.GetAllConnectors();

            Assert.IsType<OkObjectResult>(data);
        }


        [Fact]
        public async void Task_GetAllConnectors_MatchResults()
        {
            var controller = new ConnectorsController(connectorService);

            var data = await controller.GetAllConnectors();

            Assert.IsType<OkObjectResult>(data);

            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var connectors = okResult.Value.Should().BeAssignableTo<List<IConnector>>().Subject;

            Assert.Equal(1, connectors[0].RefChargeStationId);
            Assert.Equal(100, connectors[0].MaxCurrentAmps);

            Assert.Equal(2, connectors[3].RefChargeStationId);
            Assert.Equal(200, connectors[3].MaxCurrentAmps);

            Assert.Equal(3, connectors[7].RefChargeStationId);
            Assert.Equal(300, connectors[7].MaxCurrentAmps);

            Assert.Equal(4, connectors[9].RefChargeStationId);
            Assert.Equal(500, connectors[9].MaxCurrentAmps);

            Assert.Equal(5, connectors[11].RefChargeStationId);
            Assert.Equal(550, connectors[11].MaxCurrentAmps);
        }

        [Fact]
        public async void Task_GetConnectorById_Ok()
        {
            var controller = new ConnectorsController(connectorService);
            var connectorId = 1;

            var data = await controller.GetConnector(connectorId);

            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async void Task_GetConnectorById_NotFound()
        {
            var controller = new ConnectorsController(connectorService);
            var connectorId = 100;

            var data = await controller.GetConnector(connectorId);

            Assert.IsType<NotFoundResult>(data);
        }


        [Fact]
        public async void Task_GetConnectorById_MatchResult()
        {
            var controller = new ConnectorsController(connectorService);
            int connectorId = 1;

            var data = await controller.GetConnector(connectorId);

            Assert.IsType<OkObjectResult>(data);

            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var connector = okResult.Value.Should().BeAssignableTo<Connector>().Subject;

            Assert.Equal(1, connector.RefChargeStationId);
            Assert.Equal(100, connector.MaxCurrentAmps);
        }


        [Fact]
        public async Task Task_SaveConnector_Ok()
        {
            var controller = new ConnectorsController(connectorService);
            var connector = new Connector() { ConnectorId = 28, RefChargeStationId = 1, MaxCurrentAmps = 50 };

            var data = await controller.SaveConnector(connector);

            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async Task Task_SaveConnector_MatchGroupCapacity_Ok()
        {
            var controller = new ConnectorsController(connectorService);
            var connector = new Connector() { ConnectorId = 30, RefChargeStationId = 1, MaxCurrentAmps = 100 };

            var data = await controller.SaveConnector(connector);
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async Task Task_SaveConnector_MoreThan5ConnectorsPerChargeStation_BadRequest()
        {
            var controller = new ConnectorsController(connectorService);
            var connector = new Connector() { ConnectorId = 29, RefChargeStationId = 10, MaxCurrentAmps = 500 };

            var data = await controller.SaveConnector(connector);
            Assert.IsType<BadRequestObjectResult>(data);
        }       

        [Fact]
        public async Task Task_SaveConnector_ExceedGroupCapacity_BadRequest()
        {
            var controller = new ConnectorsController(connectorService);
            var connector = new Connector() { ConnectorId = 31, RefChargeStationId = 1, MaxCurrentAmps = 300 };

            var data = await controller.SaveConnector(connector);
            Assert.IsType<BadRequestObjectResult>(data);
        }

        [Fact]
        public async Task Task_SaveConnector_ZeroMaxCurrentAmps_BadRequest()
        {
            var controller = new ConnectorsController(connectorService);
            var connector = new Connector() { ConnectorId = 32, RefChargeStationId = 1, MaxCurrentAmps = 0 };

            var data = await controller.SaveConnector(connector);
            Assert.IsType<BadRequestObjectResult>(data);
        }

        [Fact]
        public async void Task_UpdateConnector_Ok()
        {
            var controller = new ConnectorsController(connectorService);
            var connectorId = 8;

            var maxCurrentAmps = 800;

            var updatedData = await controller.UpdateConnector(connectorId, maxCurrentAmps);

            Assert.IsType<OkObjectResult>(updatedData);
        }

        [Fact]
        public async void Task_UpdateConnector_NotFound()
        {
            var controller = new ConnectorsController(connectorService);
            var connectorId = 500;

            var maxCurrentAmps = 800;

            var updatedData = await controller.UpdateConnector(connectorId, maxCurrentAmps);

            Assert.IsType<NotFoundResult>(updatedData);
        }

        [Fact]
        public async Task Task_UpdateConnector_MatchGroupCapacity_Ok()
        {
            var controller = new ConnectorsController(connectorService);
            var connectorId = 1;

            var maxCurrentAmps = 100;

            var data = await controller.UpdateConnector(connectorId, maxCurrentAmps);
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async Task Task_UpdateConnector_Exactly5ConnectorsPerChargeStation_BadRequest()
        {
            var controller = new ConnectorsController(connectorService);
            var connectorId = 27;

            var maxCurrentAmps = 500;

            var data = await controller.UpdateConnector(connectorId, maxCurrentAmps);
            Assert.IsType<BadRequestObjectResult>(data);
        }

        [Fact]
        public async Task Task_UpdateConnector_ExceedGroupCapacity_BadRequest()
        {
            var controller = new ConnectorsController(connectorService);
            var connectorId = 1;

            var maxCurrentAmps = 500;

            var data = await controller.UpdateConnector(connectorId, maxCurrentAmps);
            Assert.IsType<BadRequestObjectResult>(data);
        }

        [Fact]
        public async Task Task_UpdateConnector_ZeroMaxCurrentAmps_BadRequest()
        {
            var controller = new ConnectorsController(connectorService);
            var connectorId = 1;

            var maxCurrentAmps = 0;

            var data = await controller.UpdateConnector(connectorId, maxCurrentAmps);
            Assert.IsType<BadRequestObjectResult>(data);
        }

        [Fact]
        public async void Task_DeleteConnector_Ok()
        {
            var controller = new ConnectorsController(connectorService);
            var connectorId = 5;

            var data = await controller.DeleteConnector(connectorId);

            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async void Task_DeleteConnector_NotFound()
        {
            var controller = new ConnectorsController(connectorService);
            var connectorId = 500;

            var data = await controller.DeleteConnector(connectorId);

            Assert.IsType<NotFoundResult>(data);
        }

        #endregion
    }
}