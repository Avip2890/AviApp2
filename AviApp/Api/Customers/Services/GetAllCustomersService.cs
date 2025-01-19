using AviApp.Domain.Context;
using AviApp.Results;
using Microsoft.EntityFrameworkCore;

namespace AviApp.Api.Customers.Services;

public class GetAllCustomersService(AvipAppDbContext context)
{
    public async Task<Result<List<Domain.Entities.Customer>>> ExecuteAsync(CancellationToken cancellationToken)
    {
        var customers = await context.Customers.AsNoTracking().ToListAsync(cancellationToken);

        if (!customers.Any())
        {
            return Result<List<Domain.Entities.Customer>>.Failure("No customers found.");
        }

        return Result<List<Domain.Entities.Customer>>.Success(customers);
    }
}