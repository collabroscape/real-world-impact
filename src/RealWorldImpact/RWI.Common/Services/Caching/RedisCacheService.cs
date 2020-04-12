using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RWI.Common.Configuration.Caching;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RWI.Common.Services.Caching
{
    public class RedisCacheService : ICacheService
    {
        private IConfiguration _configuration;
        private RedisCacheConfiguration _redisCacheConfiguration;
        private ConnectionMultiplexer _connection;

        private DateTime _lastInternalError;
        private DateTime _lastConnectionFailure;
        private double _exceptionThresholdMinutes = -0.5;
        private bool _resetConnection = false;
        private JsonSerializerSettings _jsonSerializerSettings;

        public RedisCacheService(IConfiguration configuration)
        {
            _configuration = configuration;
            _redisCacheConfiguration = new RedisCacheConfiguration();
            _configuration.GetSection("Redis").Bind(_redisCacheConfiguration);

            _jsonSerializerSettings = new JsonSerializerSettings()
            {
                MaxDepth = 10
            };

            ResetConnection();
        }

        private void ConnectionMultiplexerInternalError(object sender, InternalErrorEventArgs e)
        {
            if (e.Exception != null && DateTime.UtcNow.AddMinutes(_exceptionThresholdMinutes) > _lastInternalError)
            {
                _lastInternalError = DateTime.UtcNow;
            }
        }

        private void ConnectionMultiplexerConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            if (e.Exception != null && DateTime.UtcNow.AddMinutes(_exceptionThresholdMinutes) > _lastConnectionFailure)
            {
                _resetConnection = true;
                _lastConnectionFailure = DateTime.UtcNow;
            }
        }

        private IDatabase GetCacheInstance()
        {
            if (_resetConnection)
            {
                ResetConnection();
            }

            IDatabase cache = _connection.GetDatabase(_redisCacheConfiguration.DatabaseIndexValue);
            return cache;
        }

        public async Task<T> GetCachedItemAsync<T>(string key, Func<Task<T>> fallback = null) where T : class
        {
            T instance = null;
            RedisValue cached = RedisValue.Null;

            IDatabase cache = GetCacheInstance();
            if (cache != null)
            {
                cached = await cache.StringGetAsync(key);
                if (cached.HasValue && !cached.IsNullOrEmpty)
                {
                    if (typeof(T).FullName == "System.String")
                    {
                        instance = cached.ToString() as T;
                    }
                    else
                    {
                        instance = JsonConvert.DeserializeObject<T>(cached, _jsonSerializerSettings);
                    }
                }
            }

            if (instance == null && fallback != null)
            {
                instance = await fallback();
                await AddItemAsync(instance, key);
            }
            return instance;
        }

        public T GetCachedItem<T>(string key, Func<T> fallback = null) where T : class
        {
            T instance = null;

            IDatabase cache = GetCacheInstance();

            if (cache != null)
            {
                var cached = cache.StringGet(key);
                if (cached.HasValue && !cached.IsNullOrEmpty)
                {
                    if (typeof(T).FullName == "System.String")
                    {
                        instance = cached.ToString() as T;
                    }
                    else
                    {
                        instance = JsonConvert.DeserializeObject<T>(cached, _jsonSerializerSettings);
                    }
                }
            }

            if (instance == null && fallback != null)
            {
                instance = fallback();
                AddItem(instance, key);
            }
            return instance;
        }

        public async Task AddItemAsync(object objectToCache, string key)
        {
            if (objectToCache != null)
            {
                string stringToCache = (objectToCache is string ?
                    objectToCache as string :
                    JsonConvert.SerializeObject(objectToCache, _jsonSerializerSettings));
                IDatabase cache = GetCacheInstance();
                if (cache != null)
                {
                    await cache.StringSetAsync(key, stringToCache, TimeSpan.FromSeconds(_redisCacheConfiguration.CacheDurationValue), When.Always);
                }
            }
        }

        public void AddItem(object objectToCache, string key)
        {
            if (objectToCache != null)
            {
                string stringToCache = (objectToCache is string ?
                    objectToCache as string :
                    JsonConvert.SerializeObject(objectToCache, _jsonSerializerSettings));
                IDatabase cache = GetCacheInstance();
                if (cache != null)
                {
                    cache.StringSet(key, stringToCache, TimeSpan.FromSeconds(_redisCacheConfiguration.CacheDurationValue), When.Always);
                }
            }
        }

        public async Task DeleteItemAsync(string key)
        {
            IDatabase cache = GetCacheInstance();
            if (cache != null)
            {
                await cache.KeyDeleteAsync(key);
            }
        }

        public void DeleteItem(string key)
        {
            IDatabase cache = GetCacheInstance();
            if (cache != null)
            {
                cache.KeyDelete(key);
            }
        }

        public void ResetConnection()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
                _resetConnection = false;
            }

            if (_connection == null)
            {
                _connection = ConnectionMultiplexer.Connect(_configuration.GetConnectionString("RedisCache"));
                _connection.ConnectionFailed += ConnectionMultiplexerConnectionFailed;
                _connection.InternalError += ConnectionMultiplexerInternalError;
            }
        }

    }
}
