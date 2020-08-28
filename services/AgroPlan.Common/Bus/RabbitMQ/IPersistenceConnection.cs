using System;
using RabbitMQ.Client;

namespace AgroPlan.Common.RabbitMQ
{
    public interface IPersistenceConnection
        : IDisposable
    {
        bool IsConnected {get;}
        bool TryConnect();
        IModel CreateModel();
    }
}