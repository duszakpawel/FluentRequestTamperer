﻿using RequestObfuscator.Client;
using RequestObfuscator.Api;
using RequestObfuscator.Enums;

namespace RequestObfuscator.Usage
{
    public class DemoClient : ApiClient
    {
        public override IApiBuilder Configure(IApiBuilder api)
        {
            api.WithScheme("https")
               .WithHost("reqres.in")
               .RequestContentType(RequestContentTypeEnum.Json);

            api.ForRequest()
                .WithPath(path => path.Contains("/api/users/2"))
                //.MethodType(HttpMethodEnum.GET)
                .AbortRequest();
                //.BeginTamper()
                //.TamperFragment("test")
                //.TamperHeader("test", "test2")
                //.TamperMethodType(HttpMethodEnum.POST)
                //.TamperPath(path => path.Replace("/api/users/2", "/api/users/3"));

            return api;
        }
    }
}
