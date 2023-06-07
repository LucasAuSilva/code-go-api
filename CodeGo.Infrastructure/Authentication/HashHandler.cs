
using CodeGo.Application.Common.Interfaces.Authentication;

namespace CodeGo.Infrastructure.Authentication;

using BC = BCrypt.Net.BCrypt;

public class HashHandler : IHashGenerator
{
    public string GenerateHash(string plainText)
    {
        return BC.HashPassword(plainText);
    }
}
