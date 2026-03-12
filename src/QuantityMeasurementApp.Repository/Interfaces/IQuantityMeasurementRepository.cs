using QuantityMeasurementApp.Models.Entities;
using System.Collections.Generic;

namespace QuantityMeasurementApp.Repository
{
    public interface IQuantityMeasurementRepository
    {
        void Save(QuantityMeasurementEntity entity);
        IEnumerable<QuantityMeasurementEntity> GetAll();
    }
}
