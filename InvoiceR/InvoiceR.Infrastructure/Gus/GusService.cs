using GusServiceReference;
using InvoiceR.Domain.Abstractions;
using System.ServiceModel.Channels;
using System.ServiceModel;
using WcfCoreMtomEncoder;
using Microsoft.Extensions.Configuration;

namespace InvoiceR.Infrastructure.Gus;

public class GusService : IGusService
{
    private readonly UslugaBIRzewnPublClient _gusServices;
    private readonly IConfiguration _configuration;

    public GusService(IConfiguration configuration)
    {
        _gusServices = new UslugaBIRzewnPublClient();
        _configuration = configuration;
    }

    public async Task<string> GetSearchResultByNipAsync(string nip)
    {
        ParametryWyszukiwania searchParameters = new ParametryWyszukiwania();
        searchParameters.Nip = nip;

        string requestResult = string.Empty;

        var encoding = new MtomMessageEncoderBindingElement(new TextMessageEncodingBindingElement());
        var transport = new HttpsTransportBindingElement();
        var customBinding = new CustomBinding(encoding, transport);

        string gusEndpointAddress = _configuration.GetValue<string>("GusSettings:EndpointAddress");
        EndpointAddress endPoint = new EndpointAddress(gusEndpointAddress);

        UslugaBIRzewnPublClient client = new UslugaBIRzewnPublClient(customBinding, endPoint);
        await client.OpenAsync();
        string userKey = _configuration.GetValue<string>("GusSettings:UserKey");
        var session = await client.ZalogujAsync(userKey);

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
