using MediatR;

namespace StudentPlanner.Application.Commands;

public class SignUpCommand : IRequest<Unit>
{
}

public class SignUpCommandHandler : IRequestHandler<SignUpCommand, Unit>
{
    public Task<Unit> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        return Unit.Task;
    }
}