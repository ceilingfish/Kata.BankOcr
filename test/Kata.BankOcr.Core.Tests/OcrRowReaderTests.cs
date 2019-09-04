using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Kata.BankOcr.Core.Tests
{
    public class OcrRowReaderTests
    {
        [Fact]
        public async Task CanReadASingleGlyph()
        {
            var temp = Path.GetTempFileName();
            await File.WriteAllLinesAsync(temp, new[] { "123", "456", "789", "012" });
            var reader = new OcrRowReader(temp);
            var rows = await reader.Rows().ToArray();
            var row = Assert.Single(rows);
            var glyph = Assert.Single(row);
            Assert.Equal('1', glyph[0, 0]);
            Assert.Equal('2', glyph[1, 0]);
            Assert.Equal('3', glyph[2, 0]);
            Assert.Equal('4', glyph[0, 1]);
            Assert.Equal('5', glyph[1, 1]);
            Assert.Equal('6', glyph[2, 1]);
            Assert.Equal('7', glyph[0, 2]);
            Assert.Equal('8', glyph[1, 2]);
            Assert.Equal('9', glyph[2, 2]);
        }

        [Fact]
        public async Task CanReadConsecutiveGlyphs()
        {
            var temp = Path.GetTempFileName();
            await File.WriteAllLinesAsync(temp, new[] { "123456", "789012", "abcdef", "ghijkl" });
            var reader = new OcrRowReader(temp);
            var rows = await reader.Rows().ToArray();
            var row = Assert.Single(rows);
            Assert.Equal(2, row.Count);
            var first = row[0];
            Assert.Equal('1', first[0, 0]);
            Assert.Equal('2', first[1, 0]);
            Assert.Equal('3', first[2, 0]);
            Assert.Equal('7', first[0, 1]);
            Assert.Equal('8', first[1, 1]);
            Assert.Equal('9', first[2, 1]);
            Assert.Equal('a', first[0, 2]);
            Assert.Equal('b', first[1, 2]);
            Assert.Equal('c', first[2, 2]);

            var second = row[1];
            Assert.Equal('4', second[0, 0]);
            Assert.Equal('5', second[1, 0]);
            Assert.Equal('6', second[2, 0]);
            Assert.Equal('0', second[0, 1]);
            Assert.Equal('1', second[1, 1]);
            Assert.Equal('2', second[2, 1]);
            Assert.Equal('d', second[0, 2]);
            Assert.Equal('e', second[1, 2]);
            Assert.Equal('f', second[2, 2]);
        }

        [Fact]
        public async Task CanReadRowsOfGlyphs()
        {
            var temp = Path.GetTempFileName();
            await File.WriteAllLinesAsync(temp, new[] { "123", "456", "789", "012", "abc", "def", "ghi", "jkl" });
            var reader = new OcrRowReader(temp);
            var rows = await reader.Rows().ToArray();
            Assert.Equal(2, rows.Length);
            var firstRow = rows[0];
            var first = Assert.Single(firstRow);
            Assert.Equal('1', first[0, 0]);
            Assert.Equal('2', first[1, 0]);
            Assert.Equal('3', first[2, 0]);
            Assert.Equal('4', first[0, 1]);
            Assert.Equal('5', first[1, 1]);
            Assert.Equal('6', first[2, 1]);
            Assert.Equal('7', first[0, 2]);
            Assert.Equal('8', first[1, 2]);
            Assert.Equal('9', first[2, 2]);

            var secondRow = rows[1];
            var second = Assert.Single(secondRow);
            Assert.Equal('a', second[0, 0]);
            Assert.Equal('b', second[1, 0]);
            Assert.Equal('c', second[2, 0]);
            Assert.Equal('d', second[0, 1]);
            Assert.Equal('e', second[1, 1]);
            Assert.Equal('f', second[2, 1]);
            Assert.Equal('g', second[0, 2]);
            Assert.Equal('h', second[1, 2]);
            Assert.Equal('i', second[2, 2]);
        }

        [Fact]
        public async Task ThrowsOnInvalidColumnLength()
        {
            var temp = Path.GetTempFileName();
            await File.WriteAllLinesAsync(temp, new[] { "123", "456", "789", "0" });
            var reader = new OcrRowReader(temp);

            await Assert.ThrowsAsync<InvalidDataException>(() => reader.Rows().ToTask());
        }

        [Fact]
        public async Task ThrowsOnInvalidRowLength()
        {
            var temp = Path.GetTempFileName();
            await File.WriteAllLinesAsync(temp, new[] { "123", "456", "789" });
            var reader = new OcrRowReader(temp);

            await Assert.ThrowsAsync<InvalidDataException>(() => reader.Rows().ToTask());
        }
    }
}
