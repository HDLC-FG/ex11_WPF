using ApplicationCore.Models;
using ApplicationCore;
using Effort;
using Infrastructure.Repositories;
using Infrastructure;

namespace InfrastructureTest.Repositories
{
    [TestClass]
    public sealed class T03_VehicleRepositoryTest_Update
    {
        [TestMethod]
        public void Update_Vehicle_ReturnOk()
        {
            using (var connection = DbConnectionFactory.CreateTransient())
            {
                using (var dbContext = new ApplicationDbContext(connection, true))
                {
                    var addVehicle = new Vehicle
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
                    };
                    dbContext.Vehicles.Add(addVehicle);
                    dbContext.SaveChanges();

                    var repository = new VehicleRepository(dbContext);

                    addVehicle.Brand = "Peugeot";
                    addVehicle.Name = "208";
                    addVehicle.Engine.Horsepower = 120;
                    addVehicle.Engine.Price = 9000;
                    addVehicle.Engine.Type = Enums.EngineType.Diesel;
                    addVehicle.Options[0].Name = "Climatisation";
                    addVehicle.Options[0].Price = 400;
                    addVehicle.Price = 13000;

                    repository.Update(addVehicle).Wait();

                    var result = dbContext.Vehicles.ToList();
                    Assert.IsNotNull(result);
                    Assert.AreEqual(1, result.Count());
                    var vehicleUpdated = result[0];
                    Assert.IsNotNull(vehicleUpdated);
                    Assert.AreEqual(1, vehicleUpdated.Id);
                    Assert.AreEqual("Peugeot", vehicleUpdated.Brand);
                    Assert.AreEqual("208", vehicleUpdated.Name);
                    var engineUpdated = vehicleUpdated.Engine;
                    Assert.IsNotNull(engineUpdated);
                    Assert.AreEqual(1, engineUpdated.Id);
                    Assert.AreEqual(120, engineUpdated.Horsepower);
                    Assert.AreEqual(9000, engineUpdated.Price);
                    Assert.AreEqual(Enums.EngineType.Diesel, engineUpdated.Type);
                    Assert.IsNotNull(vehicleUpdated.Options);
                    var optionUpdated = vehicleUpdated.Options[0];
                    Assert.IsNotNull(optionUpdated);
                    Assert.AreEqual(1, optionUpdated.Id);
                    Assert.AreEqual("Climatisation", optionUpdated.Name);
                    Assert.AreEqual(400, optionUpdated.Price);
                    Assert.AreEqual(13000, vehicleUpdated.Price);
                    Assert.AreEqual(22400, vehicleUpdated.TotalPrice);
                }
            }
        }
    }
}
