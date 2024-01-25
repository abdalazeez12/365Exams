using System;
using System.Text;

namespace Imtihani.Helpers
{

    public static class EncryptionHelper
    {
        private static readonly byte[] Key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };

        public static string EncryptGuid(Guid guid)
        {
            byte[] guidBytes = guid.ToByteArray();
            byte[] encryptedBytes = new byte[guidBytes.Length];

            for (int i = 0; i < guidBytes.Length; i++)
            {
                encryptedBytes[i] = (byte)(guidBytes[i] ^ Key[i % Key.Length]);
            }

            return Convert.ToBase64String(encryptedBytes);
        }

        public static Guid DecryptGuid(string encryptedGuid)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedGuid);
            byte[] decryptedBytes = new byte[encryptedBytes.Length];

            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                decryptedBytes[i] = (byte)(encryptedBytes[i] ^ Key[i % Key.Length]);
            }

            return new Guid(decryptedBytes);
        }
    }

}
