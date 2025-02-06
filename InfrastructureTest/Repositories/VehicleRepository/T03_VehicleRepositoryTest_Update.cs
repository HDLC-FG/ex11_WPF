using System.Collections.ObjectModel;
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
        public void Update_VehicleFields_ReturnOk()
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
                        Options = new ObservableCollection<Option>
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
        public void Update_UpdateEngine_ReturnOk()
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
                        Options = new ObservableCollection<Option>()
                    };
                    dbContext.Vehicles.Add(addVehicle);
                    dbContext.SaveChanges();

                    dbContext.Engines.Add(new Engine
                    {
                        Id = 2,
                        Horsepower = 120,
                        Price = 8000,
                        Type = Enums.EngineType.Electric
                    });
                    dbContext.SaveChanges();
                    //Detached all previous entities
                    foreach (var entry in dbContext.ChangeTracker.Entries())
                    {
                        entry.State = EntityState.Detached;
                    }

                    var vehicle = dbContext.Vehicles.Include(v => v.Engine).Include(v => v.Options).FirstOrDefault();
                    var engines = dbContext.Engines.ToList();
                    vehicle.Engine = engines[1];

                    var vehicleRepository = new VehicleRepository(dbContext);

                    vehicleRepository.Update(vehicle).Wait();

                    var enginesBDD = dbContext.Engines.ToList();
                    Assert.AreEqual(2, enginesBDD.Count);
                    var engineBDD1 = enginesBDD[0];
                    Assert.AreEqual(1, engineBDD1.Id);
                    Assert.AreEqual(100, engineBDD1.Horsepower);
                    Assert.AreEqual(5000, engineBDD1.Price);
                    Assert.AreEqual(Enums.EngineType.Petrol, engineBDD1.Type);
                    var engineBDD2 = enginesBDD[1];
                    Assert.AreEqual(2, engineBDD2.Id);
                    Assert.AreEqual(120, engineBDD2.Horsepower);
                    Assert.AreEqual(8000, engineBDD2.Price);
                    Assert.AreEqual(Enums.EngineType.Electric, engineBDD2.Type);

                    var vehiclesBDD = dbContext.Vehicles.ToList();
                    Assert.IsNotNull(vehiclesBDD);
                    Assert.AreEqual(1, vehiclesBDD.Count);

                    var vehicleBDD = vehiclesBDD[0];
                    Assert.AreEqual(1, vehicleBDD.Id);
                    Assert.AreEqual("Renault", vehicleBDD.Brand);
                    Assert.AreEqual("Megane", vehicleBDD.Name);

                    var engine = vehicleBDD.Engine;
                    Assert.AreEqual(2, engine.Id);
                    Assert.AreEqual(120, engine.Horsepower);
                    Assert.AreEqual(8000, engine.Price);
                    Assert.AreEqual(Enums.EngineType.Electric, engine.Type);

                    Assert.IsNotNull(vehicleBDD.Options);
                    Assert.AreEqual(0, vehicleBDD.Options.Count);
                }
            }
        }

        [TestMethod]
        public void Update_RemoveOption_ReturnOk()
        {
            using (var connection = DbConnectionFactory.CreateTransient())
            {
                using (var dbContext = new ApplicationDbContext(connection, true))
                {
                    dbContext.Vehicles.Add(new Vehicle
                    {
                        Id = 1,
                        Brand = "Renault",
                        Name = "Megane",
                        Engine = new Engine(),
                        Options = new ObservableCollection<Option>
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
                    });
                    dbContext.SaveChanges();
                    //Detached all previous entities
                    foreach (var entry in dbContext.ChangeTracker.Entries())
                    {
                        entry.State = EntityState.Detached;
                    }

                    var model = dbContext.Vehicles.Include(v => v.Engine).Include(v => v.Options).FirstOrDefault();
                    model.Options.Remove(model.Options.First());

                    var vehicleRepository = new VehicleRepository(dbContext);

                    vehicleRepository.Update(model).Wait();

                    var optionsBDD = dbContext.Options.ToList();
                    Assert.AreEqual(2, optionsBDD.Count);
                    var optionBDD1 = optionsBDD[0];
                    Assert.AreEqual(1, optionBDD1.Id);
                    Assert.AreEqual("GPS", optionBDD1.Name);
                    Assert.AreEqual(200, optionBDD1.Price);
                    var optionBDD2 = optionsBDD[1];
                    Assert.AreEqual(2, optionBDD2.Id);
                    Assert.AreEqual("Caméra de recul", optionBDD2.Name);
                    Assert.AreEqual(1000, optionBDD2.Price);

                    var vehiclesBDD = dbContext.Vehicles.ToList();
                    Assert.AreEqual(1, vehiclesBDD.Count);

                    var vehicleBDD = vehiclesBDD.FirstOrDefault();
                    Assert.IsNotNull(vehicleBDD);
                    Assert.AreEqual("Renault", vehicleBDD.Brand);
                    Assert.AreEqual("Megane", vehicleBDD.Name);

                    Assert.IsNotNull(vehicleBDD.Options);
                    Assert.AreEqual(1, vehicleBDD.Options.Count);
                    var option1 = vehicleBDD.Options[0];
                    Assert.AreEqual(2, option1.Id);
                    Assert.AreEqual("Caméra de recul", option1.Name);
                    Assert.AreEqual(1000, option1.Price);
                }
            }
        }

        [TestMethod]
        public void Update_AddOption_ReturnOk()
        {
            using (var connection = DbConnectionFactory.CreateTransient())
            {
                using (var dbContext = new ApplicationDbContext(connection, true))
                {
                    dbContext.Vehicles.Add(new Vehicle
                    {
                        Id = 1,
                        Brand = "Renault",
                        Name = "Megane",
                        Engine = new Engine(),
                        Options = new ObservableCollection<Option>
                        {
                            new Option
                            {
                                Id = 1,
                                Name= "GPS",
                                Price = 200
                            }
                        }
                    });
                    dbContext.SaveChanges();
                    //Detached all previous entities
                    foreach (var entry in dbContext.ChangeTracker.Entries())
                    {
                        entry.State = EntityState.Detached;
                    }

                    var vehicle = dbContext.Vehicles.Include(v => v.Engine).Include(v => v.Options).FirstOrDefault();
                    vehicle.Options.Add(new Option
                    {
                        Name = "Caméra de recul",
                        Price = 1000
                    });

                    var vehicleRepository = new VehicleRepository(dbContext);

                    vehicleRepository.Update(vehicle).Wait();

                    var optionsBDD = dbContext.Options.ToList();
                    Assert.AreEqual(2, optionsBDD.Count);
                    var optionBDD1 = optionsBDD[0];
                    Assert.AreEqual(1, optionBDD1.Id);
                    Assert.AreEqual("GPS", optionBDD1.Name);
                    Assert.AreEqual(200, optionBDD1.Price);
                    var optionBDD2 = optionsBDD[1];
                    Assert.AreEqual(2, optionBDD2.Id);
                    Assert.AreEqual("Caméra de recul", optionBDD2.Name);
                    Assert.AreEqual(1000, optionBDD2.Price);

                    var vehiclesBDD = dbContext.Vehicles.ToList();
                    Assert.AreEqual(1, vehiclesBDD.Count);

                    var vehicleBDD = vehiclesBDD.FirstOrDefault();
                    Assert.IsNotNull(vehicleBDD);
                    Assert.AreEqual("Renault", vehicleBDD.Brand);
                    Assert.AreEqual("Megane", vehicleBDD.Name);

                    Assert.IsNotNull(vehicleBDD.Options);
                    Assert.AreEqual(2, vehicleBDD.Options.Count);
                    var option1 = vehicleBDD.Options[0];
                    Assert.AreEqual(1, option1.Id);
                    Assert.AreEqual("GPS", option1.Name);
                    Assert.AreEqual(200, option1.Price);
                    var option2 = vehicleBDD.Options[1];
                    Assert.AreEqual(2, option2.Id);
                    Assert.AreEqual("Caméra de recul", option2.Name);
                    Assert.AreEqual(1000, option2.Price);
                }
            }
        }

        [TestMethod]
        public void Update_UpdateOption_ReturnOk()
        {
            using (var connection = DbConnectionFactory.CreateTransient())
            {
                using (var dbContext = new ApplicationDbContext(connection, true))
                {
                    dbContext.Vehicles.Add(new Vehicle
                    {
                        Id = 1,
                        Brand = "Renault",
                        Name = "Megane",
                        Engine = new Engine(),
                        Options = new ObservableCollection<Option>
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
                                Name = "Caméra de recul",
                                Price = 1000
                            }
                        }
                    });
                    dbContext.SaveChanges();

                    dbContext.Options.Add(new Option
                    {
                        Id = 3,
                        Name = "Climatisation",
                        Price = 500
                    });
                    dbContext.SaveChanges();
                    //Detached all previous entities
                    foreach (var entry in dbContext.ChangeTracker.Entries())
                    {
                        entry.State = EntityState.Detached;
                    }

                    var vehicle = dbContext.Vehicles.Include(v => v.Engine).Include(v => v.Options).FirstOrDefault();
                    var newOption = dbContext.Options.FirstOrDefault(o => o.Id == 3);

                    vehicle.Options[0] = newOption;

                    var vehicleRepository = new VehicleRepository(dbContext);

                    vehicleRepository.Update(vehicle).Wait();

                    var optionsBDD = dbContext.Options.ToList();
                    Assert.AreEqual(3, optionsBDD.Count);
                    var optionBDD1 = optionsBDD[0];
                    Assert.AreEqual(1, optionBDD1.Id);
                    Assert.AreEqual("GPS", optionBDD1.Name);
                    Assert.AreEqual(200, optionBDD1.Price);
                    var optionBDD2 = optionsBDD[1];
                    Assert.AreEqual(2, optionBDD2.Id);
                    Assert.AreEqual("Caméra de recul", optionBDD2.Name);
                    Assert.AreEqual(1000, optionBDD2.Price);
                    var optionBDD3 = optionsBDD[2];
                    Assert.AreEqual(3, optionBDD3.Id);
                    Assert.AreEqual("Climatisation", optionBDD3.Name);
                    Assert.AreEqual(500, optionBDD3.Price);

                    var vehiclesBDD = dbContext.Vehicles.ToList();
                    Assert.AreEqual(1, vehiclesBDD.Count);

                    var vehicleBDD = vehiclesBDD.FirstOrDefault();
                    Assert.IsNotNull(vehicleBDD);
                    Assert.AreEqual("Renault", vehicleBDD.Brand);
                    Assert.AreEqual("Megane", vehicleBDD.Name);

                    Assert.IsNotNull(vehicleBDD.Options);
                    Assert.AreEqual(2, vehicleBDD.Options.Count);
                    var option1 = vehicleBDD.Options[0];
                    Assert.AreEqual(3, option1.Id);
                    Assert.AreEqual("Climatisation", option1.Name);
                    Assert.AreEqual(500, option1.Price);
                    var option2 = vehicleBDD.Options[1];
                    Assert.AreEqual(2, option2.Id);
                    Assert.AreEqual("Caméra de recul", option2.Name);
                    Assert.AreEqual(1000, option2.Price);
                }
            }
        }
    }
}
