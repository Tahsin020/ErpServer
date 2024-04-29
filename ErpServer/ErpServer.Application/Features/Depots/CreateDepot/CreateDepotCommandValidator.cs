using FluentValidation;

namespace ErpServer.Application.Features.Depots.CreateDepot;

public sealed class CreateDepotCommandValidator : AbstractValidator<CreateDepotCommand>
{
    public CreateDepotCommandValidator()
    {
        RuleFor(p => p.Name).MinimumLength(3).MaximumLength(50);
    }
}

