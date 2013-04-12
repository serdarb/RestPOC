namespace RestPOC.API.Wrapper.Net {
    using System;
    using System.ComponentModel;

    using RestPOC.API.Wrapper.Net.Configuration;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ApiClientContextExtensions
    {
        public static IPeopleClient GetPeopleClient(this ApiClientContext apiClientContext)
        {
            return apiClientContext.GetClient<IPeopleClient>(() => new  PeopleClient(apiClientContext.HttpClient));
        }

        internal static TClient GetClient<TClient>(this ApiClientContext apiClientContext, Func<TClient> valueFactory)
        {
            return (TClient)apiClientContext.Clients.GetOrAdd(typeof(TClient), k => valueFactory());
        }
    }
}