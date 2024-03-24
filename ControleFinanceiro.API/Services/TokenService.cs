using ControleFinanceiro.Dominio.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;


namespace ControleFinanceiro.API.Services
{
    // Gera o token
    public class TokenService
    {

        public static object GenerateToken(Usuarios usuario, string jwtKey)
        {

            // pegando minha chave
            var key = Encoding.ASCII.GetBytes(jwtKey);
            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    // definindo o payload do meu token 
                    // salvando o id do usuario no token
                      new Claim("usuarioID", usuario.UsuarioId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);
            var tokenString = tokenHandler.WriteToken(token);

            return new
            {
                token = tokenString
            };
        }
    }
}
