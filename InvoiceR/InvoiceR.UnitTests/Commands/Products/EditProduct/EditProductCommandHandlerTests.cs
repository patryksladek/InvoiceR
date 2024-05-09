using InvoiceR.Application.Commands.Products.AddProduct;
using InvoiceR.Domain.Abstractions;
using InvoiceR.Domain.Abstractions.Repositories;
using InvoiceR.Domain.Entities.Products;
using InvoiceR.Domain.Enums;
using InvoiceR.Domain.Exceptions;
using Moq;

namespace InvoiceR.UnitTests.Commands.Customers.EditCusotmer;


public class EditProductCommandHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public EditProductCommandHandlerTests()
    {
        _productRepositoryMock = new();
        _unitOfWorkMock = new();
    }

    [Fact]
    public async Task Handle_Should_CallUpdateOnReposiotry_WhenNameIsUnique()
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
            x => x.Update(It.IsAny<Product>()));

        _productRepositoryMock.Setup(
            x => x.IsAlreadyExistWithSameNameAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var handler = new AddProductCommandHandler(
            _productRepositoryMock.Object,
            _unitOfWorkMock.Object);

        // Act 
        var productDto = await handler.Handle(command, default);

        // Assert
        _productRepositoryMock.Verify(
            x => x.Add(It.Is<Product>(x => x.Id == productDto.Id)),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ThrowProductAlreadyExistsException_WhenNameIsNotUnique()
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
             x => x.Update(It.IsAny<Product>()));

        _productRepositoryMock.Setup(
            x => x.IsAlreadyExistWithSameNameAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var handler = new AddProductCommandHandler(
           _productRepositoryMock.Object,
           _unitOfWorkMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ProductWithSameNameAlreadyExistsException>(async () => await handler.Handle(command, default));
    }
}
