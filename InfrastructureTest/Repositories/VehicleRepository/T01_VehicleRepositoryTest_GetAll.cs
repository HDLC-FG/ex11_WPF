using ApplicationCore;
using ApplicationCore.Models;
using Effort;
using Infrastructure;
using Infrastructure.Repositories;

namespace InfrastructureTest.Repositories
{
    [TestClass]
    public sealed class T01_VehicleRepositoryTest_GetAll
    {
        [TestMethod]
        public void GetAll_0Vehicles_ReturnEmptyList()
        {
            using (var connection = DbConnectionFactory.CreateTransient())
            {
                using (var dbContext = new ApplicationDbContext(connection, true))
                {
                    var repository = new VehicleRepository(dbContext);

                    var result = repository.GetAll().Result;

                    Assert.IsNotNull(result);
                    Assert.AreEqual(0, result.Count());
                }
            }
        }

        [TestMethod]
        public void GetAll_2Vehicles_Return2Vehicles()
        {
            using (var connection = DbConnectionFactory.CreateTransient())
            {
                using (var dbContext = new ApplicationDbContext(connection, true))
                {
                    dbContext.Vehicles.Add(new Vehicle
                    {
                        Brand = "Renault",
                        Name = "Megane",
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
                        },
                        Price = 12000
                    });
                    dbContext.Vehicles.Add(new Vehicle
                    {
                        Brand = "Peugeot",
                        Name = "208",
                        Engine = new Engine
                        {
                            Horsepower = 110,
                            Price = 9000,
                            Type = Enums.EngineType.Diesel
                        },
                        Options = new List<Option>
                        {
                            new Option
                            {
                                Name= "GPS",
                                Price = 300
                            },
                            new Option
                            {
                                Name= "Climatisation",
                                Price = 400
                            }
                        },
                        Price = 13000
                    });
                    dbContext.SaveChanges();
                    var repository = new VehicleRepository(dbContext);

                    var result = repository.GetAll().Result;

                    Assert.IsNotNull(result);
                    Assert.AreEqual(2, result.Count());
                    var vehicleRenault = result[0];
                    Assert.IsNotNull(vehicleRenault);
                    Assert.AreEqual(1, vehicleRenault.Id);
                    Assert.AreEqual("Renault", vehicleRenault.Brand);
                    Assert.AreEqual("Megane", vehicleRenault.Name);
                    var engineRenault = vehicleRenault.Engine;
                    Assert.IsNotNull(engineRenault);
                    Assert.AreEqual(1, engineRenault.Id);
                    Assert.AreEqual(100, engineRenault.Horsepower);
                    Assert.AreEqual(8000, engineRenault.Price);
                    Assert.AreEqual(Enums.EngineType.Petrol, engineRenault.Type);
                    Assert.IsNotNull(vehicleRenault.Options);
                    var optionRenault = vehicleRenault.Options[0];
                    Assert.IsNotNull(optionRenault);
                    Assert.AreEqual(1, optionRenault.Id);
                    Assert.AreEqual("GPS", optionRenault.Name);
                    Assert.AreEqual(200, optionRenault.Price);
                    Assert.AreEqual(12000, vehicleRenault.Price);
                    Assert.AreEqual(20200, vehicleRenault.TotalPrice);

                    var vehiclePeugeot = result[1];
                    Assert.IsNotNull(vehiclePeugeot);
                    Assert.AreEqual(2, vehiclePeugeot.Id);
                    Assert.AreEqual("Peugeot", vehiclePeugeot.Brand);
                    Assert.AreEqual("208", vehiclePeugeot.Name);
                    var enginePeugeot = vehiclePeugeot.Engine;
                    Assert.IsNotNull(enginePeugeot);
                    Assert.AreEqual(2, enginePeugeot.Id);
                    Assert.AreEqual(110, enginePeugeot.Horsepower);
                    Assert.AreEqual(9000, enginePeugeot.Price);
                    Assert.AreEqual(Enums.EngineType.Diesel, enginePeugeot.Type);
                    Assert.IsNotNull(vehiclePeugeot.Options);
                    var optionPeugeot1 = vehiclePeugeot.Options[0];
                    Assert.IsNotNull(optionPeugeot1);
                    Assert.AreEqual(2, optionPeugeot1.Id);
                    Assert.AreEqual("GPS", optionPeugeot1.Name);
                    Assert.AreEqual(300, optionPeugeot1.Price);
                    var optionPeugeot2 = vehiclePeugeot.Options[1];
                    Assert.IsNotNull(optionPeugeot2);
                    Assert.AreEqual(3, optionPeugeot2.Id);
                    Assert.AreEqual("Climatisation", optionPeugeot2.Name);
                    Assert.AreEqual(400, optionPeugeot2.Price);
                    Assert.AreEqual(13000, vehiclePeugeot.Price);
                    Assert.AreEqual(22700, vehiclePeugeot.TotalPrice);
                }
            }
        }
    }
}
