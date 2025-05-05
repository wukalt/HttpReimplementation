using System.Text;

namespace HttpReimplementation.Http;

public class HttpRequest
{
    public HttpMethod Method { get; set; }
    public string Path { get; set; }
    public HttpVersion Version { get; set; }
    public Dictionary<string, string> Headers { get; set; } = new();
    public string? Body { get; set; }

    public HttpRequest(HttpMethod method, string path, HttpVersion version)
    {
        Method = method;
        Path = path;
        Version = version;

        Headers["User-Agent"] = "MyHttpClient/1.0";
        Headers["Connection"] = "close";
    }

    public void AddHeader(string name, string value)
    {
        Headers[name] = value;
    }

    public string Build()
    {
        StringBuilder stringBuilder = new();

        stringBuilder.Append($"{Method} {Path} {GetVersionString()}\r\n");
        foreach (var header in Headers)
        {
            stringBuilder.Append($"{header.Key}: {header.Value}\r\n");
        }
        stringBuilder.Append("\r\n");

        if (!string.IsNullOrEmpty(Body))
        {
            stringBuilder.Append(Body);
        }

        return stringBuilder.ToString();
    }

    private string GetVersionString() => Version switch
    {
        HttpVersion.Http10 => "HTTP/1.0",
        HttpVersion.Http11 => "HTTP/1.1",
        _ => throw new InvalidOperationException("Unsupported HTTP version")
    };
}