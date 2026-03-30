using QuantityMeasurementApp.Models.Entities;

namespace QuantityMeasurementApp.Repository
{
    public interface IUserRepository
    {
        UserEntity? GetByEmail(string email);
        void Add(UserEntity user);
    }
}