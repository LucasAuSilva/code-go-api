

using ErrorOr;

namespace CodeGo.Domain.Common.Errors;

public static partial class Errors
{
    public static class Categories
    {
        public static Error NotFound => Error.NotFound(
            code: "Category.NotFound",
            description: "Category with this id doesn't exists");
    }
}
