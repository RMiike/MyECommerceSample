using FluentAssertions;
using MECS.Core.Domain.Entities;
using Xunit;

namespace MECS.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var audience = "Audience";
            var settings = new AppSettings();
            settings.Audience = audience;

            settings.Audience.Should().Be(audience);
        }
    }
}
