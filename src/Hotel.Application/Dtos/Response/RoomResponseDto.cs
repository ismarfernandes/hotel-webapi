namespace Hotel.Application.Dtos.Response
{
    public class RoomResponseDto :EntityResponseDto
    {
        public int Number { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}
