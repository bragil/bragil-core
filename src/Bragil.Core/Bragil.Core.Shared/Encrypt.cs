using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Bragil.Core
{
    /// <summary>
    /// Funções de criptografia.
    /// </summary>
    public static class Encrypt
    {
        private static string SALT = "8$94@5TM*C498(TUYNH!GWTFH";

        /// <summary>
        /// Gera o hash SHA256 do texto passado
        /// </summary>
        /// <param name="text">Texto a ser encriptado</param>
        /// <param name="useSalt">Flag que indica se deve usar salt (default: true)</param>
        /// <returns>Hash SHA256 do texto</returns>
        public static string WithSHA256(string text, bool useSalt = true)
        {
            using var sha1 = SHA256.Create();
            text = useSalt ? text + SALT : text;
            return sha1.ComputeHash(Encoding.UTF8.GetBytes(text))
                        .Select(b => b.ToString("x2"))
                        .Aggregate((acc, c) => acc + c);
        }

        /// <summary>
        /// Gera o hash SHA1 do texto passado
        /// </summary>
        /// <param name="text">Texto a ser encriptado</param>
        /// <param name="useSalt">Flag que indica se deve usar salt (default: true)</param>
        /// <returns>Hash SHA1 do texto</returns>
        public static string WithSHA1(string text, bool useSalt = true)
        {
            using var sha1 = SHA1.Create();
            text = useSalt ? text + SALT : text;
            return sha1.ComputeHash(Encoding.UTF8.GetBytes(text))
                        .Select(b => b.ToString("x2"))
                        .Aggregate((acc, c) => acc + c);
        }

        /// <summary>
        /// Gera o hash MD5 do texto passado
        /// </summary>
        /// <param name="text">Texto a ser encriptado</param>
        /// <param name="useSalt">Flag que indica se deve usar salt (default: true)</param>
        /// <returns>Hash MD5 do texto</returns>
        public static string WithMD5(string text, bool useSalt = true)
        {
            using var md5 = MD5.Create();
            text = useSalt ? text + SALT : text;
            return md5.ComputeHash(Encoding.UTF8.GetBytes(text))
                      .Select(b => b.ToString("x2"))
                      .Aggregate((acc, c) => acc + c);
        }
    }
}
