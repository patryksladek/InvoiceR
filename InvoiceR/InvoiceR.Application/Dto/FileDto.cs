namespace InvoiceR.Application.Dto;

public class FileDto
{
    public string Name { get; set; }
    public byte[] Content { get; set; }

    public FileDto(string name, byte[] content)
    {
        Name = name;
        Content = content;
    }
}
