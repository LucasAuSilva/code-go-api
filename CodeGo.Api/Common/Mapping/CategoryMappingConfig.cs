using CodeGo.Application.Common.Results;
using CodeGo.Application.Users.Command.EditProfile;
using CodeGo.Application.Users.Command.RegisterCourse;
using CodeGo.Application.Users.Command.ResponseFriendshipRequest;
using CodeGo.Application.Users.Command.SendFriendshipRequest;
using CodeGo.Application.Users.Command.UpdateUserRole;
using CodeGo.Application.Users.Queries.ListFriendsRequests;
using CodeGo.Application.Users.Queries.ListUsersByEmail;
using CodeGo.Application.Users.Queries.UserProfile;
using CodeGo.Contracts.Categories;
using CodeGo.Contracts.Users;
using CodeGo.Domain.CategoryAggregateRoot;
using CodeGo.Domain.UserAggregateRoot;
using CodeGo.Domain.UserAggregateRoot.Entities;
using Mapster;

namespace CodeGo.Api.Common.Mapping;

public class CategoryMappingConfig : IRegister
{
 public void Register(TypeAdapterConfig config)
    {
        CategoryResponseConfig(config);
        ListAllCategoriesResponseMapping(config);
    }

    private static void CategoryResponseConfig(TypeAdapterConfig config)
    {
        config.NewConfig<Category, CategoryResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString())
            .Map(dest => dest.Language, src => src.Language.Value);
    }

    private static void ListAllCategoriesResponseMapping(TypeAdapterConfig config)
    {
        config.NewConfig<PagedListResult<Category>, PagedListResult<CategoryResponse>>()
            .Fork(config => config.Default.PreserveReference(true))
            .Map(dest => dest.Data, src => src.Data.Adapt<List<CategoryResponse>>())
            .PreserveReference(true);
    }
}
