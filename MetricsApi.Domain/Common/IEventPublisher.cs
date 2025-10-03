using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsApi.Domain.Common
{
    public interface IEventPublisher
    {
        /// <summary>
        /// Publica um evento de domínio
        /// </summary>
        /// <param name="domainEvent">O evento</param>
        Task PublishAsync(object domainEvent);
    }
}
