using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Models;
using ApplicationCore.Services;
using Moq;

namespace ApplicationCoreTest.Services.VehicleServiceTest
{
    [TestClass]
    public sealed class T04_VehicleServiceTest_Delete
    {
        [TestMethod]
        public void Delete_VehicleNotExist_ThrowException()
        {
            var mockVehicleRepository = new Mock<IVehicleRepository>(MockBehavior.Strict);
            mockVehicleRepository.Setup(x => x.GetById(1)).ReturnsAsync((Vehicle?)null);
            var service = new VehicleService(mockVehicleRepository.Object);

            var ex = Assert.ThrowsExceptionAsync<Exception>(() => service.Delete(1)).Result;

            Assert.IsNotNull(ex);
            Assert.AreEqual("Vehicle does not exist", ex.Message);
            mockVehicleRepository.Verify(x => x.GetById(1), Times.Once);
        }

        [TestMethod]
        public void Delete_VehicleExist_ReturnOk()
        {
            var vehicle = new Vehicle();
            var mockVehicleRepository = new Mock<IVehicleRepository>(MockBehavior.Strict);
            mockVehicleRepository.Setup(x => x.GetById(1)).ReturnsAsync(vehicle);
            mockVehicleRepository.Setup(x => x.Delete(vehicle)).Returns(Task.CompletedTask);
            var service = new VehicleService(mockVehicleRepository.Object);

            var task = service.Delete(1).GetAwaiter();

            Assert.IsNotNull(task);
            Assert.IsTrue(task.IsCompleted);
            mockVehicleRepository.Verify(x => x.GetById(1), Times.Once);
            mockVehicleRepository.Verify(x => x.Delete(vehicle), Times.Once);
        }
    }
}
