
namespace CodeGo.Application.Common.Interfaces.Authentication;

public interface IHashGenerator
{
    string GenerateHash(string plainText);
}
