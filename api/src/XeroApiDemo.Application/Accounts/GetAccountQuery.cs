using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace XeroApiDemo.Application.Accounts;

public record GetAccountQuery : IRequest<AccountDto>
{
    public Guid AccountId { get; init; }
}

// ReSharper disable once UnusedType.Global
public class GetProfileQueryHandler : IRequestHandler<GetAccountQuery, AccountDto>
{
    private readonly XeroApiDemoContext _db;
    private readonly IMapper _mapper;

    public GetProfileQueryHandler(XeroApiDemoContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<AccountDto> Handle(GetAccountQuery request, CancellationToken cancellationToken) =>
        await _db.Accounts
            .Where(x => x.Id == request.AccountId)
            .ProjectTo<AccountDto>(_mapper.ConfigurationProvider)
            .SingleAsync(cancellationToken: cancellationToken);
}
