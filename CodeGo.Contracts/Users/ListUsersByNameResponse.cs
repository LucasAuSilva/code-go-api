
using System.Net;

namespace CodeGo.Contracts.Users;

public record ListUsersByNameResponse(
    string Id,
    string FirstName,
    string LastName,
    int Visibility,
    string ProfilePicture);
