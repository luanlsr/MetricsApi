using MediatR;
using MetricsApi.CrossCutting.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsApi.Application.Queries.Users
{
    public record GetUserByIdQuery(Guid Id) : IRequest<UserDto>;
}
