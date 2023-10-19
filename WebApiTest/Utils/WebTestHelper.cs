using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.IO.Compression;
using System.Text;

namespace WebApiTest.Utils
{
    public class WebTestHelper<TStartup> : IDisposable where TStartup : class
    {
        public WebTestHelper(string baseUrl, WebApplicationFactory<TStartup> webApplicationFactory,
            Action<IServiceCollection> registerServices, Action<WebHostBuilderContext, IConfigurationBuilder> configure)
        {
            BaseUrl = baseUrl;
            WebApplicationFactory = webApplicationFactory.WithWebHostBuilder(x =>
                x.ConfigureAppConfiguration(configure)
                    .UseContentRoot(".")
                    .ConfigureLogging(logging => logging.ClearProviders())
                    .ConfigureServices(registerServices));
            ResetUrl();
        }

        private WebApplicationFactory<TStartup> WebApplicationFactory { get; }

        public IServiceProvider ServiceProvider => WebApplicationFactory.Services;

        public UrlBuilder UrlBuilder { get; set; }

        private string BaseUrl { get; }

        public async Task<T> SendRequest<T>(HttpMethod method, object content, Action<HttpRequestHeaders> addHeaders = null) where T : class
        {
            var httpContent = content as HttpContent;
            if (httpContent == null && content != null)
            {
                httpContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8,
                    "application/json");
            }

            return await SendRequest<T>(method, httpContent, addHeaders);
        }

        public async Task<T> SendRequest<T>(HttpMethod method, HttpContent content, Action<HttpRequestHeaders> addHeaders = null) where T : class
        {
            using var client = WebApplicationFactory.CreateClient(new WebApplicationFactoryClientOptions { HandleCookies = false });

            using var request = new HttpRequestMessage(method, UrlBuilder.ToUri().ToString())
            {
                Content = content
            };

            // Request response compression
            request.Headers.Add("accept-encoding", "br");

            addHeaders?.Invoke(request.Headers);

            using var response = await client.SendAsync(request, CancellationToken.None);

            // Decompress Brotli if necessary
            if (response.Content.Headers.TryGetValues("Content-Encoding", out var ce) && ce.First() == "br")
            {
                var stream = await response.Content.ReadAsStreamAsync();
                var decompressed = new BrotliStream(stream, CompressionMode.Decompress);
                response.Content = new StreamContent(decompressed);
            }

            if (typeof(T) == typeof(HttpResponseMessage))
            {
                return response as T;
            }
            else if ((int)response.StatusCode >= 400)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                throw new Exception(responseContent);
            }
            else if (typeof(T) == typeof(string))
            {
                return await response.Content.ReadAsStringAsync() as T;
            }
            else
            {
                return response.Content != null
                    ? JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync())
                    : null;
            }
        }

        public void ResetUrl()
        {
            UrlBuilder = new UrlBuilder(BaseUrl);
        }

        public void Dispose()
        {
            WebApplicationFactory?.Dispose();
        }
    }

}