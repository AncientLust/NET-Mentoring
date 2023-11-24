using System.Net;

namespace Listener;

class Program
{
    private static HttpListener listener;

    static void Main()
    {
        listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:8888/");
        listener.Start();

        Console.WriteLine("Listening at http://localhost:8888/");

        while (true)
        {
            var context = listener.GetContext();
            var response = context.Response;

            var resourcePath = context.Request.RawUrl.Trim('/');
            switch (resourcePath)
            {
                case "MyName":
                    SendPayload(response, "Alex");
                    break;
                case "Information":
                    response.StatusCode = 103; // Didn't have success with these codes: 100, 101, 102, 103.
                    response.StatusCode = 200;
                    SendPayload(response, "Information payload");
                    break;
                case "Success":
                    response.StatusCode = 200;
                    SendPayload(response, "Success payload");
                    break;
                case "Redirection":
                    response.StatusCode = 302;
                    SendPayload(response, "Redirection payload");
                    break;
                case "ClientError":
                    response.StatusCode = 400;
                    SendPayload(response, "Client error payload");
                    break;
                case "ServerError":
                    response.StatusCode = 500;
                    SendPayload(response, "Server error payload");
                    break;
                case "MyNameByHeader":
                    response.Headers.Add("X-MyName", "Alex");
                    SendPayload(response, "The name sent in the header");
                    break;
                case "MyNameByCookies":
                    response.Cookies.Add(new Cookie("MyName", "Alex"));
                    SendPayload(response, "The name sent in the cookie");
                    break;
                default:
                    response.StatusCode = 404;
                    break;
            }

            response.Close();
        }
    }

    static void SendPayload(HttpListenerResponse response, string payload)
    {
        var buffer = System.Text.Encoding.UTF8.GetBytes(payload);
        response.ContentLength64 = buffer.Length;

        var output = response.OutputStream;
        output.Write(buffer, 0, buffer.Length);
        output.Close();
    }
}