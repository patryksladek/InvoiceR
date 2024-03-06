namespace InvoiceR.Domain.Exceptions;

public class CustomerWithSameNameAlreadyExistsException : Exception
{
    public string Name { get; set; }
    public CustomerWithSameNameAlreadyExistsException(string name) 
        : base($"Customer with name {name} already exists.")
        => Name = name;
}
