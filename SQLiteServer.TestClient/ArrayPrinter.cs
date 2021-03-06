using System;

// Abgeleitet von: http://stackoverflow.com/questions/856845/how-to-best-way-to-draw-table-in-console-app-c
// Original Autor: Patrick McDonald

namespace TestClient
{
	class ArrayPrinter
	{
		static bool isLeftAligned = false;
		const string cellLeftTop = "┌";
		const string cellRightTop = "┐";
		const string cellLeftBottom = "└";
		const string cellRightBottom = "┘";
		// const string cellHorizontalJointTop = "┬";
		// const string cellHorizontalJointbottom = "┴";
		const string cellVerticalJointLeft = "├";
		const string cellTJoint = "┼";
		const string cellVerticalJointRight = "┤";
		const string cellHorizontalLine = "─";
		const string cellVerticalLine = "│";

		public static string GetDataInTableFormat (string[] arrFieldNames, string[,] arrValues)
		{
			string formattedString = string.Empty;
			
			// Exit cases
			if (arrValues == null) return formattedString;
			if (arrFieldNames == null) return formattedString;
			
			// Dimensions
			int dimension1Length = arrValues.GetLength (0); // Rows
			int dimension2Length = arrValues.GetLength (1); // Columns
			
			// Max width of Columns
			int maxValueWidth = GetMaxCellWidth (arrValues);
			int maxFieldWidth = GetMaxFieldWidth (arrFieldNames);
			int maxCellWidth = maxValueWidth > maxFieldWidth ? maxValueWidth : maxFieldWidth; 
			
			// Width of a complete Line
			int indentLength = (dimension2Length * maxCellWidth) + (dimension2Length - 1);

			// ---------------------------------------------------------------------------------
			// Fields
			// ---------------------------------------------------------------------------------

			// Top Line
			formattedString = string.Format ("{0}{1}{2}{3}", cellLeftTop, Indent (indentLength, '─'), cellRightTop, System.Environment.NewLine);
			
			// Field Names
			string lineWithValues = cellVerticalLine;
			for (int i = 0; i < dimension2Length; i++) {
				string value = (isLeftAligned) ? arrFieldNames [i].PadRight (maxCellWidth, ' ') : arrFieldNames [i].PadLeft (maxCellWidth, ' ');
				lineWithValues += string.Format ("{0}{1}", value, cellVerticalLine);
			}
			formattedString += string.Format("{0}{1}", lineWithValues, System.Environment.NewLine);

			// Bottom Line
			formattedString += string.Format("{0}{1}{2}{3}", cellLeftBottom, Indent(indentLength, '─'), cellRightBottom, System.Environment.NewLine);

			// ---------------------------------------------------------------------------------
			// Rows
			// ---------------------------------------------------------------------------------

			// Top Line
			formattedString += string.Format ("{0}{1}{2}{3}", cellLeftTop, Indent (indentLength, '─'), cellRightTop, System.Environment.NewLine);

			// Rows
			string line;
			for (int i = 0; i < dimension1Length; i++)
			{
				lineWithValues = cellVerticalLine;
				line = cellVerticalJointLeft;
				for (int j = 0; j < dimension2Length; j++)
				{
					string value = (isLeftAligned) ? arrValues[i, j].PadRight(maxCellWidth, ' ') : arrValues[i, j].PadLeft(maxCellWidth, ' ');
					lineWithValues += string.Format("{0}{1}", value, cellVerticalLine);
					line += Indent(maxCellWidth, '─');
					if (j < (dimension2Length - 1))
					{
						line += cellTJoint;
					}
				}
				line += cellVerticalJointRight;
				formattedString += string.Format("{0}{1}", lineWithValues, System.Environment.NewLine);
				if (i < (dimension1Length - 1))
				{
					formattedString += string.Format("{0}{1}", line, System.Environment.NewLine);
				}
			}

			// Bottom Line
			formattedString += string.Format("{0}{1}{2}{3}", cellLeftBottom, Indent(indentLength, '─'), cellRightBottom, System.Environment.NewLine);

			return formattedString;
		}

		// Get Max Width of Columns
		private static int GetMaxCellWidth(string[,] arrValues)
		{
			int maxWidth = 1;
			
			for (int i = 0; i < arrValues.GetLength(0); i++)
			{
				for (int j = 0; j < arrValues.GetLength(1); j++)
				{
					int length = arrValues[i, j].Length;
					if (length > maxWidth)
					{
						maxWidth = length;
					}
				}
			}
			
			return maxWidth;
		}

		// Get Max Width of Field Names
		private static int GetMaxFieldWidth(string[] arrValues)
		{
			int maxWidth = 1;
			
			for (int i = 0; i < arrValues.GetLength(0); i++)
			{
				int length = arrValues[i].Length;
				if (length > maxWidth)
				{
					maxWidth = length;
				}
			}
			
			return maxWidth;
		}

		// Return a Line
		private static string Indent(int count, char chr)
		{
			return string.Empty.PadLeft(count, chr);                 
		}

	}
}