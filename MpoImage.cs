using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace MpoViewer
{
    public static class MpoImage
    {
        public static List<Image> GetMpoImages(string fileName)
        {
            var images = new List<Image>();
            byte[] tempBytes = new byte[100];

            using (var f = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                tempBytes = new byte[f.Length];
                f.Read(tempBytes, 0, (int)f.Length);
            }

            List<int> imageOffsets = new List<int>();
            int offset = 0, tempOffset = 0;
            byte[] keyBytes = { 0xFF, 0xD8, 0xFF, 0xE1 };
            byte[] keyBytes2 = { 0xFF, 0xD8, 0xFF, 0xE0 };

            while (true)
            {
                tempOffset = Utils.SearchBytes(tempBytes, keyBytes, offset, tempBytes.Length);
                if (tempOffset == -1)
                    tempOffset = Utils.SearchBytes(tempBytes, keyBytes2, offset, tempBytes.Length);
                if (tempOffset == -1) break;
                offset = tempOffset;
                imageOffsets.Add(offset);
                offset += 4;
            }

            for (int i = 0; i < imageOffsets.Count; i++)
            {
                int length;
                if (i < (imageOffsets.Count - 1))
                    length = imageOffsets[i + 1] - imageOffsets[i];
                else
                    length = tempBytes.Length - imageOffsets[i];

                MemoryStream stream = new MemoryStream(tempBytes, imageOffsets[i], length);
                images.Add(new Bitmap(stream));
            }

            return images;
        }

    }
}
