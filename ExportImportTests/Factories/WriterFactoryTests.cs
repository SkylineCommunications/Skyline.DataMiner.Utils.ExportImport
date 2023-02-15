namespace ExportImportTests.Factories
{
	using System;

	using FluentAssertions;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using Skyline.DataMiner.Utils.ExportImport.Factories;
	using Skyline.DataMiner.Utils.ExportImport.Writers;

	[TestClass]
	public class WriterFactoryTests
	{
		[TestMethod]
		public void GetWriter_NullFilePath()
		{
			// Act
			Action act = () => WriterFactory.GetWriter<DataRow>(null);

			// Assert
			act.Should().ThrowExactly<ArgumentNullException>();
		}

		[TestMethod]
		public void GetWriter_EmptyOrWhitespaceFilePath()
		{
			// Act
			Action act = () => WriterFactory.GetWriter<DataRow>("  ");

			// Assert
			act.Should().ThrowExactly<ArgumentException>();
		}

		[TestMethod]
		[DeploymentItem(@"TestFiles\EmptyXml.xml")]
		[DeploymentItem(@"TestFiles\EmptyJson.json")]
		[DeploymentItem(@"TestFiles\HeaderTestFile.txt")]
		[DataRow("EmptyXml.xml", typeof(XmlWriter<DataRow>))]
		[DataRow("EmptyJson.json", typeof(JsonWriter<DataRow>))]
		[DataRow("HeaderTestFile.txt", typeof(CsvWriter<DataRow>))]
		public void GetWriter_WriterType(string filePath, Type expectedType)
		{
			// Act
			var writer = WriterFactory.GetWriter<DataRow>(filePath);

			// Assert
			writer.Should().BeOfType(expectedType);
		}

		private class DataRow
		{
		}
	}
}