using AutoMapper;
using NUnit.Framework;
using XeroApiDemo.Application.Common;

namespace XeroApiDemo.Application.Tests;

public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;
        
    public MappingTests()
    {
        _configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = _configuration.CreateMapper();
    }

    [Test]
    public void Should_Have_Valid_Configuration() => 
        _configuration.AssertConfigurationIsValid();
}
