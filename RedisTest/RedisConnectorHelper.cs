using StackExchange.Redis;
using System;

namespace RedisTest
{
    public class RedisConnectorHelper
    {
        private static string RedisConnectionString => "XXX"; // Redis Connection String
        private static int DbIndex => 5; // Redis Db Number 
        static RedisConnectorHelper()
        {
            var options = ConfigurationOptions.Parse(RedisConnectionString);
            options.DefaultDatabase = DbIndex;
            options.AbortOnConnectFail = true;

            lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(options);
            });
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection;

        private static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }

        public static IDatabase GetDB
        {
            get 
            {
                return Connection.GetDatabase(DbIndex);
            }
        }

    }
}
