using AviApp.Domain.Entities;
using AviApp.Models;

namespace AviApp.Mappers;

public static class CustomerMapper
{
    public static Customer ToEntity(this CustomerDto model)
    {
        return new Customer
        {
            Id = model.Id,
            CustomerName = model.CustomerName,
            Phone = model.Phone
        };
    }

    public static CustomerDto ToDto(this Customer entity)
    {
        return new CustomerDto
        {
            Id = entity.Id,
            CustomerName = entity.CustomerName,
            Phone = entity.Phone
        };
    }
}