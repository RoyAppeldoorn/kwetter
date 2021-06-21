using Kwetter.Services.KweetService.API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Kwetter.Services.KweetService.Tests.Domain
{
    public class KweetTest
    {
        [Fact]
        public void Should_Create_Kweet()
        {
            // Arrange
            Guid kweetId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            string message = "test message";

            // Act
            Kweet kweet = new(kweetId, userId, message);

            // Assert
            Assert.NotNull(kweet);
        }
    }
}
