using NUnit.Framework;

namespace XeroApiDemo.Application.Tests.Common;

public abstract class TestSpecification
{
    [OneTimeSetUp]
    public async Task Init()
    {
        Given();
        await GivenAsync();
            
        When();
        await WhenAsync();
    }

    public virtual void Given() { }

    public virtual Task GivenAsync() => Task.CompletedTask;

    public virtual void When() { }

    public virtual Task WhenAsync() => Task.CompletedTask;
}
    
public class ThenAttribute : TestAttribute {}
