using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace ControleFinanceiro.API.Services
{
    public class SegurancaServico
    {
        private const int SaltSize = 16; // Tamanho do sal em bytes
        private const int HashSize = 32; // Tamanho do hash em bytes
        private const int Iterations = 10000; // Número de iterações para o PBKDF2

        public static string HashSenha(string senha)
        {
            // Gerar um sal aleatório
            byte[] salt = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            // Calcular o hash da senha usando HMAC-SHA256 com PBKDF2
            using (var hmac = new HMACSHA256(salt))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));
                // Concatenar o sal e o hash
                byte[] hashBytes = new byte[SaltSize + HashSize];
                Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);
                // Converter bytes para string base64
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static bool VerificandoSenha(string senha, string senhaHash)
        {
            // Converter a senha hash de base64 de volta para bytes
            byte[] hashBytes = Convert.FromBase64String(senhaHash);

            // Extrair o sal dos bytes
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Calcular o hash da senha fornecida usando o mesmo sal e verificar a igualdade
            using (var hmac = new HMACSHA256(salt))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));
                // Comparar os hashes
                for (int i = 0; i < HashSize; i++)
                {
                    if (hashBytes[i + SaltSize] != hash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}