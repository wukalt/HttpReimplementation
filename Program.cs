using HttpReimplementation.Http;

HttpRequest req = new(HttpReimplementation.Http.HttpMethod.GET, "/", HttpVersion.Http11);
req.AddHeader("Host", "wuka.com");

string build = req.Build();
Console.WriteLine(build);