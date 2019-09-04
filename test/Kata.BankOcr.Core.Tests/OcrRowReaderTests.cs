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
            Assert.Equal(3, glyph.Characters.GetLength(0));
            Assert.Equal(3, glyph.Characters.GetLength(1));
            Assert.Equal('1', glyph.Characters[0, 0]);
            Assert.Equal('2', glyph.Characters[0, 1]);
            Assert.Equal('3', glyph.Characters[0, 2]);
            Assert.Equal('4', glyph.Characters[1, 0]);
            Assert.Equal('5', glyph.Characters[1, 1]);
            Assert.Equal('6', glyph.Characters[1, 2]);
            Assert.Equal('7', glyph.Characters[2, 0]);
            Assert.Equal('8', glyph.Characters[2, 1]);
            Assert.Equal('9', glyph.Characters[2, 2]);
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
            Assert.Equal('1', first.Characters[0, 0]);
            Assert.Equal('2', first.Characters[0, 1]);
            Assert.Equal('3', first.Characters[0, 2]);
            Assert.Equal('7', first.Characters[1, 0]);
            Assert.Equal('8', first.Characters[1, 1]);
            Assert.Equal('9', first.Characters[1, 2]);
            Assert.Equal('a', first.Characters[2, 0]);
            Assert.Equal('b', first.Characters[2, 1]);
            Assert.Equal('c', first.Characters[2, 2]);

            var second = row[1];
            Assert.Equal('4', second.Characters[0, 0]);
            Assert.Equal('5', second.Characters[0, 1]);
            Assert.Equal('6', second.Characters[0, 2]);
            Assert.Equal('0', second.Characters[1, 0]);
            Assert.Equal('1', second.Characters[1, 1]);
            Assert.Equal('2', second.Characters[1, 2]);
            Assert.Equal('d', second.Characters[2, 0]);
            Assert.Equal('e', second.Characters[2, 1]);
            Assert.Equal('f', second.Characters[2, 2]);
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
            Assert.Equal('1', first.Characters[0, 0]);
            Assert.Equal('2', first.Characters[0, 1]);
            Assert.Equal('3', first.Characters[0, 2]);
            Assert.Equal('4', first.Characters[1, 0]);
            Assert.Equal('5', first.Characters[1, 1]);
            Assert.Equal('6', first.Characters[1, 2]);
            Assert.Equal('7', first.Characters[2, 0]);
            Assert.Equal('8', first.Characters[2, 1]);
            Assert.Equal('9', first.Characters[2, 2]);

            var secondRow = rows[1];
            var second = Assert.Single(secondRow);
            Assert.Equal('a', second.Characters[0, 0]);
            Assert.Equal('b', second.Characters[0, 1]);
            Assert.Equal('c', second.Characters[0, 2]);
            Assert.Equal('d', second.Characters[1, 0]);
            Assert.Equal('e', second.Characters[1, 1]);
            Assert.Equal('f', second.Characters[1, 2]);
            Assert.Equal('g', second.Characters[2, 0]);
            Assert.Equal('h', second.Characters[2, 1]);
            Assert.Equal('i', second.Characters[2, 2]);
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
