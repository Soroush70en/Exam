using MediatR;

namespace Application.Features.Discounts.Commands;

public record DeleteDiscountCmd : IRequest<object>
{
    public Guid PkDiscountId { get; set; }
}
