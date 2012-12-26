﻿namespace RestPOC.API.Wrapper.Net
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;

    using Newtonsoft.Json.Linq;

    public class HttpApiResponseMessage<TModel> : HttpApiResponseMessage
    {
        public HttpApiResponseMessage(HttpResponseMessage response)
            : base(response)
        {
        }

        public HttpApiResponseMessage(HttpResponseMessage response, JToken httpError)
            : base(response, httpError)
        {
        }

        public HttpApiResponseMessage(HttpResponseMessage response, TModel model) :
            base(response)
        {

            this.Model = model;
        }

        public TModel Model { get; private set; }
    }

    public class HttpApiResponseMessage
    {

        internal const string ModelStateKey = "ModelState";

        public HttpApiResponseMessage(HttpResponseMessage response, JToken httpError)
            : this(response)
        {

            if (httpError == null)
            {
                throw new ArgumentNullException("httpError");
            }

            JToken modelState = httpError[ModelStateKey];

            if (modelState != null)
            {

                this.ModelState = httpError[ModelStateKey].ToObject<Dictionary<string, string[]>>();
            }

            this.HttpError = httpError;
        }

        public HttpApiResponseMessage(HttpResponseMessage response)
        {

            if (response == null)

                this.Response = response;
        }

        /// <summary>
        /// Represents the HttpResponseMessage for the request.
        /// </summary>
        public HttpResponseMessage Response { get; private set; }

        /// <summary>
        /// Determines if the response is a success or not.
        /// </summary>
        public bool IsSuccess
        {
            get
            {
                return (Response != null) ? this.Response.IsSuccessStatusCode : false;
            }
        }

        /// <summary>
        /// Represents the HTTP error message retrieved from the server if the response has "400 Bad Request" status code.
        /// </summary>
        public JToken HttpError { get; private set; }

        /// <summary>
        /// Represents the ModelState if the response has "400 Bad Request" status code and ModelState is available.
        /// </summary>
        public Dictionary<string, string[]> ModelState { get; private set; }
    }
}