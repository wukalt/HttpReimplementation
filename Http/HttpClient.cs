using System.Net.Sockets;
using System.Text;

namespace HttpReimplementation.Http;

public class HttpClient
{
    public async Task<string> SendAsync(HttpRequest request)
    {
        if (!request.Headers.TryGetValue("Host", out string? host))
        {
            throw new InvalidOperationException("Request must include a Host header.");
        }
        const int port = 80;

        using TcpClient client = new TcpClient();
        await client.ConnectAsync(host, port);

        using NetworkStream stream = client.GetStream();

        string rawRequest = request.Build();
        byte[] requestBytes = Encoding.ASCII.GetBytes(rawRequest);
        
        await stream.WriteAsync(requestBytes, 0, requestBytes.Length);

        byte[] buffer = new byte[4096];
        int bytesRead;
        StringBuilder responseBuilder = new StringBuilder();

        do
        {
            bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            responseBuilder.Append(Encoding.ASCII.GetString(buffer, 0, bytesRead));
        }
        while (bytesRead > 0);

        return responseBuilder.ToString();
    }
}
