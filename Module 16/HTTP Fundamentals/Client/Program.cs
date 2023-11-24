class Program
{
    static async Task Main()
    {
        using var client = new HttpClient();

        var urls = new List<string>()
        {
            "http://localhost:8888/MyName/",
            "http://localhost:8888/Information/",
            "http://localhost:8888/Success/",
            "http://localhost:8888/Redirection/",
            "http://localhost:8888/ClientError/",
            "http://localhost:8888/ServerError/",
            "http://localhost:8888/MyNameByHeader/",
            "http://localhost:8888/MyNameByCookies/"
        };

        foreach (var url in urls)
        {
            // Task 1 - 2
            var response = await client.GetAsync(url);
            var payload = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"""
                               Received response from URL: {url} 
                                   Status code: {(int)response.StatusCode}
                                   Code meaning: {response.StatusCode}
                                   Payload: {payload}
                                   Headers:
                               """);

            // Task 3
            var headers = response.Headers;
            foreach (var header in headers)
            {
                Console.WriteLine($"\t{header.Key}: {string.Join(" ", header.Value)}");
            }

            // Task 4
            if (!headers.TryGetValues("Set-Cookie", out var cookies)) continue;
            var cookieString = cookies.FirstOrDefault(c => c.StartsWith("MyName"));
            var cookieValue = cookieString?.Split('=')[1];
            Console.WriteLine("\t\tCookie value:" + cookieValue);
        }

        Console.ReadKey();
    }
}