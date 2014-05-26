using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Data.Services;

namespace DataSql.NUnit
{
    [TestFixture]
    public class PaginationToolsTests
    {
        [Test]
        public void ConvertPagesToCount_InvalidStartPage()
        {
            var result = PaginationTools.ConvertPagesToCount(-1, 10);

            Assert.AreEqual(0, result.Item1);
            Assert.AreEqual(10, result.Item2);
        }

        [Test]
        public void ConvertPagesToCount_InvalidPageCount()
        {
            var result = PaginationTools.ConvertPagesToCount(1, -1);

            Assert.AreEqual(0, result.Item1);
            Assert.AreEqual(10, result.Item2);
        }

        [Test]
        public void ConvertPagesToCount_Page1Count()
        {
            var result = PaginationTools.ConvertPagesToCount(1, 10);

            Assert.AreEqual(0, result.Item1);
            Assert.AreEqual(10, result.Item2);
        }

        [Test]
        public void ConvertPagesToCount_Page2Count()
        {
            var result = PaginationTools.ConvertPagesToCount(2, 10);

            Assert.AreEqual(10, result.Item1);
            Assert.AreEqual(10, result.Item2);
        }
    }
}
