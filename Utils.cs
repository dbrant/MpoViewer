using System.Text;

namespace MpoViewer
{
    public static class Utils
    {
        
        /// <summary>
        /// Search an array of bytes for a byte pattern specified in another array.
        /// </summary>
        /// <param name="bytesToSearch">Array of bytes to search</param>
        /// <param name="matchBytes">Byte pattern to search for</param>
        /// <param name="startIndex">Starting index within the first array to start searching</param>
        /// <param name="count">Number of bytes in the first array to search</param>
        /// <returns>Zero-based index of the beginning of the byte pattern found in 
        /// the byte array, or -1 if not found.</returns>
        public static int SearchBytes(byte[] bytesToSearch, byte[] matchBytes, int startIndex, int count)
        {
            int ret = -1, max = count - matchBytes.Length + 1;
            bool found;
            for (int i = startIndex; i < max; i++)
            {
                found = true;
                for (int j = 0; j < matchBytes.Length; j++)
                {
                    if (bytesToSearch[i + j] != matchBytes[j]) { found = false; break; }
                }
                if (found) { ret = i; break; }
            }
            return ret;
        }
        public static int SearchBytes(byte[] bytesToSearch, string matchStr, int startIndex, int count)
        {
            byte[] matchBytes = Encoding.ASCII.GetBytes(matchStr);
            return SearchBytes(bytesToSearch, matchBytes, startIndex, count);
        }

        /// <summary>
        /// Compare two arrays of bytes.
        /// </summary>
        /// <param name="array1">First array to compare.</param>
        /// <param name="start1">Starting index in the first array to begin comparing.</param>
        /// <param name="array2">Second array to compare.</param>
        /// <param name="start2">Starting index in the second array to begin comparing.</param>
        /// <param name="count">Number of bytes to compare.</param>
        /// <returns>True if the bytes are identical, false otherwise.</returns>
        public static bool MemCmp(byte[] array1, int start1, byte[] array2, int start2, int count)
        {
            bool ret = true;
            for (int i = 0; i < count; i++)
            {
                if (array1[start1] != array2[start2]) { ret = false; break; }
                start1++; start2++;
            }
            return ret;
        }

    }
}
