using MediatR;

namespace Application.Features.Discounts.Queries;

public class GetDiscountByIdQry : IRequest<object>
{
    public Guid PkDiscountId { get; set; }
}
