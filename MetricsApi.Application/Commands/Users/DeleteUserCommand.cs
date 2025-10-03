using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsApi.Application.Commands.Users
{
    public record DeleteUserCommand(Guid Id) : IRequest;
}
