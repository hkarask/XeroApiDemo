using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace XeroApiDemo.Application.Invoices;

public record GetInvoicesQuery : IRequest<List<InvoiceDto>>
{
    public Guid AccountId { get; init; }
}

// ReSharper disable once UnusedType.Global
public class GetProfileQueryHandler : IRequestHandler<GetInvoicesQuery, List<InvoiceDto>>
{
    private readonly IMapper _mapper;
    private readonly XeroApiDemoContext _db;

    public GetProfileQueryHandler(IMapper mapper, XeroApiDemoContext db)
    {
        _mapper = mapper;
        _db = db;
    }

    public async Task<List<InvoiceDto>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken) =>
        await _db.Invoices
            .Where(i => i.AccountId == request.AccountId)
            .OrderByDescending(i => i.DateIssued)
            .Take(10)
            .ProjectTo<InvoiceDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
}
