using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace ControleFinanceiro.API.Services
{
    public class SegurancaServico
    {
        private const int SaltSize = 16; // Tamanho do sal em bytes
        private const int HashSize = 20; // Tamanho do hash em bytes
        private const int Iterations = 10000; // Número de iterações para o PBKDF2
        public static string HashSenha(string senha)
        {
            // Gerar um sal aleatório
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt;
                rng.GetBytes(salt = new byte[SaltSize]);

                // Hash da senha usando PBKDF2
                using (var pbkdf2 = new Rfc2898DeriveBytes(senha, salt, Iterations))
                {
                    byte[] hash = pbkdf2.GetBytes(HashSize);

                    // Concatenar o sal e o hash
                    byte[] hashBytes = new byte[SaltSize + HashSize];
                    Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                    Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

                    // Converter bytes para string base64
                    string base64Hash = Convert.ToBase64String(hashBytes);

                    // Formatar a string com o algoritmo, número de iterações e o sal
                    return string.Format("$HASH|V1${0}${1}${2}", Iterations, base64Hash.Length, base64Hash);
                }
            }
        }
    }
}
