using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Models;
using ApplicationCore.Services;
using Moq;

namespace ApplicationCoreTest.Services.ChassisServiceTest
{
    [TestClass]
    public sealed class T01_ChassisServiceTest_GetAll
    {
        [TestMethod]
        public void GetAll_0Chassis_ReturnOk()
        {
            var mockChassisRepository = new Mock<IChassisRepository>(MockBehavior.Strict);
            mockChassisRepository.Setup(x => x.GetAll()).ReturnsAsync(new List<Chassis>());

            var repository = new ChassisService(mockChassisRepository.Object);

            var result = repository.GetAll().Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());

            mockChassisRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [TestMethod]
        public void GetAll_2Chassis_ReturnOk()
        {
            var mockChassisRepository = new Mock<IChassisRepository>(MockBehavior.Strict);
            mockChassisRepository.Setup(x => x.GetAll()).ReturnsAsync(new List<Chassis>
            {
               new Chassis
               {
                   Name = "Megane",
                   Brand = "Renault",
                   Price = 12000
               },
               new Chassis
               {
                   Name = "208",
                   Brand = "Peugeot",
                   Price = 13000
               }
            });

            var repository = new ChassisService(mockChassisRepository.Object);

            var result = repository.GetAll().Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            var chassis1 = result[0];
            Assert.IsNotNull(chassis1);
            Assert.AreEqual("Megane", chassis1.Name);
            Assert.AreEqual("Renault", chassis1.Brand);
            Assert.AreEqual(12000, chassis1.Price);
            var chassis2 = result[1];
            Assert.IsNotNull(chassis2);
            Assert.AreEqual("208", chassis2.Name);
            Assert.AreEqual("Peugeot", chassis2.Brand);
            Assert.AreEqual(13000, chassis2.Price);

            mockChassisRepository.Verify(x => x.GetAll(), Times.Once);
        }
    }
}
