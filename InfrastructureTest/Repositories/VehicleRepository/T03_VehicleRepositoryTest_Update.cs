using System.Data.Entity;
using ApplicationCore;
using ApplicationCore.Models;
using Effort;
using Infrastructure;
using Infrastructure.Repositories;

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

        [TestMethod]
        public void Update_AddExistingOption_ReturnOkAndDontCreateNewDbContextOption()
        {
            using (var connection = DbConnectionFactory.CreateTransient())
            {
                using (var dbContext = new ApplicationDbContext(connection, true))
                {
                    var addVehicle = new Vehicle
                    {
                        Name = "Megane",
                        Engine = new Engine(),
                        Options = new List<Option>
                        {
                            new Option
                            {
                                Name= "GPS",
                                Price = 200
                            }
                        }
                    };
                    dbContext.Vehicles.Add(addVehicle);
                    dbContext.SaveChanges();

                    var newOptionsAvailable = new Option
                    {
                        Name = "Climatisation",
                        Price = 400
                    };
                    dbContext.Options.Add(newOptionsAvailable);
                    dbContext.SaveChanges();

                    var optionsAvailable = dbContext.Options.ToList();
                    addVehicle.Name = "208";
                    addVehicle.Options.Add(optionsAvailable[1]);

                    var repository = new VehicleRepository(dbContext);

                    repository.Update(addVehicle).Wait();

                    var result = dbContext.Vehicles.ToList();
                    Assert.IsNotNull(result);
                    Assert.AreEqual(1, result.Count());
                    var vehicleUpdated = result[0];
                    Assert.IsNotNull(vehicleUpdated);
                    Assert.AreEqual(1, vehicleUpdated.Id);
                    Assert.AreEqual("208", vehicleUpdated.Name);

                    Assert.IsNotNull(vehicleUpdated.Options);
                    Assert.AreEqual(2, vehicleUpdated.Options.Count);

                    var originalOption = vehicleUpdated.Options[0];
                    Assert.IsNotNull(originalOption);
                    Assert.AreEqual(1, originalOption.Id);
                    Assert.AreEqual("GPS", originalOption.Name);
                    Assert.AreEqual(200, originalOption.Price);

                    var newOption = vehicleUpdated.Options[1];
                    Assert.IsNotNull(newOption);
                    Assert.AreEqual(2, newOption.Id);
                    Assert.AreEqual("Climatisation", newOption.Name);
                    Assert.AreEqual(400, newOption.Price);

                    Assert.AreEqual(2, dbContext.Options.ToList().Count);
                }
            }
        }

        [TestMethod]
        public void Update_UpdateEngine_RemoveModifyUpdateOptions_ReturnOkWithNoNewOption()
        {
            using (var connection = DbConnectionFactory.CreateTransient())
            {
                using (var dbContext = new ApplicationDbContext(connection, true))
                {
                    var addVehicle = new Vehicle
                    {
                        Id = 1,
                        Brand = "Renault",
                        Name = "Megane",
                        Engine = new Engine
                        {
                            Id = 1,
                            Horsepower = 100,
                            Price = 5000,
                            Type = Enums.EngineType.Petrol
                        },
                        Options = new List<Option>
                        {
                            new Option
                            {
                                Id = 1,
                                Name= "GPS",
                                Price = 200
                            },
                            new Option
                            {
                                Id = 2,
                                Name= "Caméra de recul",
                                Price = 1000
                            }
                        }
                    };
                    dbContext.Vehicles.Add(addVehicle);
                    dbContext.SaveChanges();

                    var newOptionsAvailable = new Option
                    {
                        Id = 3,
                        Name = "Climatisation",
                        Price = 400
                    };
                    dbContext.Options.Add(newOptionsAvailable);
                    dbContext.SaveChanges();

                    var newEngineAvailable = new Engine
                    {
                        Id = 2,
                        Horsepower = 120,
                        Price = 8000,
                        Type = Enums.EngineType.Electric
                    };
                    dbContext.Engines.Add(newEngineAvailable);
                    dbContext.SaveChanges();
                    //Detached all previous entities
                    foreach (var entry in dbContext.ChangeTracker.Entries())
                    {
                        entry.State = EntityState.Detached;
                    }

                    var vehicleRepository = new VehicleRepository(dbContext);
                    var vehicles = vehicleRepository.GetAll().Result;
                    var vehicle = vehicles.FirstOrDefault();

                    var optionRepository = new OptionRepository(dbContext);
                    var optionsAvailable = optionRepository.GetAvailables(vehicle.Options).Result;

                    //Remove + Modify + Add options
                    vehicle.Options.Remove(vehicle.Options.First());
                    vehicle.Options.First().Name = "Nouvelle caméra de recul";
                    vehicle.Options.First().Price = 1200;
                    vehicle.Options.Add(optionsAvailable.FirstOrDefault());

                    //Update engine
                    var engineRepository = new EngineRepository(dbContext);
                    var engines = engineRepository.GetAll().Result;
                    vehicle.Engine = engines[1];

                    vehicleRepository.Update(vehicle).Wait();

                    var enginesBDD = engineRepository.GetAll().Result;
                    Assert.AreEqual(2, enginesBDD.Count);

                    var optionsBDD = optionRepository.GetAll().Result;
                    Assert.AreEqual(3, optionsBDD.Count);

                    var vehicleBDD = vehicleRepository.GetAll().Result.FirstOrDefault();
                    Assert.IsNotNull(vehicleBDD);
                    Assert.AreEqual("Renault", vehicleBDD.Brand);
                    Assert.AreEqual("Megane", vehicleBDD.Name);

                    var engine = vehicleBDD.Engine;
                    Assert.AreEqual(120, engine.Horsepower);
                    Assert.AreEqual(8000, engine.Price);
                    Assert.AreEqual(Enums.EngineType.Electric, engine.Type);

                    Assert.IsNotNull(vehicleBDD.Options);
                    Assert.AreEqual(2, vehicleBDD.Options.Count);
                    var option1 = vehicleBDD.Options[0];
                    Assert.AreEqual("Nouvelle caméra de recul", option1.Name);
                    Assert.AreEqual(1200, option1.Price);
                    var option2 = vehicleBDD.Options[1];
                    Assert.AreEqual("Climatisation", option2.Name);
                    Assert.AreEqual(400, option2.Price);
                }
            }
        }
    }
}
