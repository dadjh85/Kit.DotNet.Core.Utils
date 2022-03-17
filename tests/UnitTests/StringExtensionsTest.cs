using Xunit;
using Kit.DotNet.Core.Utils.Extensions.System;
using FluentAssertions;
using System;

namespace UnitTests
{
    public class StringExtensionsTest
    {
        [Fact]
        public void when_string_convert_first_letter_toUpper()
        {
            string s = "test";
            s.Capitalize().Should().Be("Test");
        }

        [Fact]
        public void when_the_first_letter_of_string_is_already_capitalised()
        {
            string s = "Test";
            s.Capitalize().Should().Be("Test");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("  ")]
        public void when_string_is_null_or_empty_and_try_convert_first_letter_toUpper(string s)
        {
            Assert.Throws<InvalidOperationException>(() => s.Capitalize());
        }

        [Fact]
        public void when_string_convert_first_letter_toLower()
        {
            string s = "Test";
            s.Uncapitalize().Should().Be("test");
        }

        [Fact]
        public void when_the_first_letter_of_string_is_already_lower_case_letter()
        {
            string s = "test";
            s.Uncapitalize().Should().Be("test");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("  ")]
        public void when_string_is_null_or_empty_and_try_convert_first_letter_toLowerCase(string s)
        {
            Assert.Throws<InvalidOperationException>(() => s.Uncapitalize());
        }
    }
}