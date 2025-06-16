using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Web;

namespace Toolidol.WebAPI.Services
{
    public class HttpService
    {
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpService(ILogger<HttpService> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> SendRequestAsync(string httpMethod, string url, Dictionary<string, string>? headers, Dictionary<string, string>? queryParams, object? content, CancellationToken cancellationToken = default)
        {
            HttpContent? contentObj = content == null ? null : content switch
            {
                string stringContent => new StringContent(stringContent),
                byte[] blobContent => new ByteArrayContent(blobContent),
                Stream streamContent => new StreamContent(streamContent),
                object => JsonContent.Create(content),
                _ => throw new NotImplementedException("Unsupported content-type")
            };
            return await this.SendRequestAsync(httpMethod, url, headers, queryParams, contentObj, cancellationToken);
        }

        private async Task<HttpResponseMessage> SendRequestAsync(string httpMethod, string url, Dictionary<string, string>? headers, Dictionary<string, string>? queryParams, HttpContent? content, CancellationToken cancellationToken = default)
        {
            using var httpClient = _httpClientFactory.CreateClient();

            UriBuilder uriBuilder = new(url);
            if (queryParams != null)
            {
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                foreach (KeyValuePair<string, string> queryParam in queryParams)
                    query.Add(queryParam.Key, queryParam.Value);
                uriBuilder.Query = query.ToString();
            }

            HttpRequestMessage request = new(new HttpMethod(httpMethod), uriBuilder.Uri);
            if (content != null)
                request.Content = content;

            request.Headers.Add("Toolidol-App", "Verifications");
            if (headers != null)
                foreach (var keyValue in headers)
                    request.Headers.Add(keyValue.Key, keyValue.Value);

            _logger.LogDebug("{httpMethod} {url}\n{request}", httpMethod, url, request.ToString());
            return await httpClient.SendAsync(request, cancellationToken);
        }
    }
}