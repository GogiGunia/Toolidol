using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text;
using Toolidol.WebAPI.Interfaces;
using Toolidol.WebAPI.Models.ExternalViewModels.Facebook.Responses;
using Toolidol.WebAPI.Models;

namespace Toolidol.WebAPI.Services
{
    public class FacebookService : IFacebookService
    {
        private readonly HttpService _httpService;
        private readonly FacebookSettings _facebookSettings;
        private readonly ILogger<FacebookService> _logger;

        public FacebookService(
            HttpService httpService,
            IOptions<FacebookSettings> facebookSettings,
            ILogger<FacebookService> logger)
        {
            _httpService = httpService;
            _facebookSettings = facebookSettings.Value;
            _logger = logger;
        }

        /// <summary>
        /// Orchestrates the full authentication flow to get long-lived Page access tokens.
        /// </summary>
        public async Task<List<PageAccountResponse>> AuthenticateAndGetPageTokensAsync(string shortLivedUserToken)
        {
            _logger.LogInformation("Starting Facebook authentication flow.");

            // Step 1: Exchange the short-lived user token for a long-lived one.
            var longLivedUserToken = await GetLongLivedUserAccessTokenAsync(shortLivedUserToken);

            // Step 2: Use the long-lived user token to get long-lived page tokens.
            var pageTokens = await GetPageAccessTokensAsync(longLivedUserToken);

            _logger.LogInformation("Successfully retrieved {PageCount} page tokens.", pageTokens.Count);
            return pageTokens;
        }

        /// <summary>
        /// Exchanges a short-lived user access token for a long-lived one.
        /// </summary>
        private async Task<string> GetLongLivedUserAccessTokenAsync(string shortLivedToken)
        {
            _logger.LogInformation("Exchanging short-lived token for a long-lived user token.");
            var url = $"{_facebookSettings.GraphApiUrl}/oauth/access_token";

            var queryParams = new Dictionary<string, string>
        {
            { "grant_type", "fb_exchange_token" },
            { "client_id", _facebookSettings.AppId },
            { "client_secret", _facebookSettings.AppSecret },
            { "fb_exchange_token", shortLivedToken }
        };

            var response = await _httpService.SendRequestAsync("GET", url, null, queryParams, null);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Failed to get long-lived user token. Status: {StatusCode}, Response: {ErrorContent}", response.StatusCode, errorContent);
                throw new HttpRequestException($"Facebook API error while getting long-lived token: {errorContent}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<LongLivedTokenResponse>(content);

            if (tokenResponse == null || string.IsNullOrEmpty(tokenResponse.AccessToken))
            {
                _logger.LogError("Failed to deserialize or access token is missing in the response from Facebook.");
                throw new InvalidOperationException("Could not retrieve the long-lived access token from Facebook's response.");
            }

            _logger.LogInformation("Successfully obtained long-lived user token.");
            return tokenResponse.AccessToken;
        }

        /// <summary>
        /// Retrieves a list of pages and their permanent access tokens using a long-lived user token.
        /// </summary>
        private async Task<List<PageAccountResponse>> GetPageAccessTokensAsync(string longLivedUserToken)
        {
            _logger.LogInformation("Fetching page access tokens using long-lived user token.");
            var url = $"{_facebookSettings.GraphApiUrl}/me/accounts";

            var queryParams = new Dictionary<string, string>
        {
            { "access_token", longLivedUserToken },
            { "appsecret_proof", GenerateAppSecretProof(longLivedUserToken) }
        };

            var response = await _httpService.SendRequestAsync("GET", url, null, queryParams, null);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Failed to get page access tokens. Status: {StatusCode}, Response: {ErrorContent}", response.StatusCode, errorContent);
                throw new HttpRequestException($"Facebook API error while getting page tokens: {errorContent}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var pageListResponse = JsonSerializer.Deserialize<PageAccountListResponse>(content);

            if (pageListResponse == null)
            {
                _logger.LogError("Failed to deserialize the page list response from Facebook.");
                throw new InvalidOperationException("Could not retrieve the page list from Facebook's response.");
            }

            _logger.LogInformation("Successfully obtained page accounts list.");
            return pageListResponse.Data;
        }

        /// <summary>
        /// Generates the appsecret_proof for securing server-to-server API calls.
        /// </summary>
        private string GenerateAppSecretProof(string accessToken)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_facebookSettings.AppSecret));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(accessToken));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
