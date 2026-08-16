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
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;

namespace inzynierka.Tests.Controllers;

public class RegisterControllerTests
{

    [Fact]
    public void Index_ShouldReturnView()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<InzynierkaContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new InzynierkaContext(options);

        var controller = new RegisterController(null, context);

        // Act
        var result = controller.Index();

        // Assert
        Assert.IsType<ViewResult>(result);
    }
    [Fact]
    public void SubmitRegister_EmailAlreadyExists_ShouldReturnView()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<InzynierkaContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new InzynierkaContext(options);

        var existingUser = new UserModel
        {
            Username = "istniejacy",
            Email = "test@test.pl",
            Password = "Haslo123!",
            Name = "Jan",
            Surname = "Kowalski"
        };

        context.Users.Add(existingUser);
        context.SaveChanges();

        var controller = new RegisterController(null, context);

        var user = new UserModel
        {
            Username = "nowy",
            Email = "test@test.pl",
            Password = "Haslo456!",
            Name = "Adam",
            Surname = "Nowak"
        };

        var tempData = new TempDataDictionary(
                new DefaultHttpContext(),
                Mock.Of<ITempDataProvider>()
        );

        controller.TempData = tempData;

        // Act
        var result = controller.SubmitRegister(user);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);

        Assert.Equal("Index", viewResult.ViewName);
        Assert.True(controller.ModelState.ContainsKey("Email"));
    }

    [Fact]
    public void SubmitRegister_UsernameAlreadyExists_ShouldReturnView()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<InzynierkaContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new InzynierkaContext(options);

        var existingUser = new UserModel
        {
            Username = "test",
            Email = "test1@test.pl",
            Password = "Haslo123!",
            Name = "Jan",
            Surname = "Kowalski"
        };

        context.Users.Add(existingUser);
        context.SaveChanges();

        var controller = new RegisterController(null, context);

        var user = new UserModel
        {
            Username = "test",
            Email = "test@test.pl",
            Password = "Haslo456!",
            Name = "Adam",
            Surname = "Nowak"
        };

        var tempData = new TempDataDictionary(
                new DefaultHttpContext(),
                Mock.Of<ITempDataProvider>()
        );

        controller.TempData = tempData;

        // Act
        var result = controller.SubmitRegister(user);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);

        Assert.Equal("Index", viewResult.ViewName);
        Assert.True(controller.ModelState.ContainsKey("Username"));
    }

    [Fact]
    public void SubmitRegister_InvalidModel_ShouldReturnView()
    {
        var options = new DbContextOptionsBuilder<InzynierkaContext>()
           .UseInMemoryDatabase(Guid.NewGuid().ToString())
           .Options;

        using var context = new InzynierkaContext(options);

        var controller = new RegisterController(null, context);

        var user = new UserModel
        {
            Username = "test",
            Email = "test1@test.pl",
            Password = "Haslo123!",
            Name = "Jan",
            Surname = "Kowalski"
        };

        var tempData = new TempDataDictionary(
                new DefaultHttpContext(),
                Mock.Of<ITempDataProvider>()
        );

        controller.TempData = tempData;

        controller.ModelState.AddModelError(
            "Email",
            "Niepoprwany email"
        );

        var result = controller.SubmitRegister(user);

        //asert
        var viewResult = Assert.IsType<ViewResult>(result);

        Assert.Equal("Index", viewResult.ViewName);
    }

    [Fact]
    public void SubmitRegister_ValidUser_ShouldRedirect()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<InzynierkaContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new InzynierkaContext(options);

        var emailServiceMock = new Mock<EmailService>(
            Mock.Of<IConfiguration>()
        );

        emailServiceMock
            .Setup(x => x.SendEmailAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        var controller = new RegisterController(
            emailServiceMock.Object,
            context
        );

        controller.Url = Mock.Of<IUrlHelper>();

        var httpContext = new DefaultHttpContext();

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };

        var user = new UserModel
        {
            Username = "nowyuser",
            Email = "nowy@test.pl",
            Password = "Haslo123!",
            Name = "Jan",
            Surname = "Kowalski"
        };

        var tempData = new TempDataDictionary(
            new DefaultHttpContext(),
            Mock.Of<ITempDataProvider>()
        );

        controller.TempData = tempData;

        // Act
        var result = controller.SubmitRegister(user);

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);

        Assert.Equal("GoToLogin", redirectResult.ActionName);
        Assert.Equal("Home", redirectResult.ControllerName);
    }

    [Fact]
    public void VerifyEmail_NullToken_ShouldReturnBadRequest()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<InzynierkaContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new InzynierkaContext(options);

        var controller = new RegisterController(null, context);

        // Act
        var result = controller.VerifyEmail(null);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Niepoprawny token", badRequest.Value);
    }
    
    [Fact]
    public void VerifyEmail_InvalidToken_ShouldReturnBadRequest()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<InzynierkaContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new InzynierkaContext(options);

        var controller = new RegisterController(null, context);

        // Act
        var result = controller.VerifyEmail("nieistniejacy-token");

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Nieprawidłowy token", badRequest.Value);
    }

    [Fact]
    public void VerifyEmail_ExpiredToken_ShouldReturnBadRequest()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<InzynierkaContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new InzynierkaContext(options);

        var user = new UserModel
        {
            Username = "testuser",
            Email = "test@test.pl",
            Password = "Haslo123!",
            Name = "Jan",
            Surname = "Kowalski",
            EmailConfirmed = false,
            VerificationToken = "expired-token",
            VerificationTokenExpires = DateTime.UtcNow.AddHours(-1)
        };

        context.Users.Add(user);
        context.SaveChanges();

        var controller = new RegisterController(null, context);

        // Act
        var result = controller.VerifyEmail("expired-token");

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Token wygasł", badRequest.Value);
    }

    [Fact]
    public void VerifyEmail_ValidToken_ShouldConfirmEmailAndRedirect()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<InzynierkaContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new InzynierkaContext(options);

        var user = new UserModel
        {
            Username = "testuser",
            Email = "test@test.pl",
            Password = "Haslo123!",
            Name = "Jan",
            Surname = "Kowalski",
            EmailConfirmed = false,
            VerificationToken = "valid-token",
            VerificationTokenExpires = DateTime.UtcNow.AddHours(1)
        };

        context.Users.Add(user);
        context.SaveChanges();

        var controller = new RegisterController(null, context);

        var tempData = new TempDataDictionary(
            new DefaultHttpContext(),
            Mock.Of<ITempDataProvider>()
        );

        controller.TempData = tempData;

        // Act
        var result = controller.VerifyEmail("valid-token");

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);

        Assert.Equal("GoToLogin", redirectResult.ActionName);
        Assert.Equal("Home", redirectResult.ControllerName);

        var updatedUser = context.Users.First();

        Assert.True(updatedUser.EmailConfirmed);
        Assert.Null(updatedUser.VerificationToken);
        Assert.Null(updatedUser.VerificationTokenExpires);
    }
}