namespace RestPOC.API.Wrapper.Net.Configuration {
    using System;
    using System.Configuration;
    using System.Text;

    public class ApiClientConfigurationExpression
    {
        private readonly ApiClientContext _apiClientContext;

        internal ApiClientConfigurationExpression(ApiClientContext apiClientContext)
        {
            if (apiClientContext == null)
            {
                throw new ArgumentNullException("apiClientContext");
            }

            this._apiClientContext = apiClientContext;
        }

        public ApiClientConfigurationExpression SetCredentialsFromAppSetting(string usernameAppSettingKey, string passwordAppSettingKey, string apiKeyAppSettingKey)
        {
            if (string.IsNullOrEmpty(usernameAppSettingKey))
            {
                throw new ArgumentNullException("usernameAppSettingKey");
            }

            if (string.IsNullOrEmpty(passwordAppSettingKey))
            {
                throw new ArgumentNullException("passwordAppSettingKey");
            }

            if (string.IsNullOrEmpty(apiKeyAppSettingKey))
            {
                throw new ArgumentNullException("apiKeyAppSettingKey");
            }

            var username = ConfigurationManager.AppSettings[usernameAppSettingKey];
            var password = ConfigurationManager.AppSettings[passwordAppSettingKey];
            var apiKey = ConfigurationManager.AppSettings[apiKeyAppSettingKey];

            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(string.Format("{0} can not be null", usernameAppSettingKey));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(string.Format("{0} can not be null", passwordAppSettingKey));
            }

            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentNullException(string.Format("{0} can not be null", apiKeyAppSettingKey));
            }

            this._apiClientContext.ApiKey = apiKey;
            this._apiClientContext.AuthorizationValue = EncodeToBase64(string.Concat(username, ":", password));

            return this;
        }

        public ApiClientConfigurationExpression ConnectTo(string baseUri)
        {
            if (string.IsNullOrEmpty(baseUri))
            {
                throw new ArgumentNullException("baseUri");
            }

            this._apiClientContext.BaseUri = new Uri(baseUri);

            return this;
        }

        private static string EncodeToBase64(string value)
        {
            byte[] toEncodeAsBytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(toEncodeAsBytes);
        }
    }
}