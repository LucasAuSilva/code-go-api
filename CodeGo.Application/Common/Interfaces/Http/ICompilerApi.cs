
using CodeGo.Domain.Common.Enums;

namespace CodeGo.Application.Common.Interfaces.Http;

public interface ICompilerApi
{
    Task<string> SendCodeToCompile(string code, Language language);
}
