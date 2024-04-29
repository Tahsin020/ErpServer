﻿using ErpServer.Domain.Entities;
using ErpServer.Domain.Repositories;
using ErpServer.Infrastructure.Context;
using GenericRepository;

namespace ErpServer.Infrastructure.Repositories;

internal sealed class OrderRepository(ApplicationDbContext context) : Repository<Order, ApplicationDbContext>(context), IOrderRepository
{
}

