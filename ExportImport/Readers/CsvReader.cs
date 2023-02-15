namespace Skyline.DataMiner.Utils.ExportImport.Readers
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Reflection;
	using System.Text;

	using Skyline.DataMiner.Utils.ExportImport.Attributes;
	using Skyline.DataMiner.Utils.ExportImport.Exceptions;

	/// <summary>
	/// This class will read out a CSV file and convert it into a list of the specified type of row.
	/// </summary>
	/// <typeparam name="T">Type of the data row.</typeparam>
	public class CsvReader<T> : Reader<T> where T : class, new()
	{
		private static char? separator;

		/// <summary>
		/// Initializes a new instance of the <see cref="CsvReader{T}"></see> class.
		/// </summary>
		public CsvReader(string fullPath) : base(fullPath)
		{
		}

		/// <inheritdoc cref="Reader{T}.Read"/>
		/// <exception cref="MissingCsvHeaderAttributeWithPositionException">Property doesn't have a CsvHeaderAttribute with position.</exception>
		/// <exception cref="InvalidColumnCountException">Invalid Column Count for a specific row.</exception>
		/// <exception cref="DuplicateHeaderCsvHeaderException">Duplicate header in specified class.</exception>
		/// <exception cref="DuplicatePositionCsvHeaderException">Duplicate position in specified class.</exception>
		/// <exception cref="InvalidDataException">Couldn't parse/convert certain values inside the file.</exception>
		public override List<T> Read()
		{
			return HasPositionHeaders() ? ReadViaPositions() : ReadViaHeaders();
		}

		private List<T> ReadViaPositions()
		{
			string[] lines = GetFileData().ToArray();

			// <Position, Setter>
			Dictionary<ushort, PropertyInfo> props = GetPositionProperties();

			List<T> data = new List<T>();
			for (int i = 0; i < lines.Length; i++)
			{
				string line = lines[i];

				if (String.IsNullOrWhiteSpace(line))
				{
					continue;
				}

				string[] columns = SplitCsvRow(line);

				if (columns.Length != props.Count)
				{
					throw InvalidColumnCountException.From(columns.Length, i, props.Count);
				}

				T dto = ParseRowBasedOnName(columns, props, i);

				data.Add(dto);
			}

			return data;
		}

		private List<T> ReadViaHeaders()
		{
			string[] lines = GetFileData().ToArray();

			if (lines.Length <= 1)
			{
				return new List<T>();
			}

			string headerLine = lines[0];

			// <Position, HeaderName>
			Dictionary<int, string> positions = GetPositions(headerLine);

			// <HeaderName, Setter>
			Dictionary<string, PropertyInfo> props = GetHeaderProperties();

			List<T> data = new List<T>();
			for (int i = 1; i < lines.Length; i++)
			{
				string line = lines[i];

				if (String.IsNullOrWhiteSpace(line))
				{
					continue;
				}

				string[] columns = SplitCsvRow(line);

				if (columns.Length != positions.Count)
				{
					throw InvalidColumnCountException.From(columns.Length, i, positions.Count);
				}

				T dto = ParseRowBasedOnPosition(columns, positions, props, i);

				data.Add(dto);
			}

			return data;
		}
		private static T ParseRowBasedOnPosition(IReadOnlyList<string> columns, IReadOnlyDictionary<int, string> positions, IReadOnlyDictionary<string, PropertyInfo> props, int rowNumber)
		{
			T dto = new T();
			for (int pos = 0; pos < columns.Count; pos++)
			{
				// Get HeaderName
				if (!positions.TryGetValue(pos, out string name) || !props.TryGetValue(name, out PropertyInfo setter))
				{
					continue;
				}

				try
				{
					ParseCell(columns, setter, pos, dto);
				}
				catch (Exception ex)
				{
					if (ex is InvalidCastException || ex is FormatException || ex is OverflowException)
					{
						throw new InvalidDataException($"Failed to convert the value for header '{name}' on row {rowNumber}", ex);
					}

					throw;
				}
			}

			return dto;
		}

		private static T ParseRowBasedOnName(IReadOnlyList<string> columns, IReadOnlyDictionary<ushort, PropertyInfo> headers, int i)
		{
			T dto = new T();
			for (ushort pos = 0; pos < columns.Count; pos++)
			{
				// Get HeaderName
				if (!headers.TryGetValue(pos, out PropertyInfo setter))
				{
					continue;
				}

				try
				{
					ParseCell(columns, setter, pos, dto);
				}
				catch (Exception ex)
				{
					if (ex is InvalidCastException || ex is FormatException || ex is OverflowException)
					{
						throw new InvalidDataException($"Failed to convert the value for header on position {pos} on row {i}", ex);
					}

					throw;
				}
			}

			return dto;
		}

		private static void ParseCell(IReadOnlyList<string> columns, PropertyInfo setter, int position, T dto)
		{
			Type t = Nullable.GetUnderlyingType(setter.PropertyType) ?? setter.PropertyType;

			object value;
			if (String.IsNullOrEmpty(columns[position]))
			{
				value = null;
			}
			else
			{
				if (setter.PropertyType.IsEnum)
				{
					value = Enum.Parse(setter.PropertyType, columns[position]);
				}
				else
				{
					value = Convert.ChangeType(columns[position], t);
				}
			}

			setter.SetMethod.Invoke(dto, new object[] { value });
		}

		private static bool HasPositionHeaders()
		{
			var tempClass = typeof(T);
			var tempProps = tempClass.GetProperties();

			foreach (var tempProp in tempProps)
			{
				var attr = tempProp.GetCustomAttribute<CsvHeaderAttribute>();

				if (attr == null)
				{
					continue;
				}

				if (attr.Position != UInt16.MaxValue)
				{
					return true;
				}
			}

			return false;
		}

		private static Dictionary<string, PropertyInfo> GetHeaderProperties()
		{
			var tempClass = typeof(T);
			var tempProps = tempClass.GetProperties();

			Dictionary<string, PropertyInfo> props = new Dictionary<string, PropertyInfo>();
			foreach (var tempProp in tempProps)
			{
				var attr = tempProp.GetCustomAttribute<CsvHeaderAttribute>();
				var ignore = tempProp.GetCustomAttribute<CsvIgnoreAttribute>();

				if (ignore != null)
				{
					continue;
				}

				var name = attr == null ? tempProp.Name : attr.Header;

				if (name == null)
				{
					continue;
				}

				if (props.ContainsKey(name))
				{
					throw DuplicateHeaderCsvHeaderException.From(attr, tempClass);
				}

				props.Add(name, tempProp);
			}

			return props;
		}

		private static Dictionary<ushort, PropertyInfo> GetPositionProperties()
		{
			var tempClass = typeof(T);
			var tempProps = tempClass.GetProperties();

			Dictionary<ushort, PropertyInfo> props = new Dictionary<ushort, PropertyInfo>();
			foreach (var tempProp in tempProps)
			{
				var attr = tempProp.GetCustomAttribute<CsvHeaderAttribute>();
				var ignore = tempProp.GetCustomAttribute<CsvIgnoreAttribute>();

				if (ignore != null)
				{
					continue;
				}

				if (attr == null || attr.Position == UInt16.MaxValue)
				{
					// Missing CsvHeaderAttribute
					// Invalid CsvHeaderAttribute defined
					throw MissingCsvHeaderAttributeWithPositionException.FromPropertyInfo(tempProp);
				}

				if (props.ContainsKey(attr.Position))
				{
					throw DuplicatePositionCsvHeaderException.From(attr, tempClass);
				}

				props.Add(attr.Position, tempProp);
			}

			return props;
		}

		private static Dictionary<int, string> GetPositions(string headerLine)
		{
			string[] temp = SplitCsvRow(headerLine);

			Dictionary<int, string> positions = new Dictionary<int, string>();

			for (int i = 0; i < temp.Length; i++)
			{
				positions.Add(i, temp[i]);
			}

			return positions;
		}

		private static string[] SplitCsvRow(string row)
		{
			// Detect the column separator
			GetUsedSeparator(row);
			return ParseLine(row);
		}

		private static void GetUsedSeparator(string row)
		{
			if (separator != null)
			{
				return;
			}

			if (row.Count(c => c == ';') > row.Count(c => c == ','))
			{
				separator = ';';
			}
			else
			{
				separator = ',';
			}
		}

		private static string[] ParseLine(string line)
		{
			StringBuilder temp = new StringBuilder();

			List<string> parts = new List<string>();
			bool stringSection = false;
			bool escapeQuote = false;

			bool prevCharIsQuote = false;
			bool realCharFound = false;
			bool ignoreExtraQuote = false;
			int pos = 0;
			foreach (char c in line)
			{
				switch (c)
				{
					case '"':
						if (pos + 1 < line.Length && line[pos + 1] == '"')
						{
							// See this char as a real char

							// "" is ", """" is ""
							if (!ignoreExtraQuote)
							{
								// Check for ,"",  or "", or ,""
								bool emptyString = false;
								if (pos + 2 < line.Length)
								{
									if (line[pos + 2] == separator && (pos - 1 <= 0 || line[pos - 1] == separator))
									{
										// We have ,"",  or (start) "",
										// Empty
										emptyString = true;
									}
								}
								else
								{
									// ,"" (end)
									emptyString = true;
								}

								if (!emptyString)
								{
									temp.Append(c);
								}

								ignoreExtraQuote = true;
							}
							else
							{
								ignoreExtraQuote = false;
							}

							escapeQuote = false;
							prevCharIsQuote = false;
							realCharFound = true;
						}

						if (escapeQuote && realCharFound)
						{
							temp.Append(c);
						}
						else
						{
							stringSection = !stringSection;
						}

						escapeQuote = !escapeQuote;

						prevCharIsQuote = true;
						realCharFound = false;
						break;

					default:
						if (c == separator && !stringSection)
						{
							parts.Add(temp.ToString());
							temp.Clear();
							if (prevCharIsQuote)
							{
								escapeQuote = false;
							}

							break;
						}

						temp.Append(c);

						escapeQuote = false;
						prevCharIsQuote = false;
						realCharFound = true;

						ignoreExtraQuote = false;
						break;
				}

				pos++;
			}

			parts.Add(temp.ToString());

			return parts.ToArray();
		}
	}
}