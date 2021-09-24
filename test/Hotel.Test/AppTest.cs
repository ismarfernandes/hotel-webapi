using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Hotel.Application.Dtos.Request;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Responses;

using Microsoft.AspNetCore.Mvc.Testing;

using Xunit;

namespace Hotel.Test
{
    public class AppTest : IClassFixture<WebApplicationFactory<Api.Startup>>
    {
        #region Constructors
        public AppTest(WebApplicationFactory<Api.Startup> factory)
        {
            _factory = factory;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            _jsonOptions.Converters.Add(new JsonStringEnumConverter());
        }
        #endregion

        #region Fields
        protected string baseApiUrl = "/api/";
        protected readonly WebApplicationFactory<Api.Startup> _factory;
        protected readonly JsonSerializerOptions _jsonOptions;
        #endregion

        #region Protected Methods
        protected HttpClient CreateClient()
        {
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        #region Private Methods
        protected async Task<IResponse> MakeAsync(ReservationCreateRequestDto dto)
        {
            //Arrange
            var client = CreateClient();

            //Act
            var jsonBodyRequest = JsonSerializer.Serialize(dto, _jsonOptions);
            var response = await client.PostAsync($"{baseApiUrl}/MakeAsync", new StringContent(jsonBodyRequest, Encoding.UTF8, "application/json"));
            var jsonBodyResponse = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Response>(jsonBodyResponse, _jsonOptions);

            return result;
        }
        #endregion
        #endregion
    }
}
