using AuthService;
using Autofac;
using Core;
using Core.Auth0;
using Core.Database;
using Core.Database.Enums;
using Core.Database.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestsCore;

namespace AuthServiceTests
{
    // Remove me in future!

    //[TestClass()]
    //public class ExampleTests
    //{
    //    private readonly List<Module> _usedModules = new()
    //    {
    //        new CoreModule(false),
    //        new AuthServiceModule(),
    //    };

    //    [TestMethod()]
    //    public async Task ExampleTest()
    //    {
    //        using var toolkit = new TestsToolkit(_usedModules);
    //        var context = toolkit.Resolve<AppDbContext>();
    //        var myService1 = toolkit.Resolve<MyService1>();

    //        // Arrange
    //        var newlyAddedEmail = "test@testowy.com";
    //        context.Add(new User()
    //        {
    //            Status = DbEntityStatus.Active,
    //            FirstName = "Test",
    //            LastName = "Testowy",
    //            Email = newlyAddedEmail
    //        });
    //        await context.SaveChangesAsync();

    //        var emailOfCurrentlyLoggedUser = "przykladowy@email.com";
    //        toolkit.UpdateUserInfo(new CurrentUserInfo
    //        {
    //            Email = emailOfCurrentlyLoggedUser
    //        });

    //        // Act
    //        var currentUserEmail = myService1.GetCurrentUserEmail();
    //        var user = await myService1.GetUser();

    //        // Assert
    //        Assert.AreEqual(emailOfCurrentlyLoggedUser, currentUserEmail);
    //        Assert.AreEqual(newlyAddedEmail, user.Email);
    //    }
    //}
}