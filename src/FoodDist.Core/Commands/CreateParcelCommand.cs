
using MediatR;
namespace FoodDist.Core.Commands;
public record CreateParcelCommand(string BeneficiaryPhone, string Status) : IRequest<Guid>;

