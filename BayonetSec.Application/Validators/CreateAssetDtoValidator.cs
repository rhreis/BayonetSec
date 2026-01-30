using BayonetSec.Application.DTOs;
using FluentValidation;

namespace BayonetSec.Application.Validators;

public class CreateAssetDtoValidator : BaseValidator<CreateAssetDto>
{
    public CreateAssetDtoValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty().WithMessage("ProjectId is required");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Type is required")
            .MaximumLength(50).WithMessage("Type must not exceed 50 characters");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters");

        RuleFor(x => x.IpAddress)
            .MaximumLength(45).WithMessage("IP Address must not exceed 45 characters");

        RuleFor(x => x.Url)
            .MaximumLength(500).WithMessage("URL must not exceed 500 characters");
    }
}