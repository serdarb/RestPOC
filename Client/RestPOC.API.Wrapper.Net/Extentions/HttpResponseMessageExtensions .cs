namespace RestPOC.API.Wrapper.Net {
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Newtonsoft.Json.Linq;

    internal static class HttpResponseMessageExtensions  {
        internal static async Task<HttpApiResponseMessage<TEntity>> GetHttpApiResponseAsync<TEntity>(this Task<HttpResponseMessage> responseTask)
        {
            HttpResponseMessage response = await responseTask.ConfigureAwait(false);
            HttpApiResponseMessage<TEntity> apiResponse = await response.GetHttpApiResponseAsync<TEntity>().ConfigureAwait(false);

            return apiResponse;
        }

        internal static async Task<HttpApiResponseMessage> GetHttpApiResponseAsync(this Task<HttpResponseMessage> responseTask)
        {
            HttpResponseMessage response = await responseTask.ConfigureAwait(false);
            var apiResponse = await response.GetHttpApiResponseAsync();

            return apiResponse;
        }

        internal static async Task<HttpApiResponseMessage<TEntity>> GetHttpApiResponseAsync<TEntity>(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                TEntity content = await response.Content.ReadAsAsync<TEntity>().ConfigureAwait(false);
                return response.GetHttpApiResponse(content);
            }
            
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                JToken httpError = await response.Content.ReadAsAsync<JToken>().ConfigureAwait(false);
                return response.GetHttpApiResponse<TEntity>(httpError);
            }

            return response.GetHttpApiResponse<TEntity>();
        }

        internal static async Task<HttpApiResponseMessage> GetHttpApiResponseAsync(this HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                JToken httpError = await response.Content.ReadAsAsync<JToken>().ConfigureAwait(false);
                return new HttpApiResponseMessage(response, httpError);
            }

            return new HttpApiResponseMessage(response);
        }

        internal static HttpApiResponseMessage<TEntity> GetHttpApiResponse<TEntity>(this HttpResponseMessage response)
        {
            return new HttpApiResponseMessage<TEntity>(response);
        }

        internal static HttpApiResponseMessage<TEntity> GetHttpApiResponse<TEntity>(this HttpResponseMessage response, TEntity entity)
        {

            return new HttpApiResponseMessage<TEntity>(response, entity);
        }

        internal static HttpApiResponseMessage<TEntity> GetHttpApiResponse<TEntity>(this HttpResponseMessage response, JToken httpError)
        {

            return new HttpApiResponseMessage<TEntity>(response, httpError);
        }
    }
}