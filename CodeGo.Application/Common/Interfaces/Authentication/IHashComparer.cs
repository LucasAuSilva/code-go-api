
namespace CodeGo.Application.Common.Interfaces.Authentication;

public interface IHashComparer
{
    bool Compare(string plainText, string hash);
}
