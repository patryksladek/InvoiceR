using FluentValidation.TestHelper;
using InvoiceR.Application.Commands.Customers.AddCustomer;
using InvoiceR.Application.Commands.Products.AddProduct;
using InvoiceR.Domain.Abstractions;
using InvoiceR.Domain.Enums;
using Moq;

namespace InvoiceR.UnitTests.Commands.Customers.AddCustomer;

public class AddProductCommandValidatorTests
{
    private readonly Mock<ICustomerRepository> _productRepositoryMock;

    public AddProductCommandValidatorTests()
    {
        _productRepositoryMock = new();
    }

    [Fact]
    public void ValidationResult_Should_Not_HaveAnyValidationErrors_WhenAddProductCommandIsValidated()
    {
        // Arrange
        var command = new AddProductCommand()
        {
            Name = "Product 1",
            ProductType = (int)ProductType.Product,
            Barcode = "978020137962",
            ProductBarcodeType = (int)ProductBarcodeType.EAN13,
            CurrencyId = 1,
            UnitId = 1,
            VatRateId = 6,
            Price = 100
        };

        _productRepositoryMock.Setup(
            x => x.IsAlreadyExistWithSameNameAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var validator = new AddProductCommandValidator();

        // Act
        var validationResult = validator.TestValidate(command);

        // Assert
        validationResult.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task ValidationResult_Should_HaveValidationErrorForName_WhenNameIsEmpty()
    {
        // Arrange
        var command = new AddProductCommand()
        {
            Name = string.Empty,
            ProductType = (int)ProductType.Product,
            Barcode = "978020137962",
            ProductBarcodeType = (int)ProductBarcodeType.EAN13,
            CurrencyId = 1,
            UnitId = 1,
            VatRateId = 6,
            Price = 100
        };

        _productRepositoryMock.Setup(
           x => x.IsAlreadyExistWithSameNameAsync(
               It.IsAny<string>(),
               It.IsAny<CancellationToken>()))
           .ReturnsAsync(false);

        var validator = new AddProductCommandValidator();

        // Act
        var validationResult = await validator.TestValidateAsync(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public async Task ValidationResult_Should_HaveValidationErrorForName_WhenNameHasMoreThan240Characters()
    {
        // Arrange
        var command = new AddProductCommand()
        {
            Name = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam in ligula mollis, convallis quam at, tempus risus. Integer ut nunc sem. Cras iaculis vel enim in varius. Donec iaculis at enim vel ultrices. Nam nec leo tempus, feugiat sem et, lacinia sapien. Maecenas dignissim erat vel arcu posuere ornare id non ligula. Maecenas sed tortor ultrices, efficitur ligula eget, viverra ante. Integer pharetra, velit accumsan posuere volutpat, odio ante elementum tellus, et fringilla urna ligula at ipsum. Suspendisse potenti. Donec sagittis ligula purus, non pellentesque leo ultrices non. Nam maximus efficitur nisl, ut rutrum dolor. Sed rhoncus aliquam tortor sed ultricies. Praesent ex dolor, tristique eu nulla eu, lacinia maximus eros.\r\n\r\nIn hac habitasse platea dictumst. Nulla pellentesque ac nulla eget euismod. Aenean tempus accumsan elit. Fusce egestas eleifend nisi, ut aliquet nisl dictum non. Sed vel egestas turpis. Suspendisse ac gravida mauris, eget accumsan justo. Vestibulum viverra metus sed elit suscipit tincidunt. In fermentum, velit in finibus congue, diam nisl convallis est, nec congue tortor diam vel ante. Ut sem felis, commodo ut ultrices et, finibus ut enim.\r\n\r\nQuisque aliquam libero odio, vitae molestie lorem sodales ut. Pellentesque facilisis auctor ligula eu consectetur. Nulla et ipsum mollis, convallis augue in, lacinia lacus. Sed sollicitudin felis nunc, non malesuada tortor lobortis eget. Proin sapien diam, scelerisque ut odio eu, porttitor iaculis augue. In accumsan eget elit ac dignissim. In ultrices vulputate sapien ac placerat. Aenean dictum risus consectetur est varius viverra viverra quis ligula. Pellentesque id sollicitudin orci. Nunc commodo.",
            ProductType = (int)ProductType.Product,
            Barcode = "978020137962",
            ProductBarcodeType = (int)ProductBarcodeType.EAN13,
            CurrencyId = 1,
            UnitId = 1,
            VatRateId = 6,
            Price = 100
        };

        _productRepositoryMock.Setup(
           x => x.IsAlreadyExistWithSameNameAsync(
               It.IsAny<string>(),
               It.IsAny<CancellationToken>()))
           .ReturnsAsync(false);

        var validator = new AddProductCommandValidator();

        // Act
        var validationResult = await validator.TestValidateAsync(command);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(x => x.Name);
    }
}