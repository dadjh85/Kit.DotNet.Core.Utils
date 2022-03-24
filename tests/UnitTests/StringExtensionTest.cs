using Xunit;
using Kit.DotNet.Core.Utils.Extensions.System;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

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
            "tást".ContainsNormalized("ast", true).Should().Be(true);

            "tÉst".ContainsNormalized("est", true).Should().Be(true);

            "tást".ContainsNormalized("ast", false).Should().Be(true);

            "tÉst".ContainsNormalized("est", false).Should().Be(false);
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
            html = "<html><head></head><body><p>á</p>";
            html.NormalizeHtml().Should().Be("<html><head></head><body><p>&aacute;</p>");
            html = "<html><head></head><body><p>Á</p>";
            html.NormalizeHtml().Should().Be("<html><head></head><body><p>&Aacute;</p>");
            html = "<html><head></head><body><p>é</p>";
            html.NormalizeHtml().Should().Be("<html><head></head><body><p>&eacute;</p>");
            html = "<html><head></head><body><p>É</p>";
            html.NormalizeHtml().Should().Be("<html><head></head><body><p>&Eacute;</p>");
            html = "<html><head></head><body><p>í</p>";
            html.NormalizeHtml().Should().Be("<html><head></head><body><p>&iacute;</p>");
            html = "<html><head></head><body><p>Í</p>";
            html.NormalizeHtml().Should().Be("<html><head></head><body><p>&Iacute;</p>");
            html = "<html><head></head><body><p>ó</p>";
            html.NormalizeHtml().Should().Be("<html><head></head><body><p>&oacute;</p>");
            html = "<html><head></head><body><p>Ó</p>";
            html.NormalizeHtml().Should().Be("<html><head></head><body><p>&Oacute;</p>");
            html = "<html><head></head><body><p>ú</p>";
            html.NormalizeHtml().Should().Be("<html><head></head><body><p>&uacute;</p>");
            html = "<html><head></head><body><p>Ú</p>";
            html.NormalizeHtml().Should().Be("<html><head></head><body><p>&Uacute;</p>");
            html = "<html><head></head><body><p>ñ</p>";
            html.NormalizeHtml().Should().Be("<html><head></head><body><p>&ntilde;</p>");
            html = "<html><head></head><body><p>Ñ</p>";
            html.NormalizeHtml().Should().Be("<html><head></head><body><p>&Ntilde;</p>");
            html = "<html><head></head><body><p>Ñ</p>\r\n";
            html.NormalizeHtml().Should().Be("<html><head></head><body><p>&Ntilde;</p><br/>");
            html = "<html><head></head><body><p>Ñ</p>\n";
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
            string html = null;
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
            string test = null;
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
            string test = null;
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
            List<char> item = "abcd".ToList();
            item.First().Should().Be('a');
            item.Count().Should().Be(4);

            List<string> item2 = "a,b,c,d".ToList<string>();
            item2?.First().Should().Be("a");
            item2?.Count().Should().Be(4);

            List<int> item3 = "1,2,3,4".ToList<int>();
            item3?.First().Should().Be(1);
            item3?.Count().Should().Be(4);

            List<int> item4 = "1|2|3|4".ToList<int>("|");
            item4?.First().Should().Be(1);
            item4?.Count().Should().Be(4);
        }

        [Fact]
        public void when_a_string_with_null_or_empty_value_try_convert_to_list()
        {
            List<string> item = "".ToList<string>();
            item.Should().BeNull();

            string test = null;
            List<string> item2 = test.ToList<string>();
            item2.Should().BeNull();
        }

        [Fact]
        public void when_a_string_convert_to_hexadecimal_to_byte_array()
        {
            "100".HexToByteArray().ToList().First().Should().Be(16);
        }

        [Fact]
        public void when_a_string_of_empty_or_null_try_convert_to_hexadecimal_to_byte_array()
        {
            Assert.Throws<InvalidOperationException>(() => "".HexToByteArray());
            string test = null;
            Assert.Throws<InvalidOperationException>(() => test.HexToByteArray());
        }

        [Fact]
        public void when_a_string_convert_to_byte_array()
        {
            var result = "test".ToByteArray();
            result.ToList().First().Should().Be(116);
            result.ToList().Last().Should().Be(116);
            result.ToList().Count().Should().Be(4);
            var result2 = "test".ToByteArray(Encoding.ASCII);
            result2.ToList().First().Should().Be(116);
            result2.ToList().Last().Should().Be(116);
            result2.ToList().Count().Should().Be(4);
        }

        [Fact]
        public void when_a_string_empty_or_null_try_convert_to_byte_array()
        {
            Assert.Throws<InvalidOperationException>(() => "".ToByteArray());
            Assert.Throws<InvalidOperationException>(() => "".ToByteArray(Encoding.ASCII));
            string test = null;
            Assert.Throws<InvalidOperationException>(() => test.ToByteArray());
            Assert.Throws<InvalidOperationException>(() => test.ToByteArray(Encoding.ASCII));
        }

        [Fact]
        public async Task when_a_string_convert_ToStringContent()
        {
            StringContent stringToStringContent = "test".ToStringContent();
            string teststring = await stringToStringContent.ReadAsStringAsync();
            teststring.Should().Be("test");
        }

        [Fact]
        public void when_a_string_null_or_empty_try_convert_ToStringContent()
        {
            Assert.Throws<InvalidOperationException>(() => "".ToStringContent());
            string test = null;
            Assert.Throws<InvalidOperationException>(() => test.ToStringContent());
        }

        [Fact]
        public void when_a_string_in_base_64_convert_byte_array()
        {
            var array = "dGVzdA==".FromBase64ToByteArray();
            array.ToList().First().Should().Be(116);
            array.ToList().Last().Should().Be(116);
            array.ToList().Count().Should().Be(4);
        }

        [Fact]
        public void when_a_string_null_or_empty_from_base64_convert_byte_array()
        {
            "".FromBase64ToByteArray().Should().BeNull();
            string test = null;
            test.FromBase64ToByteArray().Should().BeNull();
        }

        private enum Level { Low, Medium, High }
        
        [Fact]
        public void when_a_diferents_string_convert_to_value_of_enum()
        {
            "High".ToEnum<Level>().Should().Be(Level.High);
            "".ToEnum<Level>().Should().Be(Level.Low);
            string test = null;
            test.ToEnum<Level>().Should().Be(Level.Low);
            "test".ToEnum<Level>().Should().Be(Level.Low);
        }
    
    }
}