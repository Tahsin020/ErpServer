using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpServer.Domain.Dtos;

public sealed record InvoiceDetailDto(
Guid ProductId,
Guid DepotId,
decimal Quantity,
decimal Price);



