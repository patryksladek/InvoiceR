using System.Xml.Serialization;

namespace GusExample.Models;

[XmlType("dane")]
public class PodmiotGus
{
    public string Regon { get; set; }
    public string Nip { get; set; }
    public string StatusNip { get; set; }
    public string Nazwa { get; set; }
    public string Wojewodztwo { get; set; }
    public string Powiat { get; set; }
    public string Gmina { get; set; }
    public string Miejscowosc { get; set; }
    public string KodPocztowy { get; set; }
    public string Ulica { get; set; }
    public string NrNieruchomosci { get; set; }
    public string NrLokalu { get; set; }
    public string Typ { get; set; }
    public string SilosID { get; set; }
    public string DataZakonczeniaDzialalnosci { get; set; }
    public string MiejscowoscPoczty { get; set; }
}
