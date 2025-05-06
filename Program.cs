using HttpReimplementation.Http;
using HttpClient = HttpReimplementation.Http.HttpClient;

var request = new HttpRequest(HttpReimplementation.Http.HttpMethod.GET,
    "/",
    HttpVersion.Http11);

request.AddHeader("Host", "example.com");

var client = new HttpClient();
string response = await client.SendAsync(request);
Console.WriteLine(response);