using Effort;
using Infrastructure.Repositories;
using Infrastructure;
using ApplicationCore.Models;
using ApplicationCore;

namespace InfrastructureTest.Repositories.VehicleRepositoryTest
{
    [TestClass]
    public sealed class T04_VehicleRepositoryTest_Delete
    {
        [TestMethod]
        public void Delete_VehicleExist_ReturnOk()
        {
            using (var connection = DbConnectionFactory.CreateTransient())
            {
                using (var dbContext = new ApplicationDbContext(connection, true))
                {
                    var vehicle = new Vehicle
                    {
                        Chassis = new Chassis
                        {
                            Name = "Megane",
                            Brand = "Renault",
                            Price = 12000
                        },
                        Engine = new Engine
                        {
                            Horsepower = 130,
                            Price = 8000,
                            Type = Enums.EngineType.Petrol
                        },
                        Options = new List<Option>
                        {
                            new Option
                            {
                                Name = "GPS",
                                Price = 400
                            },
                            new Option
                            {
                                Name = "Siege chauffant",
                                Price = 600
                            }
                        }
                    };
                    dbContext.Vehicles.Add(vehicle);
                    dbContext.SaveChanges();

                    var repository = new VehicleRepository(dbContext);

                    repository.Delete(vehicle).Wait();

                    var vehiclesDB = dbContext.Vehicles.ToList();
                    Assert.IsNotNull(vehiclesDB);
                    Assert.AreEqual(0, vehiclesDB.Count);

                    var enginesDB = dbContext.Engines.ToList();
                    Assert.IsNotNull(enginesDB);
                    Assert.AreEqual(1, enginesDB.Count);
                    var engineDB = enginesDB[0];
                    Assert.IsNotNull(engineDB);
                    Assert.AreEqual(130, engineDB.Horsepower);
                    Assert.AreEqual(8000, engineDB.Price);
                    Assert.AreEqual(Enums.EngineType.Petrol, engineDB.Type);

                    var optionsDB = dbContext.Options.ToList();
                    Assert.IsNotNull(optionsDB);
                    Assert.AreEqual(2, optionsDB.Count);
                    var optionDB1 = optionsDB[0];
                    Assert.IsNotNull(optionDB1);
                    Assert.AreEqual("GPS", optionDB1.Name);
                    Assert.AreEqual(400, optionDB1.Price);
                    var optionDB2 = optionsDB[1];
                    Assert.IsNotNull(optionDB2);
                    Assert.AreEqual("Siege chauffant", optionDB2.Name);
                    Assert.AreEqual(600, optionDB2.Price);
                }
            }
        }
    }
}
