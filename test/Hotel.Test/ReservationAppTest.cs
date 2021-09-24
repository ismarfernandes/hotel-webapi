using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;

using Hotel.Application.Dtos.Request;
using Hotel.Application.Dtos.Response;
using Hotel.Domain.Enumerations;
using Hotel.Shared.Responses;

using Microsoft.AspNetCore.Mvc.Testing;

using Xunit;

namespace Hotel.Test
{
    public class ReservationApplicationTest : AppTest
    {
        #region Constructors
        public ReservationApplicationTest(WebApplicationFactory<Api.Startup> factory) : base(factory) 
        {
            baseApiUrl = "/api/Reservation";
        }
        #endregion

        #region Tests Methods
        [Fact]
        public async void ShouldMakeBooking()
        {
            //Arrange
            var client = CreateClient();
            var dto = new ReservationCreateRequestDto
            {
                CheckIn = DateTime.Now.AddDays(1),
                CheckOut = DateTime.Now.AddDays(2)
            };

            //Act
            var jsonBodyRequest = JsonSerializer.Serialize(dto, _jsonOptions);
            var response = await client.PostAsync($"{baseApiUrl}/MakeAsync", new StringContent(jsonBodyRequest, Encoding.UTF8, "application/json"));
            var jsonBodyResponse = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Response>(jsonBodyResponse, _jsonOptions);
            var createdBookingResponseDto = (ReservationResponseDto)result.Result;

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.True(result.Success);
            Assert.True(createdBookingResponseDto.CheckIn == dto.CheckIn);
            Assert.True(createdBookingResponseDto.CheckOut == dto.CheckOut);
            Assert.True(createdBookingResponseDto.Status == ReservationStatus.Reserved.ToString());
            Assert.Null(createdBookingResponseDto.UpdatedAt);
        }

        [Fact]
        public async void ShouldModifyBooking()
        {
            //Arrange
            var client = CreateClient();

            var createBookingDto = new ReservationCreateRequestDto
            {
                CheckIn = DateTime.Now.AddDays(3),
                CheckOut = DateTime.Now.AddDays(4)
            };

            var createBookingResponse = await MakeAsync(createBookingDto);

            var createBookingResponseDto = (ReservationResponseDto)createBookingResponse.Result;

            var dto = new ReservationModifyRequestDto
            {
                CheckIn = DateTime.Now.AddDays(13),
                CheckOut = DateTime.Now.AddDays(14)
            };

            //Act
            var jsonBodyRequest = JsonSerializer.Serialize(dto, _jsonOptions);
            var response = await client.PutAsync($"{baseApiUrl}/ModifyAsync/{createBookingResponseDto.Id}", new StringContent(jsonBodyRequest, Encoding.UTF8, "application/json"));
            var jsonBodyResponse = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Response>(jsonBodyResponse, _jsonOptions);
            var modifiedBookingResponseDto = (ReservationResponseDto)result.Result;

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.True(result.Success);
            Assert.True(modifiedBookingResponseDto.CheckIn == dto.CheckIn);
            Assert.True(modifiedBookingResponseDto.CheckOut == dto.CheckOut);
            Assert.NotNull(modifiedBookingResponseDto.UpdatedAt);
        }

        [Fact]
        public async void ShouldCancelBooking()
        {
            //Arrange
            var client = CreateClient();

            var createBookingDto = new ReservationCreateRequestDto
            {
                CheckIn = DateTime.Now.AddDays(5),
                CheckOut = DateTime.Now.AddDays(6)
            };

            var createBookingResponse = await MakeAsync(createBookingDto);

            var createBookingResponseDto = (ReservationResponseDto)createBookingResponse.Result;

            //Act
            var response = await client.DeleteAsync($"{baseApiUrl}/CancelAsync/{createBookingResponseDto.Id}");
            var jsonBodyResponse = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Response>(jsonBodyResponse, _jsonOptions);

            var canceledBookingResponseDto = (ReservationResponseDto)result.Result;

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.True(result.Success);
            Assert.True(canceledBookingResponseDto.Status == ReservationStatus.Canceled.ToString());
        }
        #endregion
    }
}
