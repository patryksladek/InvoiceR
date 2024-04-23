using GusExample;

ObslugaGus obslugaGus = new ObslugaGus();
obslugaGus.ApiKey = "c1605499b23c4ded8e5e";
//obslugaGus.ApiKey = "abcde12345abcde12345";
var danePodmiotu = await obslugaGus.GetSearchResultAsync("8322071464");
Console.WriteLine("");
