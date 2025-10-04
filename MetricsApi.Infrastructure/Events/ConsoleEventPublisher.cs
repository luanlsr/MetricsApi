using MetricsApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsApi.Infrastructure.Events
{
    public class ConsoleEventPublisher : IEventPublisher
    {
        public Task PublishAsync(object domainEvent)
        {
            Console.WriteLine($"[DomainEvent] {domainEvent.GetType().Name}: {System.Text.Json.JsonSerializer.Serialize(domainEvent)}");
            return Task.CompletedTask;
        }
    }
}
