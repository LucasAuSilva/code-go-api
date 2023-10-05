
using System.Net;

namespace CodeGo.Contracts.Users;

public record ListUsersByNameResponse(
    string FirstName,
    string LastName,
    string ProfilePicture);
