using System.ComponentModel.DataAnnotations;
using inzynierka.Models;

namespace inzynierka.Tests.Models;

public class UserModelTests
{
    [Fact]
    public void Name_ShouldBeRequired()
    {
        // Arrange
        var user = new UserModel
        {
            Name = "",
            Surname = "Kowalski",
            Username = "testuser",
            Password = "Test1234",
            Email = "test@example.com",
            Ppassword = "Test1234"
        };

        var context = new ValidationContext(user);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(
            user,
            context,
            results,
            validateAllProperties: true
        );

        // Assert
        Assert.False(isValid);
        Assert.Contains(results, r => r.MemberNames.Contains(nameof(UserModel.Name)));
    }
}