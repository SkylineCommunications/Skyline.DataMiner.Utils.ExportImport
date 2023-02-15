namespace Skyline.DataMiner.Utils.ExportImport.Readers
{
	using System.Collections.Generic;
	using System.IO;
	using System.Text;

	/// <summary>
	/// This class will read out a file and convert it into a list of the specified type of row.
	/// </summary>
	/// <typeparam name="T">Type of the data row.</typeparam>
	public abstract class Reader<T> where T : class, new()
	{
		private readonly string fullPath;

		private protected Reader(string fullPath)
		{
			this.fullPath = fullPath;
		}

		/// <summary>
		/// Reads this file.
		/// </summary>
		/// <returns>List of rows.</returns>
		public abstract List<T> Read();

		private protected IEnumerable<string> GetFileData()
		{
			if (!File.Exists(fullPath))
			{
				yield break;
			}

			// Make sure it can be opened even if its used by another process
			// Read the file with encoding => used for chars like 'é'
			using (StreamReader sr = new StreamReader(new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), Encoding.Default))
			{
				while (!sr.EndOfStream)
				{
					yield return sr.ReadLine();
				}
			}
		}
	}
}