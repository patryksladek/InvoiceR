using GusServiceReference;
using InvoiceR.Domain.Abstractions;
using InvoiceR.Domain.Entities.Customers;
using System.ServiceModel.Channels;
using System.ServiceModel;
using WcfCoreMtomEncoder;

namespace InvoiceR.Infrastructure.Gus;

public class GusService : IGusService
{
    private readonly UslugaBIRzewnPublClient _gusServices;

    public GusService()
    {
        _gusServices = new UslugaBIRzewnPublClient();
    }

    public async Task<string> GetSearchResultByNipAsync(string nip)
    {
        ParametryWyszukiwania searchParameters = new ParametryWyszukiwania();
        searchParameters.Nip = nip;

        string requestResult = string.Empty;

        var encoding = new MtomMessageEncoderBindingElement(new TextMessageEncodingBindingElement());
        var transport = new HttpsTransportBindingElement();
        var customBinding = new CustomBinding(encoding, transport);

        EndpointAddress endPoint = new EndpointAddress("https://wyszukiwarkaregon.stat.gov.pl/wsBIR/UslugaBIRzewnPubl.svc");

        UslugaBIRzewnPublClient client = new UslugaBIRzewnPublClient(customBinding, endPoint);
        await client.OpenAsync();
        var session = await client.ZalogujAsync("");

        using (new OperationContextScope(client.InnerChannel))
        {
            HttpRequestMessageProperty requestMessage = new HttpRequestMessageProperty();
            requestMessage.Headers["sid"] = session.ZalogujResult;
            OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = requestMessage;
            var result = client.DaneSzukajPodmiotyAsync(searchParameters).GetAwaiter().GetResult();
            requestResult = result.DaneSzukajPodmiotyResult;
        }
        await client.WylogujAsync(session.ZalogujResult);
        await client.CloseAsync();

        return requestResult;
    }
}
