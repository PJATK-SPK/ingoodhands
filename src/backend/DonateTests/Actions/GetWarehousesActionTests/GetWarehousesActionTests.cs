using Autofac;
using Core;
using Core.Database;
using Core.Exceptions;
using Core.Setup.Enums;
using Donate;
using Donate.Actions.DonateForm.GetWarehouses;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestsBase;

namespace DonateTests.Services.GetWarehousesActionTests
{
    [TestClass()]
    public class GetWarehousesActionTests
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new DonateModule(),
        };

        [TestMethod()]
        public async Task GetWarehousesActionTest_GetWarehouses_ReturnsWareHouses()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<GetWarehousesAction>();

            // Act
            var executed = await action.Execute();
            var result = executed.Value as List<GetWarehousesResponse>;

            // Assert
            Assert.IsTrue(result!.Any());
        }

        [TestMethod()]
        public async Task GetWarehousesActionTest_RemoveAllWarehousesFromDb_ThrowException()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<GetWarehousesAction>();

            // Act
            var activeWarehouses = await context.Warehouses
                .Where(c => c.Status == Core.Database.Enums.DbEntityStatus.Active)
                .ToListAsync();

            foreach (var warehouse in activeWarehouses)
            {
                warehouse.Status = Core.Database.Enums.DbEntityStatus.Inactive;
            }

            await context.SaveChangesAsync();
            var exception = await Assert.ThrowsExceptionAsync<ItemNotFoundException>(() => action.Execute());

            // Assert
            Assert.IsInstanceOfType(exception, typeof(ItemNotFoundException));
            Assert.IsNotNull(exception.Message);
        }
    }
}