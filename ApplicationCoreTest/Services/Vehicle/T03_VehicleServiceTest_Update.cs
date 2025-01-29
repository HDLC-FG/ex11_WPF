using ApplicationCore;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Models;
using ApplicationCore.Services;
using Moq;

namespace ApplicationCoreTest.Services
{
    [TestClass]
    public sealed class T03_VehicleServiceTest_Update
    {
        [TestMethod]
        public void Update_Vehicle_ReturnOk()
        {
            var vehicle = new Vehicle
            {
                Brand = "Renault",
                Name = "Megane",
                Engine = new Engine
                {
                    Horsepower = 100,
                    Price = 8000,
                    Type = Enums.TypeEngine.Petrol
                },
                Options = new List<Option>
                {
                    new Option
                    {
                        Name= "GPS",
                        Price = 200
                    }
                },
                Price = 12000
            };
            var mockVehicleRepository = new Mock<IVehicleRepository>(MockBehavior.Strict);
            mockVehicleRepository.Setup(x => x.Update(vehicle)).Returns(Task.CompletedTask);
            var repository = new VehicleService(mockVehicleRepository.Object);

            var task = repository.Update(vehicle).GetAwaiter();

            Assert.IsNotNull(task);
            Assert.IsTrue(task.IsCompleted);
            mockVehicleRepository.Verify(x => x.Update(vehicle), Times.Once);
        }
    }
}
