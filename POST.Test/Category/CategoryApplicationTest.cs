using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POS.Application.Dtos.Request;
using POS.Application.Interfaces;
using POS.Utilities.Static;

namespace POST.Test.Category
{
    [TestClass]
    public class CategoryApplicationTest
    {
        private static WebApplicationFactory<Program>? _factory = null;
        private static IServiceScopeFactory? _scopeFactory = null;

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            _factory = new CustomWebApplicationFactory();
            _scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
        }

        [TestMethod]
        //Cuando se envie nulos
        public async Task Registercategory_WhensendingNullvaluesOrEmpty_ValidationErrors()
        {
            using var scope = _scopeFactory?.CreateScope();
            var context = scope?.ServiceProvider.GetService<ICategoryApplication>();

            //Preparar una solicitud
            //Arrange
            var name = "";
            var descrption = "";
            var state = 1;
            var expected = ReplyMessager.MESSAGE_VALIDATE;

            //Act
            var result = await context.RegisterCategory(new CategoryRequestDTO
            {
                Name = name,
                Description = descrption,
                State = state
            });
            var current = result.Message;

            //Assert
            Assert.AreEqual(expected, current);
        }

        [TestMethod]
        public async Task RegisterCategory_WhenSendingCorrectValues_RegisteredSuccessFully()
        {
            using var scope = _scopeFactory?.CreateScope();
            var context = scope?.ServiceProvider.GetService<ICategoryApplication>();

            //Preparar una solicitud
            //Arrange
            var name = "Nuevo registro";
            var descrption = "Nueva descripcion";
            var state = 1;
            var expected = ReplyMessager.MESSAGE_SAVE;

            //Act
            var result = await context.RegisterCategory(new CategoryRequestDTO
            {
                Name = name,
                Description = descrption,
                State = state
            });
            var current = result.Message;

            //Assert
            Assert.AreEqual(expected, current);
        }
    }
}
