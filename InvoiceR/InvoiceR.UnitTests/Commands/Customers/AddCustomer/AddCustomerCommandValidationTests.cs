using FluentValidation.TestHelper;
using InvoiceR.Application.Commands.Customers.AddCustomer;
using InvoiceR.Domain.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceR.UnitTests.Commands.Customers.AddCustomer;

public class AddCustomerCommandValidationTests
{
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;

    public AddCustomerCommandValidationTests()
    {
        _customerRepositoryMock = new();
    }

    [Fact]
    public void ValidationResult_Should_Not_HaveAnyValidationErrors_WhenAddCustomerCommandIsValidated()
    {
        // Arrange
        var command = new AddCustomerCommand()
        {
            Name = "Customer 1",
            NIP = "1234567890",
            Segment = 1,
            Street = "Street 1",
            StreetNumber = "123",
            Building = "A",
            City = "City 1",
            PostalCode = "12-345",
            CountryId = 1,
            Phone = "123456789",
            Email = "customer1@example.com",
            Site = "customer1.com",
            IsActive = true
        };

        _customerRepositoryMock.Setup(
            x => x.IsAlreadyExistWithSameNameAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var validator = new AddCustomerCommandValidator();

        // Act
        var validationResult = validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task ValidationResult_Should_HaveValidationErrorForName_WhenNameIsEmpty()
    {
        // Arrange
        var command = new AddCustomerCommand()
        {
            Name = string.Empty,
            NIP = "1234567890",
            Segment = 1,
            Street = "Street 1",
            StreetNumber = "123",
            Building = "A",
            City = "City 1",
            PostalCode = "12-345",
            CountryId = 1,
            Phone = "123456789",
            Email = "customer1@example.com",
            Site = "customer1.com",
            IsActive = true
        };

        _customerRepositoryMock.Setup(
           x => x.IsAlreadyExistWithSameNameAsync(
               It.IsAny<string>(),
               It.IsAny<CancellationToken>()))
           .ReturnsAsync(false);

        var validator = new AddCustomerCommandValidator();

        // Act
        var validationResult = await validator.TestValidateAsync(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public async Task ValidationResult_Should_HaveValidationErrorForName_WhenNameHasMoreThan100Characters()
    {
        // Arrange
        var command = new AddCustomerCommand()
        {
            Name = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam in ligula mollis, convallis quam at, tempus risus. Integer ut nunc sem. Cras iaculis vel enim in varius. Donec iaculis at enim vel ultrices. Nam nec leo tempus, feugiat sem et, lacinia sapien. Maecenas dignissim erat vel arcu posuere ornare id non ligula. Maecenas sed tortor ultrices, efficitur ligula eget, viverra ante. Integer pharetra, velit accumsan posuere volutpat, odio ante elementum tellus, et fringilla urna ligula at ipsum. Suspendisse potenti. Donec sagittis ligula purus, non pellentesque leo ultrices non. Nam maximus efficitur nisl, ut rutrum dolor. Sed rhoncus aliquam tortor sed ultricies. Praesent ex dolor, tristique eu nulla eu, lacinia maximus eros.\r\n\r\nIn hac habitasse platea dictumst. Nulla pellentesque ac nulla eget euismod. Aenean tempus accumsan elit. Fusce egestas eleifend nisi, ut aliquet nisl dictum non. Sed vel egestas turpis. Suspendisse ac gravida mauris, eget accumsan justo. Vestibulum viverra metus sed elit suscipit tincidunt. In fermentum, velit in finibus congue, diam nisl convallis est, nec congue tortor diam vel ante. Ut sem felis, commodo ut ultrices et, finibus ut enim.\r\n\r\nQuisque aliquam libero odio, vitae molestie lorem sodales ut. Pellentesque facilisis auctor ligula eu consectetur. Nulla et ipsum mollis, convallis augue in, lacinia lacus. Sed sollicitudin felis nunc, non malesuada tortor lobortis eget. Proin sapien diam, scelerisque ut odio eu, porttitor iaculis augue. In accumsan eget elit ac dignissim. In ultrices vulputate sapien ac placerat. Aenean dictum risus consectetur est varius viverra viverra quis ligula. Pellentesque id sollicitudin orci. Nunc commodo.",
            NIP = "1234567890",
            Segment = 1,
            Street = "Street 1",
            StreetNumber = "123",
            Building = "A",
            City = "City 1",
            PostalCode = "12-345",
            CountryId = 1,
            Phone = "123456789",
            Email = "customer1@example.com",
            Site = "customer1.com",
            IsActive = true
        };

        _customerRepositoryMock.Setup(
           x => x.IsAlreadyExistWithSameNameAsync(
               It.IsAny<string>(),
               It.IsAny<CancellationToken>()))
           .ReturnsAsync(false);

        var validator = new AddCustomerCommandValidator();

        // Act
        var validationResult = await validator.TestValidateAsync(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public async Task ValidationResult_Should_HaveValidationErrorForEmail_WhenEmailIsNotValid()
    {
        // Arrange
        var command = new AddCustomerCommand()
        {
            Name = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam in ligula mollis, convallis quam at, tempus risus. Integer ut nunc sem. Cras iaculis vel enim in varius. Donec iaculis at enim vel ultrices. Nam nec leo tempus, feugiat sem et, lacinia sapien. Maecenas dignissim erat vel arcu posuere ornare id non ligula. Maecenas sed tortor ultrices, efficitur ligula eget, viverra ante. Integer pharetra, velit accumsan posuere volutpat, odio ante elementum tellus, et fringilla urna ligula at ipsum. Suspendisse potenti. Donec sagittis ligula purus, non pellentesque leo ultrices non. Nam maximus efficitur nisl, ut rutrum dolor. Sed rhoncus aliquam tortor sed ultricies. Praesent ex dolor, tristique eu nulla eu, lacinia maximus eros.\r\n\r\nIn hac habitasse platea dictumst. Nulla pellentesque ac nulla eget euismod. Aenean tempus accumsan elit. Fusce egestas eleifend nisi, ut aliquet nisl dictum non. Sed vel egestas turpis. Suspendisse ac gravida mauris, eget accumsan justo. Vestibulum viverra metus sed elit suscipit tincidunt. In fermentum, velit in finibus congue, diam nisl convallis est, nec congue tortor diam vel ante. Ut sem felis, commodo ut ultrices et, finibus ut enim.\r\n\r\nQuisque aliquam libero odio, vitae molestie lorem sodales ut. Pellentesque facilisis auctor ligula eu consectetur. Nulla et ipsum mollis, convallis augue in, lacinia lacus. Sed sollicitudin felis nunc, non malesuada tortor lobortis eget. Proin sapien diam, scelerisque ut odio eu, porttitor iaculis augue. In accumsan eget elit ac dignissim. In ultrices vulputate sapien ac placerat. Aenean dictum risus consectetur est varius viverra viverra quis ligula. Pellentesque id sollicitudin orci. Nunc commodo.",
            NIP = "1234567890",
            Segment = 1,
            Street = "Street 1",
            StreetNumber = "123",
            Building = "A",
            City = "City 1",
            PostalCode = "12-345",
            CountryId = 1,
            Phone = "123456789",
            Email = "customer1.example.com",
            Site = "customer1.com",
            IsActive = true
        };

        _customerRepositoryMock.Setup(
           x => x.IsAlreadyExistWithSameNameAsync(
               It.IsAny<string>(),
               It.IsAny<CancellationToken>()))
           .ReturnsAsync(false);

        var validator = new AddCustomerCommandValidator();

        // Act
        var validationResult = await validator.TestValidateAsync(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x => x.Name);
    }
}