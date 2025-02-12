using ApplicationCore;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Models;
using ApplicationCore.Services;
using Moq;

namespace ApplicationCoreTest.Services.VehicleServiceTest
{
    [TestClass]
    public sealed class T01_VehicleServiceTest_GetAll
    {
        [TestMethod]
        public void GetAll_OVehicle_ReturnEmptyList()
        {
            var mockVehicleRepository = new Mock<IVehicleRepository>(MockBehavior.Strict);
            mockVehicleRepository.Setup(x => x.GetAll()).ReturnsAsync(new List<Vehicle>());

            var repository = new VehicleService(mockVehicleRepository.Object);

            var result = repository.GetAll().Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());

            mockVehicleRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [TestMethod]
        public void GetAll_2Vehicles_Return2Vehicle()
        {
            var mockVehicleRepository = new Mock<IVehicleRepository>(MockBehavior.Strict);
            mockVehicleRepository.Setup(x => x.GetAll()).ReturnsAsync(new List<Vehicle>
            {
                new Vehicle
                {
                    Id = 1,
                    Chassis = new Chassis
                    {
                        Brand = "Renault",
                        Name = "Megane",
                        Price = 12000
                    },
                    Engine = new Engine
                    {
                        Id = 2,
                        Horsepower = 100,
                        Price = 8000,
                        Type = Enums.EngineType.Petrol
                    },
                    Options = new List<Option>
                    {
                        new Option
                        {
                            Id = 3,
                            Name= "GPS",
                            Price = 200
                        }
                    }
                },
                new Vehicle
                {
                    Id = 4,
                    Chassis = new Chassis
                    {
                        Brand = "Peugeot",
                        Name = "208",
                        Price = 13000
                    },
                    Engine = new Engine
                    {
                        Id = 5,
                        Horsepower = 110,
                        Price = 9000,
                        Type = Enums.EngineType.Diesel
                    },
                    Options = new List<Option>
                    {
                        new Option
                        {
                            Id = 6,
                            Name= "GPS",
                            Price = 300
                        },
                        new Option
                        {
                            Id = 7,
                            Name= "Climatisation",
                            Price = 400
                        }
                    }
                }
            });

            var repository = new VehicleService(mockVehicleRepository.Object);

            var result = repository.GetAll().Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            var vehicleRenault = result[0];
            Assert.IsNotNull(vehicleRenault);
            Assert.AreEqual(1, vehicleRenault.Id);
            Assert.AreEqual("Renault", vehicleRenault.Chassis.Brand);
            Assert.AreEqual("Megane", vehicleRenault.Chassis.Name);
            Assert.AreEqual(12000, vehicleRenault.Chassis.Price);
            var engineRenault = vehicleRenault.Engine;
            Assert.IsNotNull(engineRenault);
            Assert.AreEqual(2, engineRenault.Id);
            Assert.AreEqual(100, engineRenault.Horsepower);
            Assert.AreEqual(8000, engineRenault.Price);
            Assert.AreEqual(Enums.EngineType.Petrol, engineRenault.Type);
            Assert.IsNotNull(vehicleRenault.Options);
            var optionRenault = vehicleRenault.Options[0];
            Assert.IsNotNull(optionRenault);
            Assert.AreEqual(3, optionRenault.Id);
            Assert.AreEqual("GPS", optionRenault.Name);
            Assert.AreEqual(200, optionRenault.Price);

            var vehiclePeugeot = result[1];
            Assert.IsNotNull(vehiclePeugeot);
            Assert.AreEqual(4, vehiclePeugeot.Id);
            Assert.AreEqual("Peugeot", vehiclePeugeot.Chassis.Brand);
            Assert.AreEqual("208", vehiclePeugeot.Chassis.Name);
            Assert.AreEqual(13000, vehiclePeugeot.Chassis.Price);
            var enginePeugeot = vehiclePeugeot.Engine;
            Assert.IsNotNull(enginePeugeot);
            Assert.AreEqual(5, enginePeugeot.Id);
            Assert.AreEqual(110, enginePeugeot.Horsepower);
            Assert.AreEqual(9000, enginePeugeot.Price);
            Assert.AreEqual(Enums.EngineType.Diesel, enginePeugeot.Type);
            Assert.IsNotNull(vehiclePeugeot.Options);
            var optionPeugeot1 = vehiclePeugeot.Options[0];
            Assert.IsNotNull(optionPeugeot1);
            Assert.AreEqual(6, optionPeugeot1.Id);
            Assert.AreEqual("GPS", optionPeugeot1.Name);
            Assert.AreEqual(300, optionPeugeot1.Price);
            var optionPeugeot2 = vehiclePeugeot.Options[1];
            Assert.IsNotNull(optionPeugeot2);
            Assert.AreEqual(7, optionPeugeot2.Id);
            Assert.AreEqual("Climatisation", optionPeugeot2.Name);
            Assert.AreEqual(400, optionPeugeot2.Price);

            mockVehicleRepository.Verify(x => x.GetAll(), Times.Once);
        }
    }
}
