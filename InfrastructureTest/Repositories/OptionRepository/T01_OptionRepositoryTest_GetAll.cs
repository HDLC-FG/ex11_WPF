using ApplicationCore.Models;
using Effort;
using Infrastructure;
using Infrastructure.Repositories;

namespace InfrastructureTest.Repositories
{
    [TestClass]
    public sealed class T01_OptionRepositoryTest_GetAll
    {
        [TestMethod]
        public void GetAll_0Options_ReturnEmptyList()
        {
            using (var connection = DbConnectionFactory.CreateTransient())
            {
                using (var dbContext = new ApplicationDbContext(connection, true))
                {
                    var repository = new OptionRepository(dbContext);

                    var result = repository.GetAll().Result;

                    Assert.IsNotNull(result);
                    Assert.AreEqual(0, result.Count());
                }
            }
        }

        [TestMethod]
        public void GetAll_2Options_Return2Options()
        {
            using (var connection = DbConnectionFactory.CreateTransient())
            {
                using (var dbContext = new ApplicationDbContext(connection, true))
                {
                    dbContext.Options.Add(new Option
                    {
                        Name = "GPS",
                        Price = 200
                    });
                    dbContext.Options.Add(new Option
                    {
                        Name = "Climatisation",
                        Price = 400
                    });
                    dbContext.SaveChanges();
                    var repository = new OptionRepository(dbContext);

                    var result = repository.GetAll().Result;

                    Assert.IsNotNull(result);
                    Assert.AreEqual(2, result.Count());
                    var vehicleOption1 = result[0];
                    Assert.IsNotNull(vehicleOption1);
                    Assert.AreEqual(1, vehicleOption1.Id);
                    Assert.AreEqual("GPS", vehicleOption1.Name);
                    Assert.AreEqual(200, vehicleOption1.Price);

                    var vehicleOption2 = result[1];
                    Assert.IsNotNull(vehicleOption2);
                    Assert.AreEqual(2, vehicleOption2.Id);
                    Assert.AreEqual("Climatisation", vehicleOption2.Name);
                    Assert.AreEqual(400, vehicleOption2.Price);
                }
            }
        }
    }
}
