using System;
using System.Collections.Generic;
using CoinApi.Domain.Common.Exceptions;
using FluentAssertions;
using FluentValidation.Results;
using Xunit;

namespace CoinApi.UnitTests.Tests
{
    public class ExceptionTests
    {
        [Fact]
        public void ValidationException_ShouldBeEmpty()
        {
            var actual = new ValidationException().Errors;

            actual.Keys.Should().BeEquivalentTo(Array.Empty<string>());
        }

        [Fact]
        public void SingleValidationException_ShouldHaveValidationResults()
        {
            var failures = new List<ValidationFailure>
            {
                new("Name", "Name is required")
            };

            var actual = new ValidationException(failures).Errors;

            actual.Keys.Should().BeEquivalentTo("Name");
            actual["Name"].Should().BeEquivalentTo("Name is required");
        }
    }
}