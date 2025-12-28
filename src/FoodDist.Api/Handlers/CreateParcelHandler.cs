using FoodDist.Core.Commands;
using FoodDist.Core.Entities;
using FoodDist.Infrastructure;
using MediatR;

namespace FoodDist.Api.Handlers;

public class CreateParcelHandler : IRequestHandler<CreateParcelCommand, Guid>
{
    private readonly ParcelRepository _repo;

    public CreateParcelHandler(ParcelRepository repo) => _repo = repo;

    public async Task<Guid> Handle(CreateParcelCommand request, CancellationToken cancellationToken)
    {
        var parcel = new Parcel { Id = Guid.NewGuid(), BeneficiaryPhone = request.BeneficiaryPhone, Status = request.Status };
        await _repo.AddAsync(parcel);
        return parcel.Id;
    }
}