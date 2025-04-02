using System.Security.Cryptography;
using System.Text;

namespace DamLoad.Core;

public static class Common
{
    private const string Base32Chars = "0123456789ABCDEFGHJKMNPQRSTVWXYZ"; // Crockford Base32

    public static string GeneratePublicId()
    {
        // Combine Guid and timestamp
        var guidBytes = Guid.NewGuid().ToByteArray();
        var timeBytes = BitConverter.GetBytes(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

        var combined = new byte[guidBytes.Length + timeBytes.Length];
        Buffer.BlockCopy(guidBytes, 0, combined, 0, guidBytes.Length);
        Buffer.BlockCopy(timeBytes, 0, combined, guidBytes.Length, timeBytes.Length);

        // Hash with SHA-256
        using var sha256 = SHA256.Create();
        var hash = sha256.ComputeHash(combined);

        // Take first N bytes (e.g., 10 = ~16 chars in base32)
        var idBytes = hash.Take(10).ToArray();
        return ToBase32(idBytes);
    }

    private static string ToBase32(byte[] data)
    {
        var builder = new StringBuilder();
        int buffer = 0, bitsLeft = 0;

        foreach (var b in data)
        {
            buffer = (buffer << 8) | b;
            bitsLeft += 8;

            while (bitsLeft >= 5)
            {
                bitsLeft -= 5;
                int index = (buffer >> bitsLeft) & 31;
                builder.Append(Base32Chars[index]);
            }
        }

        if (bitsLeft > 0)
        {
            int index = (buffer << (5 - bitsLeft)) & 31;
            builder.Append(Base32Chars[index]);
        }

        return builder.ToString();
    }
}