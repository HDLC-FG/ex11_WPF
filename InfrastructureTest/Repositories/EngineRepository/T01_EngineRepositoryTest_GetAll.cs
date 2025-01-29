using ApplicationCore;
using ApplicationCore.Models;
using Effort;
using Infrastructure;
using Infrastructure.Repositories;

namespace InfrastructureTest.Repositories
{
    [TestClass]
    public sealed class T01_EngineRepositoryTest_GetAll
    {
        [TestMethod]
        public void GetAll_0Engines_ReturnEmptyList()
        {
            using (var connection = DbConnectionFactory.CreateTransient())
            {
                using (var dbContext = new ApplicationDbContext(connection, true))
                {
                    var repository = new EngineRepository(dbContext);

                    var result = repository.GetAll().Result;

                    Assert.IsNotNull(result);
                    Assert.AreEqual(0, result.Count());
                }
            }
        }

        [TestMethod]
        public void GetAll_2Engines_Return2Engines()
        {
            using (var connection = DbConnectionFactory.CreateTransient())
            {
                using (var dbContext = new ApplicationDbContext(connection, true))
                {
                    dbContext.Engines.Add(new Engine
                    {
                        Horsepower = 100,
                        Price = 8000,
                        Type = Enums.TypeEngine.Petrol
                    });
                    dbContext.Engines.Add(new Engine
                    {
                        Horsepower = 110,
                        Price = 9000,
                        Type = Enums.TypeEngine.Diesel
                    });
                    dbContext.SaveChanges();
                    var repository = new EngineRepository(dbContext);

                    var result = repository.GetAll().Result;

                    Assert.IsNotNull(result);
                    Assert.AreEqual(2, result.Count());
                    var engine1 = result[0];
                    Assert.AreEqual(1, engine1.Id);
                    Assert.AreEqual(100, engine1.Horsepower);
                    Assert.AreEqual(8000, engine1.Price);
                    Assert.AreEqual(Enums.TypeEngine.Petrol, engine1.Type);

                    var engine2 = result[1];
                    Assert.IsNotNull(engine2);
                    Assert.AreEqual(2, engine2.Id);
                    Assert.AreEqual(110, engine2.Horsepower);
                    Assert.AreEqual(9000, engine2.Price);
                    Assert.AreEqual(Enums.TypeEngine.Diesel, engine2.Type);
                }
            }
        }
    }
}
