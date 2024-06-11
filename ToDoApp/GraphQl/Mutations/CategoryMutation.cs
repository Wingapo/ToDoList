using GraphQL;
using GraphQL.Types;
using ToDoApp.Data;
using ToDoApp.Data.Enums;
using ToDoApp.Data.Services;
using ToDoApp.GraphQl.Types;
using ToDoApp.Models;

namespace ToDoApp.GraphQl.Mutations
{
    public class CategoryMutation : ObjectGraphType
    {
        public CategoryMutation(ServiceFactory serviceFactory)
        {
            Name = "categoryM";

            serviceFactory.Location = StorageTypeLocation.Header;

            Field<CategoryType>("add")
                .Argument<NonNullGraphType<CategoryInputType>>("category")
                .Resolve(ctx =>
                {
                    Category category = ctx.GetArgument<Category>("category");

                    category.Id = serviceFactory.GetService<ICategoryService>().Add(category);

                    return category;
                });

            Field<CategoryType>("delete")
                .Argument<NonNullGraphType<IdGraphType>>("id")
                .Resolve(ctx =>
                {
                    ICategoryService service = serviceFactory.GetService<ICategoryService>();

                    Category? category = service.Get(ctx.GetArgument<int>("id"));
                    if (category != null)
                    {
                    service.Delete(category.Id);
                    }
                    return category;
                });
        }
    }
}
