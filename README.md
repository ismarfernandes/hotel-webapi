# Hotel Web Api
Web API implemented using .Net platform to handle hotel room booking

### Technologies

* [ASP.NET Core 5](https://docs.microsoft.com/en-us/dotnet/core/dotnet-five)
* [Automapper](https://automapper.org/)
* [Entity Framework Core 5](https://docs.microsoft.com/en-us/ef/core/)
* [Fluent Validation](https://fluentvalidation.net/)
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-2019)
* [Swagger](https://swagger.io/solutions/api-documentation/)

## The challenge
Post-Covid scenario:
People are now free to travel everywhere but because of the pandemic, a lot of hotels went bankrupt. Some former famous travel places are left with only one hotel.
You’ve been given the responsibility to develop a booking API for the very last hotel in Cancun.


## Business rules
- All reservations start at least the next day of booking;
- The stay can’t be longer than 3 days;
- It can’t be reserved more than 30 days in advance;
- To simplify, the API is insecure.

## Environment requirements
- Install [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
- Install [EF Core CLI](https://docs.microsoft.com/pt-br/ef/core/cli/dotnet)
- Install [SQL Server](https://www.microsoft.com/en-ca/sql-server/sql-server-downloads) or use the [SQL Server Express LocalDB](https://www.microsoft.com/en-ca/sql-server/sql-server-downloads)

## Executing the project
```bash
# Clone the repo
git clone git@github.com:ismarfernandes/hotel-webapi.git

# Go to \src\Hotel.Api\appsettings.json file and set up the SQL Server ConnectionString 

# Access the source code folder
cd .\hotel-webapi\

# Run migrations
dotnet ef database update --project .\src\Hotel.Data\  --startup-project .\src\Hotel.Api\ --context HotelContext

# Run the project
dotnet run --project .\src\Hotel.Api\Hotel.Api.csproj
```


## Usage
The project can be tested using the Swagger already added to the project. In a web browser access the url: https://localhost:5001/swagger/index.html and enjoy.


## References

- [Understand how to create a Hotel Booking](https://sloboda-studio.com/blog/how-to-create-a-hotel-booking-website/)
- [Overview about Booking System Rules](https://smallbusiness.co.uk/how-to-create-an-online-booking-system-2550306/)


## Support

If you are having problems, please let us know by [raising a new issue](https://github.com/ismarfernandes/hotel-webapi/issues/new).
