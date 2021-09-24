using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;

using Hotel.Api;
using Hotel.Application.Dtos.Request;
using Hotel.Application.Dtos.Response;
using Hotel.Domain.Enumerations;
using Hotel.Shared.Responses;

using Microsoft.AspNetCore.Mvc.Testing;

using Xunit;

namespace Hotel.Test
{
    public class RoomAppTest : AppTest
    {
        #region Constructors
        public RoomAppTest(WebApplicationFactory<Startup> factory) : base(factory)
        {
            baseApiUrl = "/api/Room";
        }
        #endregion

        #region Public Methods
        [Fact]
        public async void ShouldMakeBooking()
        {
            //Arrange
            var client = CreateClient();
            var checkIn = DateTime.Now.AddDays(1);
            var checkOut = DateTime.Now.AddDays(2);
            var checkInJson = JsonSerializer.Serialize(checkIn).Replace("\"", "");
            var checkOutJson = JsonSerializer.Serialize(checkOut).Replace("\"", "");

            //Act
            var response = await client.GetAsync($"{baseApiUrl}/CheckAvailabilityAsync?checkIn={checkInJson}&checkOut={checkOutJson}");
            var jsonBodyResponse = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Response>(jsonBodyResponse, _jsonOptions);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.True(result.Success);
            Assert.NotNull(result.Result);
        }
        #endregion
    }
}
