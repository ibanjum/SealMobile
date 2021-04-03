using System.IO;
using System.Xml;

namespace Seal.Utilities
{
    public static class XmlUtility
    {
        public static byte[] GetFileAsBytes(FileStream input)
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

        public static XmlDocument GetFileAsXML(byte[] sceneBytes)
        {
            XmlDocument doc = new XmlDocument();
            using (MemoryStream ms = new MemoryStream(sceneBytes))
            {
                doc.Load(ms);
            }
            return doc;
        }
    }
}
