namespace ExportImportTests.Factories
{
	using System;
	using System.IO;

	using FluentAssertions;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using Skyline.DataMiner.Utils.ExportImport.Factories;
	using Skyline.DataMiner.Utils.ExportImport.Readers;

	[TestClass]
	public class ReaderFactoryTests
	{
		[TestMethod]
		public void GetReader_NullFilePath()
		{
			// Act
			Action act = () => ReaderFactory.GetReader<DataRow>(null);

			// Assert
			act.Should().ThrowExactly<ArgumentNullException>();
		}

		[TestMethod]
		public void GetReader_EmptyOrWhitespaceFilePath()
		{
			// Act
			Action act = () => ReaderFactory.GetReader<DataRow>("  ");

			// Assert
			act.Should().ThrowExactly<ArgumentException>();
		}

		[TestMethod]
		public void GetReader_UnknownFile()
		{
			// Arrange
			const string filePath = "C:\\test.xml";

			// Act
			Action act = () => ReaderFactory.GetReader<DataRow>(filePath);

			// Assert
			act.Should().ThrowExactly<FileNotFoundException>();
		}

		[TestMethod]
		[DeploymentItem(@"TestFiles\EmptyXml.xml")]
		[DeploymentItem(@"TestFiles\EmptyJson.json")]
		[DeploymentItem(@"TestFiles\HeaderTestFile.txt")]
		[DataRow("EmptyXml.xml", typeof(XmlReader<DataRow>))]
		[DataRow("EmptyJson.json", typeof(JsonReader<DataRow>))]
		[DataRow("HeaderTestFile.txt", typeof(CsvReader<DataRow>))]
		public void GetReader_ReaderType(string filePath, Type expectedType)
		{
			// Act
			var reader = ReaderFactory.GetReader<DataRow>(filePath);

			// Assert
			reader.Should().BeOfType(expectedType);
		}

		private class DataRow
		{
		}
	}
}