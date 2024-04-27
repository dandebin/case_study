using System;
namespace ReconcileService.Extensions
{

    /// <summary>
    /// This static class provides an extension method for string to parse pagination information.
    /// </summary>
	public static class PaginationExtension
	{
        /// <summary>
        /// Parses a pagination range string (format: "startIndex,count") into a tuple containing offset and size.
        /// Defaults offset to 0 and size to 10 if parsing fails.
        /// </summary>
        /// <param name="range">The pagination range string (format: "startIndex,count").</param>
        /// <returns>A ValueTuple containing offset (starting index) and size (number of items).</returns>
        public static ValueTuple<int, int> ToOffsetAndSize(this string range)
        {
            int offset = 0, size = 10;
            var array = range.Replace("[", "").Replace("]", "").Split(",");
            if (array?.Length > 1)
            {
                int.TryParse(array[0], out offset);
                if (int.TryParse(array[1], out size)){ size += 1; };
            }
            return new ValueTuple<int, int>(offset, size);
        }
	}
}

