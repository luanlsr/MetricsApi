using MediatR;
using MetricsApi.CrossCutting.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsApi.Application.Commands.Users
{
    public record CreateUserCommand(string Name, string Email) : IRequest<UserDto>;

}
