using System.Security.Claims;
using Blog.Models;

namespace Blog.Extensions;

public static class FuncaoClaimsExtesion
{
    public static IEnumerable<Claim> GetClaims(this Usuario usuario)
    {
        var result = new List<Claim> {
            new (ClaimTypes.Name, usuario.Email),
        };

        result.AddRange(
            usuario.Funcoes.Select(funcao => new Claim(ClaimTypes.Role, funcao.NomeFuncao))
        );

        return result;
    }
}