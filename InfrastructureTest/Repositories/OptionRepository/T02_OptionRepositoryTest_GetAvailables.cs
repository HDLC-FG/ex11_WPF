using ApplicationCore.Models;
using Effort;
using Infrastructure;
using Infrastructure.Repositories;

namespace InfrastructureTest.Repositories
{
    [TestClass]
    public sealed class T02_OptionRepositoryTest_GetAvailables
    {
        [TestMethod]
        public void GetAvailables_2options_FirtOptionNOTAvailable_ReturnSecondOptionAvailable()
        {
            using (var connection = DbConnectionFactory.CreateTransient())
            {
                using (var dbContext = new ApplicationDbContext(connection, true))
                {
                    var option1 = new Option
                    {
                        Name = "GPS",
                        Price = 200
                    };
                    var option2 = new Option
                    {
                        Name = "Climatisation",
                        Price = 400
                    };
                    dbContext.Options.Add(option1);
                    dbContext.Options.Add(option2);
                    dbContext.SaveChanges();

                    var optionsNotAvailable = new List<Option>
                    {
                        option1
                    };
                    var repository = new OptionRepository(dbContext);

                    var result = repository.GetAvailables(optionsNotAvailable).Result;

                    Assert.IsNotNull(result);
                    Assert.AreEqual(1, result.Count());
                    var vehicleAvailable = result[0];
                    Assert.IsNotNull(vehicleAvailable);
                    Assert.AreEqual(2, vehicleAvailable.Id);
                    Assert.AreEqual("Climatisation", vehicleAvailable.Name);
                    Assert.AreEqual(400, vehicleAvailable.Price);
                }
            }
        }
    }
}
