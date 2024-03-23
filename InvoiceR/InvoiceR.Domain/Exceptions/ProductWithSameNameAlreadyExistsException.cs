namespace InvoiceR.Domain.Exceptions;

public class ProductWithSameNameAlreadyExistsException : Exception
{
    public string Name { get; set; }
    public ProductWithSameNameAlreadyExistsException(string name) 
        : base($"Product with name {name} already exists.")
        => Name = name;
}
