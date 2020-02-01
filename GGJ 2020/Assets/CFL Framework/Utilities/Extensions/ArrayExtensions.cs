using System.Collections.Generic;
using System.Linq;

namespace Utilities.Extensions
{
    public static class ArrayExtensions
    {
        #region BEHAVIORS

        public static bool IsEqual<T>(this IEnumerable<T> collection, IEnumerable<T> otherCollection)
        {
            return Enumerable.SequenceEqual(collection, otherCollection);
        }

        #endregion
    }
}
