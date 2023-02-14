namespace ExportImportTests.Readers
{
	using System.Collections.Generic;

	using FluentAssertions;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using Skyline.DataMiner.Utils.ExportImport.Attributes;
	using Skyline.DataMiner.Utils.ExportImport.Exceptions;
	using Skyline.DataMiner.Utils.ExportImport.Readers;

	[TestClass]
	public class CsvReaderTests
	{
		/*
		 * Make sure that the 'Copy to Output Directory' for the test files is set to 'Copy always'.
		 */

		[TestMethod]
		public void ReadTest_Headers()
		{
			List<HeaderRow> expectedRows = new List<HeaderRow>
			{
				new HeaderRow { Column1 = "Test Value 1", Column2 = "TestValue2", Column3 = "{ \"name\" = \"Foo\" }" },
				new HeaderRow { Column1 = "Test Value 1.1", Column2 = "TestValue2.1", Column3 = "{ \"name\" = \"Bar\" }" },
			};

			var reader = new CsvReader<HeaderRow>(@".\TestFiles\HeaderTestFile.txt");

			List<HeaderRow> rows = reader.Read();

			rows.Should().BeEquivalentTo(expectedRows);
		}

		[TestMethod]
		public void ReadTest_Headers_IgnoredColumn2()
		{
			List<HeaderIgnoredRow> expectedRows = new List<HeaderIgnoredRow>
			{
				new HeaderIgnoredRow { Column1 = "Test Value 1", Column2 = null, Column3 = "{ \"name\" = \"Foo\" }" },
				new HeaderIgnoredRow { Column1 = "Test Value 1.1", Column2 = null, Column3 = "{ \"name\" = \"Bar\" }" },
			};

			var reader = new CsvReader<HeaderIgnoredRow>(@".\TestFiles\HeaderTestFile.txt");

			List<HeaderIgnoredRow> rows = reader.Read();

			rows.Should().BeEquivalentTo(expectedRows);
		}

		[TestMethod]
		public void ReadTest_Headers_MissingAttributeColumn2()
		{
			List<HeaderMissingAttributeRow> expectedRows = new List<HeaderMissingAttributeRow>
			{
				new HeaderMissingAttributeRow { Column1 = "Test Value 1", Column2 = "TestValue2", Column3 = "{ \"name\" = \"Foo\" }" },
				new HeaderMissingAttributeRow { Column1 = "Test Value 1.1", Column2 = "TestValue2.1", Column3 = "{ \"name\" = \"Bar\" }" },
			};

			var reader = new CsvReader<HeaderMissingAttributeRow>(@".\TestFiles\HeaderTestFile2.txt");

			List<HeaderMissingAttributeRow> rows = reader.Read();

			rows.Should().BeEquivalentTo(expectedRows);
		}

		[TestMethod]
		public void ReadTest_Positions()
		{
			List<PositionRow> expectedRows = new List<PositionRow>
			{
				new PositionRow { Column1 = "Test Value 1", Column2 = "TestValue2", Column3 = "{ \"name\" = \"Foo\" }" },
				new PositionRow { Column1 = "Test Value 1.1", Column2 = "TestValue2.1", Column3 = "{ \"name\" = \"Bar\" }" },
			};

			var reader = new CsvReader<PositionRow>(@".\TestFiles\PositionTestFile.txt");

			List<PositionRow> rows = reader.Read();

			rows.Should().BeEquivalentTo(expectedRows);
		}

		[TestMethod]
		[ExpectedException(typeof(MissingCsvHeaderAttributeWithPositionException))]
		public void ReadTest_Positions_MissingAttributes()
		{
			var reader = new CsvReader<PositionMissingRow>(@".\TestFiles\PositionTestFile.txt");

			reader.Read();
		}

		public class HeaderRow
		{
			[CsvHeader("Column 1")]
			public string Column1 { get; set; }

			[CsvHeader("Column 2")]
			public string Column2 { get; set; }

			[CsvHeader("Column 3")]
			public string Column3 { get; set; }
		}

		public class HeaderIgnoredRow
		{
			[CsvHeader("Column 1")]
			public string Column1 { get; set; }

			[CsvIgnore]
			public string Column2 { get; set; }

			[CsvHeader("Column 3")]
			public string Column3 { get; set; }
		}

		public class HeaderMissingAttributeRow
		{
			[CsvHeader("Column 1")]
			public string Column1 { get; set; }

			public string Column2 { get; set; }

			[CsvHeader("Column 3")]
			public string Column3 { get; set; }
		}

		public class PositionRow
		{
			[CsvHeader(0)]
			public string Column1 { get; set; }

			[CsvHeader(1)]
			public string Column2 { get; set; }

			[CsvHeader(2)]
			public string Column3 { get; set; }
		}

		public class PositionMissingRow
		{
			[CsvHeader(0)]
			public string Column1 { get; set; }

			public string Column2 { get; set; }

			[CsvHeader(2)]
			public string Column3 { get; set; }
		}
	}
}