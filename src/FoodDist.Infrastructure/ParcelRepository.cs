using System.Data;
using Dapper;
using FoodDist.Core.Entities;

namespace FoodDist.Infrastracture;

public class ParcelRepository
{
    private readonly IDbConnection _db;

    public ParcelRepository(IDbConnection db) => _db = db;

    public async Task AddAsync(Parcel parcel)
    {
        var sql = "INSERT INTO Parcels (Id, BeneficiaryPhone, Status) VALUES (@Id, @BeneficiaryPhone, @Status)";
        await _db.ExecuteAsync(sql, parcel);
    }

    public async Task<IEnumerable<Parcel>> GetAllAsync()
        => await _db.QueryAsync<Parcel>("SELECT * FROM Parcels");
}