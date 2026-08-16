using inzynierka.Controllers;
using inzynierka.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Identity;

namespace inzynierka.Tests.Controllers;

public class LoginControllerTests
{
    [Fact]
    public void Index_ShouldReturnView()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<InzynierkaContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new InzynierkaContext(options);
        var controller = new LoginController(context);

        // Act
        var result = controller.Index();

        // Assert
        Assert.IsType<ViewResult>(result);
    }


    [Fact]
    public void SubmitLogin_UserDoesNotExist_ShouldReturnView()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<InzynierkaContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new InzynierkaContext(options);

        var controller = new LoginController(context);

        var tempData = new TempDataDictionary(
            new DefaultHttpContext(),
            Mock.Of<ITempDataProvider>()
        );

        controller.TempData = tempData;

        var user = new UserModel
        {
            Username = "nieistniejacy",
            Password = "Test1234"
        };

        // Act
        var result = controller.SubmitLogin(user);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);

        Assert.Equal("Index", viewResult.ViewName);
        Assert.True(controller.ModelState.ContainsKey("Username"));
    }

    [Fact]
    public void SubmitLogin_EmailNotConfirmed_ShouldReturnView()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<InzynierkaContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new InzynierkaContext(options);

        var existingUser = new UserModel
        {
            UserId = 1,
            Username = "testuser",
            Password = "Test1234",
            Email = "test@test.pl",
            Name = "Jan",
            Surname = "Kowalski",
            EmailConfirmed = false
        };

        context.Users.Add(existingUser);
        context.SaveChanges();

        var controller = new LoginController(context);

        var user = new UserModel
        {
            Username = "testuser",
            Password = "Test1234"
        };
        var tempData = new TempDataDictionary(
                new DefaultHttpContext(),
                Mock.Of<ITempDataProvider>()
        );

        controller.TempData = tempData;


        // Act
        var result = controller.SubmitLogin(user);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);

        Assert.Equal("Index", viewResult.ViewName);
        Assert.True(controller.ModelState.ContainsKey("Username"));
    }

    [Fact]
    public void SubmitLogin_WrongPassword_ShouldReturnView()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<InzynierkaContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new InzynierkaContext(options);

        var existingUser = new UserModel
        {
            UserId = 1,
            Username = "testuser",
            Email = "test@test.pl",
            Name = "Jan",
            Surname = "Kowalski",
            EmailConfirmed = true
        };
        
        var passwordHasher = new PasswordHasher<UserModel>();

        existingUser.Password = passwordHasher.HashPassword(
            existingUser,
            "test1234"
        );
        
        context.Users.Add(existingUser);
        context.SaveChanges();

        var controller = new LoginController(context);

        var user = new UserModel
        {
            Username = "testuser",
            Password = "zlehaslo"
        };
        var tempData = new TempDataDictionary(
                new DefaultHttpContext(),
                Mock.Of<ITempDataProvider>()
        );

        controller.TempData = tempData;


        // Act
        var result = controller.SubmitLogin(user);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);

        Assert.Equal("Index", viewResult.ViewName);
        Assert.True(controller.ModelState.ContainsKey("Password"));
    }
}