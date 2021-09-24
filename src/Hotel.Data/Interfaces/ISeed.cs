using Microsoft.EntityFrameworkCore;

namespace Hotel.Data.Interfaces
{
    public interface ISeed
    {
        void Populate(ModelBuilder context);
    }
}
