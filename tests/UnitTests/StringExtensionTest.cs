using Xunit;
using Kit.DotNet.Core.Utils.Extensions.System;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    public class StringExtensionTest
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

        [Fact]
        public void when_string_contains_another_string()
        {
            "tEst".Contains("est", true).Should().Be(true);

            "tEst".Contains("Est", true).Should().Be(true);

            "test".Contains("Est", true).Should().Be(true);

            "tEst".Contains("est", false).Should().Be(false);

            "tEst".Contains("Est", false).Should().Be(true);

            "test".Contains("Est", false).Should().Be(false);

            "test".Contains("est", false).Should().Be(true);
        }

        [Theory]
        [InlineData("test", "", true)]
        [InlineData("", "", true)]
        [InlineData(null, "test", true)]
        [InlineData(null, "", true)]
        [InlineData("", null, true)]
        [InlineData("test", null, true)]
        [InlineData(" ", "", true)]
        [InlineData(" ", "test", true)]
        [InlineData(" ", " ", true)]
        [InlineData(" ", null, true)]
        [InlineData("", " ", true)]
        [InlineData("test", "  ", true)]
        [InlineData("  ", "test", true)]
        [InlineData(null, "  ", true)]
        [InlineData("  ", "  ", true)]
        [InlineData("  ", null, true)]
        [InlineData("test", "", false)]
        [InlineData("", "", false)]
        [InlineData(null, "test", false)]
        [InlineData(null, "", false)]
        [InlineData("", null, false)]
        [InlineData("test", null, false)]
        [InlineData(" ", "", false)]
        [InlineData(" ", "test", false)]
        [InlineData(" ", " ", false)]
        [InlineData(" ", null, false)]
        [InlineData("", " ", false)]
        [InlineData("test", "  ", false)]
        [InlineData("  ", "test", false)]
        [InlineData(null, "  ", false)]
        [InlineData("  ", "  ", false)]
        [InlineData("  ", null, false)]
        public void when_string_with_null_value_or_empty_contains_another_string(string s, string search, bool ignoreCase)
        {
            Assert.Throws<InvalidOperationException>(() => s.Contains(search, ignoreCase));
        }

        [Fact]
        public void when_string_contains_normalized_anotherstring()
        {
            "t�st".ContainsNormalized("ast", true).Should().Be(true);

            "t�st".ContainsNormalized("est", true).Should().Be(true);

            "t�st".ContainsNormalized("ast", false).Should().Be(true);

            "t�st".ContainsNormalized("est", false).Should().Be(false);
        }

        [Theory]
        [InlineData("test", "", true)]
        [InlineData("", "", true)]
        [InlineData(null, "test", true)]
        [InlineData(null, "", true)]
        [InlineData("", null, true)]
        [InlineData("test", null, true)]
        [InlineData(" ", "", true)]
        [InlineData(" ", "test", true)]
        [InlineData(" ", " ", true)]
        [InlineData(" ", null, true)]
        [InlineData("", " ", true)]
        [InlineData("test", "  ", true)]
        [InlineData("  ", "test", true)]
        [InlineData(null, "  ", true)]
        [InlineData("  ", "  ", true)]
        [InlineData("  ", null, true)]
        [InlineData("test", "", false)]
        [InlineData("", "", false)]
        [InlineData(null, "test", false)]
        [InlineData(null, "", false)]
        [InlineData("", null, false)]
        [InlineData("test", null, false)]
        [InlineData(" ", "", false)]
        [InlineData(" ", "test", false)]
        [InlineData(" ", " ", false)]
        [InlineData(" ", null, false)]
        [InlineData("", " ", false)]
        [InlineData("test", "  ", false)]
        [InlineData("  ", "test", false)]
        [InlineData(null, "  ", false)]
        [InlineData("  ", "  ", false)]
        [InlineData("  ", null, false)]
        public void when_string_with_null_value_or_empty_containsNormalized_another_string(string s, string search, bool ignoreCase)
        {
            Assert.Throws<InvalidOperationException>(() => s.ContainsNormalized(search, ignoreCase));
        }

        [Fact]
        public void when_string_has_html_content_and_normalize_content()
        {
            string html = "<html><head></head><body><p>-</p>";
            html.NormalizeHtml().Should().Be("<html><head></head><body><p>&ndash;</p>");
            html = "<html><head></head><body><p>�</p>";
            html.NormalizeHtml().Should().Be("<html><head></head><body><p>&aacute;</p>");
            html = "<html><head></head><body><p>�</p>";
            html.NormalizeHtml().Should().Be("<html><head></head><body><p>&Aacute;</p>");
            html = "<html><head></head><body><p>�</p>";
            html.NormalizeHtml().Should().Be("<html><head></head><body><p>&eacute;</p>");
            html = "<html><head></head><body><p>�</p>";
            html.NormalizeHtml().Should().Be("<html><head></head><body><p>&Eacute;</p>");
            html = "<html><head></head><body><p>�</p>";
            html.NormalizeHtml().Should().Be("<html><head></head><body><p>&iacute;</p>");
            html = "<html><head></head><body><p>�</p>";
            html.NormalizeHtml().Should().Be("<html><head></head><body><p>&Iacute;</p>");
            html = "<html><head></head><body><p>�</p>";
            html.NormalizeHtml().Should().Be("<html><head></head><body><p>&oacute;</p>");
            html = "<html><head></head><body><p>�</p>";
            html.NormalizeHtml().Should().Be("<html><head></head><body><p>&Oacute;</p>");
            html = "<html><head></head><body><p>�</p>";
            html.NormalizeHtml().Should().Be("<html><head></head><body><p>&uacute;</p>");
            html = "<html><head></head><body><p>�</p>";
            html.NormalizeHtml().Should().Be("<html><head></head><body><p>&Uacute;</p>");
            html = "<html><head></head><body><p>�</p>";
            html.NormalizeHtml().Should().Be("<html><head></head><body><p>&ntilde;</p>");
            html = "<html><head></head><body><p>�</p>";
            html.NormalizeHtml().Should().Be("<html><head></head><body><p>&Ntilde;</p>");
            html = "<html><head></head><body><p>�</p>\r\n";
            html.NormalizeHtml().Should().Be("<html><head></head><body><p>&Ntilde;</p><br/>");
            html = "<html><head></head><body><p>�</p>\n";
            html.NormalizeHtml().Should().Be("<html><head></head><body><p>&Ntilde;</p><br/>");
        }

        [Fact]
        public void when_string_has_html_content_and_normalize_content_with_useNonBreakingSpaces_to_true()
        {
            string html = "<html><head></head><body><p> </p>";
            html.NormalizeHtml(true).Should().Be("<html><head></head><body><p>&nbsp;</p>");
        }

        [Fact]
        public void when_string_with_null_or_empty_try_normalize_html_content()
        {
            "".NormalizeHtml(true).Should().Be("");
            " ".NormalizeHtml(true).Should().Be(" ");
            string? html = null;
            html?.NormalizeHtml(true).Should().Be(null);
        }

        [Fact]
        public void when_a_string_delete_a_char_of_content()
        {
            "test".RemoveText('t').Should().Be("es");

            "test".RemoveText('t', 'e').Should().Be("s");
        }

        [Fact]
        public void when_a_string_with_null_or_empty_value_and_try_delete_a_char_of_content()
        {
            "".RemoveText('t').Should().Be("");
            string? test = null;
            test?.RemoveText('t').Should().Be(null);
        }


        [Fact]
        public void when_a_string_delete_another_string_of_content()
        {
            "test".RemoveText("te").Should().Be("st");

            "testt".RemoveText("te", "tt").Should().Be("s");
        }

        [Fact]
        public void when_a_string_with_null_or_empty_try_delete_another_string_of_content()
        {
            "".RemoveText("te").Should().Be("");
            "".RemoveText("").Should().Be("");
            "test".RemoveText("").Should().Be("test");
            string? test = null;
            test?.RemoveText("test").Should().Be("");
        }

        [Fact]
        public void when_a_string_split_a_block_of_the_content()
        {
            List<string> item = "testAAAbbb".SplitBlock(3).ToList();
            item.First().Should().Be("tes");
            item.Last().Should().Be("b");
        }

        [Fact]
        public void when_a_string_with_value_empty_try_split_block_of_the_content()
        {
            List<string> item = "".SplitBlock(1).ToList();
            item.Any().Should().Be(false);
        }

        [Fact]
        public void when_a_string_convert_to_list()
        {
            List<char>? item = "abcd".ToList();
            item.First().Should().Be('a');
            item.Count().Should().Be(4);
        }
    }
}