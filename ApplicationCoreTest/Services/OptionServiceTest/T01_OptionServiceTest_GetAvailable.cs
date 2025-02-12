using ApplicationCore.Models;
using ApplicationCore.Services;
using Infrastructure.Repositories;
using Moq;

namespace ApplicationCoreTest.Services.OptionServiceTest
{
    [TestClass]
    public sealed class T01_OptionServiceTest_GetAvailable
    {
        [TestMethod]
        public void GetAvailable_OptionsNotAvailableIsAny_ReturnOptionsAvailable()
        {
            var mockOptionRepository = new Mock<IOptionRepository>(MockBehavior.Strict);
            mockOptionRepository.Setup(x => x.GetAvailables(It.IsAny<IList<Option>>())).ReturnsAsync(new List<Option> 
            { 
                new Option
                {
                    Id = 1,
                    Name = "GPS",
                    Price = 300
                }
            });

            var repository = new OptionService(mockOptionRepository.Object);

            var result = repository.GetAvailables(It.IsAny<IList<Option>>()).Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            var option = result[0];
            Assert.IsNotNull(option);
            Assert.AreEqual(1, option.Id);
            Assert.AreEqual("GPS", option.Name);
            Assert.AreEqual(300, option.Price);

            mockOptionRepository.Verify(x => x.GetAvailables(It.IsAny<IList<Option>>()), Times.Once);
        }
    }
}
