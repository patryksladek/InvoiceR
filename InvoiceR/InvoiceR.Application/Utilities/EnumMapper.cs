namespace InvoiceR.Application.Utilities;

public static class EnumMapper
{
    public static TTarget Map<TTarget>(Enum sourceValue) where TTarget : Enum
    {
        return (TTarget)Enum.ToObject(typeof(TTarget), sourceValue);
    }
}
