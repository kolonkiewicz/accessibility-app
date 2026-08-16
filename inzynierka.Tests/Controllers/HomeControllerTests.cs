using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using inzynierka;

namespace inzynierka.Tests.Controllers;

public class HomeControllerTests
{
    private HomeController CreateController()
    {
        var logger = LoggerFactory
            .Create(builder => { })
            .CreateLogger<HomeController>();

        return new HomeController(logger);
    }

    [Fact]
    public void Index_ShouldReturnView()
    {
        // Arrange
        var controller = CreateController();

        // Act
        var result = controller.Index();

        // Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void GoToLogin_ShouldRedirectToLogin()
    {
        // Arrange
        var controller = CreateController();

        // Act
        var result = controller.GoToLogin();

        // Assert
        var redirect = Assert.IsType<RedirectToActionResult>(result);

        Assert.Equal("Index", redirect.ActionName);
        Assert.Equal("Login", redirect.ControllerName);
    }

    [Fact]
    public void GoToRegister_ShouldRedirectToRegister()
    {
        // Arrange
        var controller = CreateController();

        // Act
        var result = controller.GoToRegister();

        // Assert
        var redirect = Assert.IsType<RedirectToActionResult>(result);

        Assert.Equal("Index", redirect.ActionName);
        Assert.Equal("Register", redirect.ControllerName);
    }

    [Fact]
    public void GoToResetPassword_ShouldRedirectToResetPassword()
    {
        // Arrange
        var controller = CreateController();

        // Act
        var result = controller.GoToResetPassword();

        // Assert
        var redirect = Assert.IsType<RedirectToActionResult>(result);

        Assert.Equal("Index", redirect.ActionName);
        Assert.Equal("ResetPassword", redirect.ControllerName);
    }

    [Fact]
    public void GoToAccount_ShouldRedirectToAccount()
    {
        // Arrange
        var controller = CreateController();

        // Act
        var result = controller.GoToAccount();

        // Assert
        var redirect = Assert.IsType<RedirectToActionResult>(result);

        Assert.Equal("Index", redirect.ActionName);
        Assert.Equal("Account", redirect.ControllerName);
    }

    [Fact]
    public void GoToChangePassword_ShouldRedirectToChangePassword()
    {
        // Arrange
        var controller = CreateController();

        // Act
        var result = controller.GoToChangePassword();

        // Assert
        var redirect = Assert.IsType<RedirectToActionResult>(result);

        Assert.Equal("EditPassword", redirect.ActionName);
        Assert.Equal("Account", redirect.ControllerName);
    }

    [Fact]
    public void GoToReports_ShouldRedirectToReports()
    {
        // Arrange
        var controller = CreateController();

        // Act
        var result = controller.GoToReports();

        // Assert
        var redirect = Assert.IsType<RedirectToActionResult>(result);

        Assert.Equal("Index", redirect.ActionName);
        Assert.Equal("Reports", redirect.ControllerName);
    }

    [Fact]
    public void GoToDashboard_ShouldRedirectToDashboard()
    {
        // Arrange
        var controller = CreateController();

        // Act
        var result = controller.GoToDashboard();

        // Assert
        var redirect = Assert.IsType<RedirectToActionResult>(result);

        Assert.Equal("Dashboard", redirect.ActionName);
        Assert.Equal("Dashboard", redirect.ControllerName);
    }
}