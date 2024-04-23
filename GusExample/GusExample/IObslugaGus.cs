using GusExample.Models;

namespace GusExample;

internal interface IObslugaGus
{
    string ApiKey { get; set; }

    Task<PodmiotGus> PobierzDanePodmiotu(string nip);
    Task<string> GetSearchResultAsync(string nip);
    Task Logout();
}
