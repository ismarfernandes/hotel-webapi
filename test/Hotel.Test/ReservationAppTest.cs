using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Hotel.Api;
using Hotel.Application.Dtos.Request;
using Hotel.Application.Dtos.Response;
using Hotel.Domain.Enumerations;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Responses;

using Newtonsoft.Json;

using Xunit;

namespace Hotel.Test
{
    public class ReservationApplicationTest : IClassFixture<HotelWebApplicationFactory<Startup>>
    {
        #region Constructors
        public ReservationApplicationTest(HotelWebApplicationFactory<Api.Startup> factory)
        {
            _endPoint = "/api/Reservation";
            _factory = factory;
        }
        #endregion

        #region Fields
        private readonly HotelWebApplicationFactory<Startup> _factory;
        private string _endPoint;
        #endregion

        #region Tests Methods
        [Fact]
        public async void ShouldMakeBooking()
        {
            //Arrange
            var client = _factory.CreateClient();
            var dto = new ReservationCreateRequestDto
            {
                CheckIn = DateTime.Now.AddDays(1),
                CheckOut = DateTime.Now.AddDays(2)
            };

            //Act
            var jsonBodyRequest = JsonConvert.SerializeObject(dto);
            var response = await client.PostAsync($"{_endPoint}/MakeAsync", new StringContent(jsonBodyRequest, Encoding.UTF8, "application/json"));
            var jsonBodyResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Response>(jsonBodyResponse);
            var resultJson = JsonConvert.SerializeObject(result.Result);
            var createdBookingResponseDto = JsonConvert.DeserializeObject<ReservationResponseDto>(resultJson);

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
            var client = _factory.CreateClient();

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
            var jsonBodyRequest = JsonConvert.SerializeObject(dto);
            var response = await client.PutAsync($"{_endPoint}/ModifyAsync/{createBookingResponseDto.Id}", new StringContent(jsonBodyRequest, Encoding.UTF8, "application/json"));
            var jsonBodyResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Response>(jsonBodyResponse);
            var resultJson = JsonConvert.SerializeObject(result.Result);
            var modifiedBookingResponseDto = JsonConvert.DeserializeObject<ReservationResponseDto>(resultJson);

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
            var client = _factory.CreateClient();

            var createBookingDto = new ReservationCreateRequestDto
            {
                CheckIn = DateTime.Now.AddDays(5),
                CheckOut = DateTime.Now.AddDays(6)
            };

            var createBookingResponse = await MakeAsync(createBookingDto);
            var createBookingResponseDto = (ReservationResponseDto)createBookingResponse.Result;

            //Act
            var response = await client.DeleteAsync($"{_endPoint}/CancelAsync/{createBookingResponseDto.Id}");
            var jsonBodyResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Response>(jsonBodyResponse);
            var resultJson = JsonConvert.SerializeObject(result.Result);
            var canceledBookingResponseDto = JsonConvert.DeserializeObject<ReservationResponseDto>(resultJson);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.True(result.Success);
            Assert.True(canceledBookingResponseDto.Status == ReservationStatus.Canceled.ToString());
        }
        #endregion

        #region Private Methods
        protected async Task<IResponse> MakeAsync(ReservationCreateRequestDto dto)
        {
            //Arrange
            var client = _factory.CreateClient();

            //Act
            var jsonBodyRequest = JsonConvert.SerializeObject(dto);
            var response = await client.PostAsync($"{_endPoint}/MakeAsync", new StringContent(jsonBodyRequest, Encoding.UTF8, "application/json"));
            var jsonBodyResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Response>(jsonBodyResponse);
            var resultJson = JsonConvert.SerializeObject(result.Result);
            var resultDto = JsonConvert.DeserializeObject<ReservationResponseDto>(resultJson);

            result.Result = resultDto;

            return result;
        }
        #endregion
    }
}
