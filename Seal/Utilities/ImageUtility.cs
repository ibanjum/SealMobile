using System.IO;
using Xamarin.Forms;

namespace Seal.Utilities
{
    public static class ImageUtility
    {
        public static byte[] GetImageStreamAsBytes(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static ImageSource GetImageAsSource(byte[] bytes)
        {
            if (bytes == null)
                return null;

            return ImageSource.FromStream(() => new MemoryStream(bytes));
        }
    }
}
