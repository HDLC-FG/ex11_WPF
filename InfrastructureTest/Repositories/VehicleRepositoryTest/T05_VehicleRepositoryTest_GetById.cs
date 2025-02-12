using ApplicationCore;
using ApplicationCore.Models;
using Effort;
using Infrastructure;
using Infrastructure.Repositories;

namespace InfrastructureTest.Repositories.VehicleRepositoryTest
{
    [TestClass]
    public class T05_VehicleRepositoryTest_GetById
    {
        [TestMethod]
        public void GetById_Id_ReturnOk()
        {
            using (var connection = DbConnectionFactory.CreateTransient())
            {
                using (var dbContext = new ApplicationDbContext(connection, true))
                {
                    dbContext.Vehicles.Add(new Vehicle
                    {
                        Chassis = new Chassis
                        {
                            Name = "Megane",
                            Brand = "Renault",
                            Price = 12000
                        },
                        Engine = new Engine
                        {
                            Horsepower = 100,
                            Price = 8000,
                            Type = Enums.EngineType.Electric
                        },
                        Options = new List<Option>
                        {
                            new Option
                            {
                                Name = "GPS",
                                Price = 300
                            }
                        }
                    });
                    dbContext.SaveChanges();
                    var repository = new VehicleRepository(dbContext);

                    var result = repository.GetById(1).Result;

                    Assert.IsNotNull(result);
                    Assert.AreEqual(1, result.Id);
                    Assert.AreEqual("Megane", result.Chassis.Name);
                    Assert.AreEqual("Renault", result.Chassis.Brand);
                    Assert.AreEqual(12000, result.Chassis.Price);
                    var engine = result.Engine;
                    Assert.IsNotNull(engine);
                    Assert.AreEqual(1, engine.Id);
                    Assert.AreEqual(100, engine.Horsepower);
                    Assert.AreEqual(8000, engine.Price);
                    Assert.AreEqual(Enums.EngineType.Electric, engine.Type);
                    Assert.IsNotNull(result.Options);
                    Assert.AreEqual(1, result.Options.Count);
                    var option = result.Options[0];
                    Assert.IsNotNull(option);
                    Assert.AreEqual(1, option.Id);
                    Assert.AreEqual("GPS", option.Name);
                    Assert.AreEqual(300, option.Price);
                }
            }
        }
    }
}
