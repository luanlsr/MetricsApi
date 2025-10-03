using MediatR;
using MetricsApi.Application.Queries.Users;
using MetricsApi.CrossCutting.Dtos.Users;
using MetricsApi.Domain.Abstractions;
using MetricsApi.Domain.Repositories;
using MetricsApi.Domain.ValueObjects;

namespace MetricsApi.Application.Handlers.Users
{
    public class UserQueryHandler :
        IRequestHandler<GetUserByIdQuery, UserDto>,
        IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public UserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            if (user is null) return null!;

            return new UserDto(
                user.Id,
                user.Name.FullName,
                user.Email.Address,
                user.Active,
                user.CreatedAt,
                user.UpdatedAt
            );
        }

        public async Task<UserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var emailVo = new Email(request.Email);
            var user = await _userRepository.GetByEmailAsync(emailVo);

            if (user is null) return null!;

            return new UserDto(
                user.Id,
                user.Name.FullName,
                user.Email.Address,
                user.Active,
                user.CreatedAt,
                user.UpdatedAt
            );
        }

        public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync(cancellationToken);

            return users.Select(u => new UserDto(
                u.Id,
                u.Name.FullName,
                u.Email.Address,
                u.Active,
                u.CreatedAt,
                u.UpdatedAt
            )).ToList();
        }
    }
}
