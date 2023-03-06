using Autofac;
using Core;
using Core.Database;
using Core.Exceptions;
using Core.Setup.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Order;
using Order.Actions.WarehousesActions.GetWarehouses;
using Order.Actions.WarehousesActions.GetWarehousesList;
using TestsBase;

namespace OrderTests.Actions.WarehousesActionTest
{
    [TestClass()]
    public class WarehousesGetListActionTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new OrderModule(),
        };

        [TestMethod()]
        public async Task WarehousesGetListActionTest_GetWarehouseList_ReturnsResponse()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<WarehousesGetListAction>();

            // Act
            var result = await action.Execute();

            // Assert
            Assert.IsTrue(result!.Any());
        }

        [TestMethod()]
        public async Task WarehousesGetListActionTest_RemoveAllWarehousesFromDb_ThrowException()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<WarehousesGetListAction>();

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
