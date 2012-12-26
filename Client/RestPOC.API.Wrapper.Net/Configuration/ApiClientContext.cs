namespace RestPOC.API.Wrapper.Net.Configuration {
    using System;
    using System.Collections.Concurrent;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;

    /// <summary>
    /// The global configuration object for PinCom.API.Client.Net.
    /// </summary>
    public sealed class ApiClientContext
    {
        public Uri BaseUri { get; internal set; }
        internal string AuthorizationValue { get; set; }
        internal string ApiKey { get; set; }

        private static readonly Lazy<ConcurrentDictionary<Type, object>> _clients = new Lazy<ConcurrentDictionary<Type, object>>(() => new ConcurrentDictionary<Type, object>(), isThreadSafe: true);

        private static readonly Lazy<HttpClient> _httpClient =
            new Lazy<HttpClient>(
                () =>
                {
                    var httpClient = HttpClientFactory.Create(innerHandler: new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip });
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

                    return httpClient;

                }, isThreadSafe: true);

        internal ConcurrentDictionary<Type, object> Clients
        {
            get { return _clients.Value; }
        }

        internal HttpClient HttpClient
        {
            get
            {
                if (!_httpClient.IsValueCreated)
                {
                    if (this.BaseUri == null)
                    {
                        throw new ArgumentNullException("BaseUri");
                    }

                    if (string.IsNullOrEmpty(this.AuthorizationValue))
                    {
                        throw new ArgumentNullException("AuthorizationValue");
                    }

                    if (string.IsNullOrEmpty(this.ApiKey))
                    {
                        throw new ArgumentNullException("ApiKey");
                    }

                    this.InitializeHttpClient();
                }

                return _httpClient.Value;
            }
        }

        public static ApiClientContext Create(Action<ApiClientConfigurationExpression> action)
        {
            var apiClientContext = new ApiClientContext();
            var configurationExpression = new ApiClientConfigurationExpression(apiClientContext);

            action(configurationExpression);

            return apiClientContext;
        }

        private void InitializeHttpClient()
        {
            // Set default headers
            _httpClient.Value.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", this.AuthorizationValue);
            _httpClient.Value.DefaultRequestHeaders.Add("X-ApiKey", this.ApiKey);
            
            _httpClient.Value.BaseAddress = BaseUri;
        }
    }
}