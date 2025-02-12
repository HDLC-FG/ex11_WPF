using Effort;
using Infrastructure.Repositories;
using Infrastructure;
using ApplicationCore.Models;

namespace InfrastructureTest.Repositories.ChassisRepositoryTest
{
    [TestClass]
    public sealed class T01_ChassisRepositoryTest_GetAll
    {
        [TestMethod]
        public void GetAll_0Chassis_ReturnEmptyList()
        {
            using (var connection = DbConnectionFactory.CreateTransient())
            {
                using (var dbContext = new ApplicationDbContext(connection, true))
                {
                    var repository = new ChassisRepository(dbContext);

                    var result = repository.GetAll().Result;

                    Assert.IsNotNull(result);
                    Assert.AreEqual(0, result.Count());
                }
            }
        }

        [TestMethod]
        public void GetAll_2Chassis_ReturnOk()
        {
            using (var connection = DbConnectionFactory.CreateTransient())
            {
                using (var dbContext = new ApplicationDbContext(connection, true))
                {
                    dbContext.Chassis.Add(new Chassis
                    {
                        Name = "Megane",
                        Brand = "Renault",
                        Price = 12000
                    });

                    dbContext.Chassis.Add(new Chassis
                    {
                        Name = "208",
                        Brand = "Peugeot",
                        Price = 14000
                    });
                    dbContext.SaveChanges();
                    var repository = new ChassisRepository(dbContext);

                    var result = repository.GetAll().Result;

                    Assert.IsNotNull(result);
                    Assert.AreEqual(2, result.Count());
                    var chassis1 = result[0];
                    Assert.IsNotNull(chassis1);
                    Assert.AreEqual("Megane", chassis1.Name);
                    Assert.AreEqual("Renault", chassis1.Brand);
                    Assert.AreEqual(12000, chassis1.Price);
                    var chassis2 = result[1];
                    Assert.IsNotNull(chassis2);
                    Assert.AreEqual("208", chassis2.Name);
                    Assert.AreEqual("Peugeot", chassis2.Brand);
                    Assert.AreEqual(14000, chassis2.Price);
                }
            }
        }
    }
}
