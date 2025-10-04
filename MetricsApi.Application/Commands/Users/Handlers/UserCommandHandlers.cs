using MediatR;
using MetricsApi.Application.Commands.Users;
using MetricsApi.CrossCutting.Dtos.Users;
using MetricsApi.Domain.Abstractions;
using MetricsApi.Domain.Entities.Users;
using MetricsApi.Domain.Repositories;
using MetricsApi.Domain.ValueObjects;

namespace MetricsApi.Application.Commands.Users.Handlers
{
    public class UserCommandHandler :
        IRequestHandler<CreateUserCommand, UserDto>,
        IRequestHandler<UpdateUserEmailCommand, UserDto>,
        IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UserCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var name = PersonName.FromFullName(request.Name);
            var email = new Email(request.Email);

            var user = User.Create(name, email);

            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new UserDto(
                user.Id,
                user.Name.FullName,
                user.Email.Address,
                user.Active,
                user.CreatedAt,
                user.UpdatedAt
            );
        }

        public async Task<UserDto> Handle(UpdateUserEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            if (user is null) throw new KeyNotFoundException("User not found");

            var email = new Email(request.NewEmail);
            user.UpdateEmail(email);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new UserDto(
                user.Id,
                user.Name.FullName,
                user.Email.Address,
                user.Active,
                user.CreatedAt,
                user.UpdatedAt
            );
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            if (user is null) throw new KeyNotFoundException("User not found");

            _userRepository.Remove(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
