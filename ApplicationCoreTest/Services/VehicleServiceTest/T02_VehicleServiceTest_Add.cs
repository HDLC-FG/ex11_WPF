using ApplicationCore;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Models;
using ApplicationCore.Services;
using Moq;

namespace ApplicationCoreTest.Services.VehicleServiceTest
{
    [TestClass]
    public sealed class T02_VehicleServiceTest_Add
    {
        [TestMethod]
        public void Add_Vehicle_ReturnOk()
        {
            var vehicle = new Vehicle
            {
                Chassis = new Chassis
                {
                    Brand = "Renault",
                    Name = "Megane",
                    Price = 12000
                },
                Engine = new Engine
                {
                    Horsepower = 100,
                    Price = 8000,
                    Type = Enums.EngineType.Petrol
                },
                Options = new List<Option>
                {
                    new Option
                    {
                        Name= "GPS",
                        Price = 200
                    }
                }
            };
            var mockVehicleRepository = new Mock<IVehicleRepository>(MockBehavior.Strict);
            mockVehicleRepository.Setup(x => x.Add(vehicle)).Returns(Task.CompletedTask);
            var repository = new VehicleService(mockVehicleRepository.Object);

            var task = repository.Add(vehicle).GetAwaiter();

            Assert.IsNotNull(task);
            Assert.IsTrue(task.IsCompleted);
            mockVehicleRepository.Verify(x => x.Add(vehicle), Times.Once);
        }
    }
}
