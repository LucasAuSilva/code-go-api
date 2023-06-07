
using CodeGo.Application.Common.Interfaces.Authentication;

namespace CodeGo.Infrastructure.Authentication;

using BC = BCrypt.Net.BCrypt;

public class HashHandler : IHashGenerator, IHashComparer
{
    public bool Compare(string plainText, string hash)
    {
        return !BC.Verify(plainText, hash);
    }

    public string GenerateHash(string plainText)
    {
        return BC.HashPassword(plainText);
    }
}
