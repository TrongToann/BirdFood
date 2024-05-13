using BirdFood.Contract.Enumerations;
using System.Collections.Generic;

namespace BirdFood.Contract.Extensions
{
    public static class SortOrderExtension
    {
        public static SortOrder CovertFromStringToSortOrder(string? sortOrder)
            => !string.IsNullOrWhiteSpace(sortOrder)
            ? sortOrder.ToUpper().Equals("ASC")
            ? SortOrder.Ascending : SortOrder.Descending : SortOrder.Descending;
        public static Dictionary<string, SortOrder> CovertStringToSortDictionary(string? sortOrder)
        {
            var result = new Dictionary<string, SortOrder>();
            if (!string.IsNullOrEmpty(sortOrder))
            {
                if (sortOrder.Trim().Split(",").Length > 0)
                {
                    foreach (var item in sortOrder.Split(","))
                    {
                        if (!item.Contains("-"))
                            throw new FormatException("Sort Column is invalid!");
                        var property = item.Trim().Split("-");
                        var key = "name";
                        var value = CovertFromStringToSortOrder(property[1]);
                        result.TryAdd(key, value);
                    }
                }
                else
                {
                    if (!sortOrder.Contains("-"))
                        throw new FormatException("Sort Column is invalid!");
                    var property = sortOrder.Trim().Split("-");
                    result.TryAdd(property[0], property[1]);
                }
                return result;
            }
            return result;
        }
    }
}
