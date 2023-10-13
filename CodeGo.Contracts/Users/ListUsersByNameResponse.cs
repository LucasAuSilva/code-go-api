
using System.Net;

namespace CodeGo.Contracts.Users;

// TODO: add profile visibility
public record ListUsersByNameResponse(
    string Id,
    string FirstName,
    string LastName,
    string ProfilePicture);
