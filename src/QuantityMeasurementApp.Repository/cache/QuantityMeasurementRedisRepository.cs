using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using QuantityMeasurementApp.Models.Entities;
using StackExchange.Redis;

namespace QuantityMeasurementApp.Repository
{
    /// <summary>
    /// Cache-aside repository that uses Redis for read optimization and SQL repository as source of truth.
    /// </summary>
    public sealed class QuantityMeasurementRedisRepository : IQuantityMeasurementRepository
    {
        private const string CacheKey = "quantity-measurements:all";
        private static readonly TimeSpan CacheTtl = TimeSpan.FromMinutes(10);

        private readonly IQuantityMeasurementRepository _databaseRepository;
        private readonly IDatabase _redisDatabase;

        public QuantityMeasurementRedisRepository(
            IQuantityMeasurementRepository databaseRepository,
            IConnectionMultiplexer connectionMultiplexer
        )
        {
            _databaseRepository = databaseRepository;
            _redisDatabase = connectionMultiplexer.GetDatabase();
        }

        public void Save(QuantityMeasurementEntity entity)
        {
            _databaseRepository.Save(entity);
            // Invalidate list cache to keep reads consistent after writes.
            _redisDatabase.KeyDelete(CacheKey);
        }

        public IEnumerable<QuantityMeasurementEntity> GetAll()
        {
            // Return cached payload when available to avoid repeated SQL reads.
            var cached = _redisDatabase.StringGet(CacheKey);
            if (cached.HasValue)
            {
                var payload = JsonSerializer.Deserialize<List<CachedQuantityMeasurement>>(cached!);
                if (payload is not null)
                {
                    return payload
                        .Select(entry =>
                            QuantityMeasurementEntity.Rehydrate(
                                entry.Id,
                                entry.Description,
                                entry.IsError,
                                entry.ErrorMessage,
                                entry.CreatedAt
                            )
                        )
                        .ToList();
                }
            }

            var fresh = _databaseRepository.GetAll().ToList();
            var serialized = JsonSerializer.Serialize(
                fresh.Select(entry => new CachedQuantityMeasurement(entry)).ToList()
            );
            // Cache fresh data with TTL to reduce DB load while limiting staleness.
            _redisDatabase.StringSet(CacheKey, serialized, CacheTtl);

            return fresh;
        }

        private sealed record CachedQuantityMeasurement(
            Guid Id,
            string Description,
            bool IsError,
            string ErrorMessage,
            DateTime CreatedAt
        )
        {
            public CachedQuantityMeasurement(QuantityMeasurementEntity source)
                : this(
                    source.Id,
                    source.Description,
                    source.IsError,
                    source.ErrorMessage,
                    source.CreatedAt
                ) { }
        }
    }
}