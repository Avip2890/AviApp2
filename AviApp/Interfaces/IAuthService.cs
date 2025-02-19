using AviApp.Domain.Entities;
using AviApp.Results;

namespace AviApp.Interfaces;

public interface IAuthService
{ 
    Task<Result<string>> LoginAsync(string email, string password, CancellationToken cancellationToken);
}