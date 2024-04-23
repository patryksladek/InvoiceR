using GusExample.Models;
using GusService;
using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Xml.Serialization;
using WcfCoreMtomEncoder;


namespace GusExample;

public class ObslugaGus : IObslugaGus
{
    public string ApiKey { get; set; }

    private readonly UslugaBIRzewnPublClient _gusServices;
    private string _sessionId;

    //public ObslugaGus()
    //{
    //    var encoding = new MtomMessageEncoderBindingElement(new TextMessageEncodingBindingElement());
    //    var transport = new HttpsTransportBindingElement();
    //    var customBinding = new CustomBinding(encoding, transport);

    //    EndpointAddress endPoint = new EndpointAddress("https://wyszukiwarkaregon.stat.gov.pl/wsBIR/UslugaBIRzewnPubl.svc");
        
    //    _gusServices = new UslugaBIRzewnPublClient(customBinding, endPoint);
    //}

    public async Task<PodmiotGus> PobierzDanePodmiotu(string nip)
    {
        await LoginIfRequired();

        ParametryWyszukiwania nipData = new ParametryWyszukiwania();
        nipData.Nip = nip;

        try
        {
            var daneSzukajResponse = await _gusServices.DaneSzukajPodmiotyAsync(nipData);

            using (var reader = new StringReader(daneSzukajResponse.DaneSzukajPodmiotyResult))
            {
                XmlRootAttribute xRoot = new XmlRootAttribute();
                xRoot.ElementName = "root";

                XmlSerializer daneSzukajSerializer = new XmlSerializer(typeof(DaneGus), xRoot);
                var daneGus = (DaneGus)daneSzukajSerializer.Deserialize(reader);

                return daneGus.dane;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return null;
    }

    // <summary>
    /// Zwraca resultat wyszukiwania w serwisie na podstawie zdefiniowanych parametrów wyszukiwania.
    /// </summary>e
    /// <param name="searchParameters"></param>
    /// <returns></returns>
    public async Task<string> GetSearchResultAsync(string nip)
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
        var session = await client.ZalogujAsync("c1605499b23c4ded8e5e");

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

    private async Task LoginIfRequired()
    {
        await _gusServices.OpenAsync();

        if ((await _gusServices.GetValueAsync("StatusSesji")).GetValueResult == "0") 
            await Login();
    }

    private async Task Login()
    {
        var _sessionId = await _gusServices.ZalogujAsync(ApiKey);

        var x = await _gusServices.GetValueAsync("StatusSesji");

        OperationContextScope scope = new OperationContextScope(_gusServices.InnerChannel);

        HttpRequestMessageProperty requestProperties = new HttpRequestMessageProperty();
        requestProperties.Headers.Add("sid", _sessionId.ZalogujResult);
        OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = requestProperties;

    }

    public async Task Logout()
    {
        await _gusServices.WylogujAsync(_sessionId);
    }
}