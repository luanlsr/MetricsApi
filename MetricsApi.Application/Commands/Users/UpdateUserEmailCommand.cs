using MediatR;
using MetricsApi.CrossCutting.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsApi.Application.Commands.Users
{
    /// <summary>
    /// Comando para atualizar o email de um usuário
    /// </summary>
    public record UpdateUserEmailCommand(Guid Id, string NewEmail) : IRequest<UserDto>;
}
