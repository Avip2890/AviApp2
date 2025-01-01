﻿using MediatR;

namespace AviApp.Commands.CustomerCommands;

public class DeleteCustomerCommand : IRequest<bool>
{
    public int Id { get; set; }

    public DeleteCustomerCommand(int id)
    {
        Id = id;
    }
}