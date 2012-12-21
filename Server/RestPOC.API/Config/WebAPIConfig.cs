﻿using RestPOC.API.Formatting;
using RestPOC.API.Model.RequestModels;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Validation;
using System.Web.Http.Validation.Providers;

namespace RestPOC.API.Config {

    public static class WebAPIConfig {

        public static void Configure(HttpConfiguration config) {

            Configure(config, true);
        }

        public static void Configure(HttpConfiguration config, bool registerTracer) {

            // Message Handlers

            // Formatters
            ConfigureFormatters(config.Formatters);

            // Filters

            // Default Services

            // If ExcludeMatchOnTypeOnly is true then we don't match on type only which means
            // that we return null if we can't match on anything in the request. This is useful
            // for generating 406 (Not Acceptable) status codes.
            config.Services.Replace(typeof(IContentNegotiator),
                new DefaultContentNegotiator(excludeMatchOnTypeOnly: true));

            // Remove all the validation providers 
            // except for DataAnnotationsModelValidatorProvider
            config.Services.RemoveAll(typeof(ModelValidatorProvider),
                validator => !(validator is DataAnnotationsModelValidatorProvider));

            // ParameterBindingRules

            // Any complex type parameter which is Assignable From 
            // IRequestCommand will be bound from the URI
            config.ParameterBindingRules.Insert(0,
                descriptor => typeof(IRequestCommand).IsAssignableFrom(descriptor.ParameterType)
                    ? new FromUriAttribute().GetBinding(descriptor) : null);
        }

        private static void ConfigureFormatters(MediaTypeFormatterCollection formatters) {

            // Remove unnecessary formatters
            MediaTypeFormatter jqueryFormatter = formatters.FirstOrDefault(x => x.GetType() == typeof(JQueryMvcFormUrlEncodedFormatter));
            formatters.Remove(formatters.XmlFormatter);
            formatters.Remove(formatters.FormUrlEncodedFormatter);
            formatters.Remove(jqueryFormatter);

            // Suppressing the IRequiredMemberSelector for all formatters
            foreach (var formatter in formatters) {

                formatter.RequiredMemberSelector = new SuppressedRequiredMemberSelector();
            }
        }
    }
}