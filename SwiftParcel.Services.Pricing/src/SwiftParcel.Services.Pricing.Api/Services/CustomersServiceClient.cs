using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.HTTP;
using SwiftParcel.Services.Pricing.Api.dto;

namespace SwiftParcel.Services.Pricing.Api.Services
{
    public class CustomersServiceClient : ICustomersServiceClient
    {
        private readonly IHttpClient _httpClient;
        private readonly string _url;

        public CustomersServiceClient(IHttpClient httpClient, HttpClientOptions options)
        {
            _httpClient = httpClient;
            _url = options.Services["customers"];
        }

        public Task<CustomerDto> GetAsync(Guid id)
            => _httpClient.GetAsync<CustomerDto>($"{_url}/customers/{id}");
    }
}