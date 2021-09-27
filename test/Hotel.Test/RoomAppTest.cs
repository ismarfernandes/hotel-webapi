using System;

using Hotel.Api;
using Hotel.Application.Dtos.Response;
using Hotel.Shared.Responses;

using Newtonsoft.Json;

using Xunit;

namespace Hotel.Test
{
    public class RoomAppTest : IClassFixture<HotelWebApplicationFactory<Startup>>
    {
        #region Constructors
        public RoomAppTest(HotelWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _endPoint = "/api/Room";
        }
        #endregion

        #region Fields
        private readonly HotelWebApplicationFactory<Startup> _factory;
        private readonly string _endPoint;
        #endregion

        #region Public Methods
        [Fact]
        public async void ShouldRoomAvailable()
        {
            //Arrange
            var client = _factory.CreateClient();
            var checkIn = DateTime.Now.AddDays(1);
            var checkOut = DateTime.Now.AddDays(2);
            var checkInJson = JsonConvert.SerializeObject(checkIn).Replace("\"", "");
            var checkOutJson = JsonConvert.SerializeObject(checkOut).Replace("\"", "");

            //Act
            var response = await client.GetAsync($"{_endPoint}/CheckAvailabilityAsync?checkIn={checkInJson}&checkOut={checkOutJson}");
            var jsonBodyResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Response>(jsonBodyResponse);
            var resultJson = JsonConvert.SerializeObject(result.Result);
            var roomResponseDto = JsonConvert.DeserializeObject<RoomResponseDto>(resultJson);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.True(result.Success);
            Assert.NotNull(result.Result);
            Assert.True(roomResponseDto.Id != 0);
        }
        #endregion
    }
}
