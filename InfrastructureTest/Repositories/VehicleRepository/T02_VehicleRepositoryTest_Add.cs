using ApplicationCore;
using ApplicationCore.Models;
using Effort;
using Infrastructure;
using Infrastructure.Repositories;

namespace InfrastructureTest.Repositories
{
    [TestClass]
    public sealed class T02_VehicleRepositoryTest_Add
    {
        [TestMethod]
        public void Add_Vehicle_ReturnOk()
        {
            using (var connection = DbConnectionFactory.CreateTransient())
            {
                using (var dbContext = new ApplicationDbContext(connection, true))
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
                    var repository = new VehicleRepository(dbContext);

                    repository.Add(vehicle).Wait();

                    var result = dbContext.Vehicles.ToList();
                    Assert.IsNotNull(result);
                    Assert.AreEqual(1, result.Count());
                    var vehicleAdded = result[0];
                    Assert.IsNotNull(vehicleAdded);
                    Assert.AreEqual(1, vehicleAdded.Id);
                    Assert.AreEqual("Renault", vehicleAdded.Chassis.Brand);
                    Assert.AreEqual("Megane", vehicleAdded.Chassis.Name);
                    Assert.AreEqual(12000, vehicleAdded.Chassis.Price);
                    var engineAdded = vehicleAdded.Engine;
                    Assert.IsNotNull(engineAdded);
                    Assert.AreEqual(1, engineAdded.Id);
                    Assert.AreEqual(100, engineAdded.Horsepower);
                    Assert.AreEqual(8000, engineAdded.Price);
                    Assert.AreEqual(Enums.EngineType.Petrol, engineAdded.Type);
                    Assert.IsNotNull(vehicleAdded.Options);
                    var optionAdded = vehicleAdded.Options[0];
                    Assert.IsNotNull(optionAdded);
                    Assert.AreEqual(1, optionAdded.Id);
                    Assert.AreEqual("GPS", optionAdded.Name);
                    Assert.AreEqual(200, optionAdded.Price);
                    Assert.AreEqual(12000, vehicleAdded.Price);
                    Assert.AreEqual(20200, vehicleAdded.TotalPrice);
                }
            }
        }
    }
}
