using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class ConnectionWrapper: IDisposable
    {

        private IConnection _connection;
        private readonly ConnectionFactory factory;

        public ConnectionWrapper(ConnectionFactory factory)
        {
            this.factory = factory;
        }

        public IConnection GetConnection()
        {
            if (this._connection == null)
            {
                this._connection = this.factory.CreateConnection();
            }

            return this._connection;
        }

        public void Dispose()
        {

            if (this._connection != null)
            {
                this._connection.Close();
            }
        }
    }
}
