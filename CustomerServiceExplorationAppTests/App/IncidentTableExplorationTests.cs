using CityPowerAndLight.App;
using CityPowerAndLight.Model;
using CityPowerAndLight.View;
using Microsoft.Xrm.Sdk;
using Moq;
using NUnit.Framework;

namespace CustomerServiceExplorationAppTests.App;

[TestFixture]
public class IncidentTableExplorationTests
{
    private Mock<IOrganizationService>? _organisationServiceMock;
    private Mock<IUserInterface>? _userInterfaceMock;
    private Mock<Incident>? _demoIncidentTemplateMock;
    private IncidentTableExploration? _cut;


    [SetUp]
    public void SetUp()
    {
        _organisationServiceMock = new();
        _userInterfaceMock = new();
        _demoIncidentTemplateMock = new();

        _cut = new(
            _organisationServiceMock.Object,
            _userInterfaceMock.Object,
            _demoIncidentTemplateMock.Object);
    }


    [Test]
    public async Task DemonstrateUpdatingAnIncident_ShallInformUserOfActionBeingTaken()
    {
        //Arrange
        var _demoIncidentToUpdateMock = new Mock<Incident>();
        var testServiceStage = servicestage.Identify;
        //Act
        await _cut!.DemonstrateUpdatingAnIncident(
            _demoIncidentToUpdateMock.Object, testServiceStage);
        //Assert
        _userInterfaceMock!.Verify(mock => mock.PrintMessage(
            It.Is<string>(message =>
                message.Contains("Updating incident"))), Times.Once);
    }


    [TestCase(servicestage.Identify)]
    [TestCase(servicestage.Research)]
    [TestCase(servicestage.Resolve)]
    public async Task
        DemonstrateUpdatingAnIncident_ShallApplyTheUpdateCorrectlyOnTheInstance(
            servicestage updatedServiceStage
        )
    {
        //Arrange
        var _demoIncidentToUpdateMock = new Mock<Incident>();
        //Act
        await _cut!.DemonstrateUpdatingAnIncident(
            _demoIncidentToUpdateMock.Object, updatedServiceStage);
        //Assert
        _demoIncidentToUpdateMock.VerifySet(
            mock => mock.ServiceStage = updatedServiceStage, Times.Once);
    }


    [Test]
    public async Task
        DemonstrateUpdatingAnIncident_ShallCorrectlyCallUpdateOnTheOrganisationService()
    {
        //Arrange
        var _demoIncidentToUpdateMock = new Mock<Incident>();
        var testServiceStage = servicestage.Identify;
        //Act
        await _cut!.DemonstrateUpdatingAnIncident(
            _demoIncidentToUpdateMock.Object, testServiceStage);
        //Assert
        _organisationServiceMock!.Verify(
            mock => mock.Update(_demoIncidentToUpdateMock!.Object),
            Times.Once,
            "Expected Update to be called on the organisationService with " +
            "the incidentToUpdate");
    }
}