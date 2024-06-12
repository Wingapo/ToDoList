using GraphQL;
using GraphQL.Types;
using ToDoApp.Data;
using ToDoApp.Data.Enums;
using ToDoApp.Data.Services;
using ToDoApp.GraphQl.Types;

namespace ToDoApp.GraphQl.Queries
{
    public class CategoryQuery : ObjectGraphType
    {
        public CategoryQuery(ServiceFactory serviceFactory)
        {
            Name = "categoryQ";

            StorageSource source = StorageSource.Header;

            Field<CategoryType>("get")
                .Argument<IdGraphType>("id")
                .Resolve(ctx =>
                {
                    int id = ctx.GetArgument<int>("id");
                    return serviceFactory.GetService<ICategoryService>(source).Get(id);
                });

            Field<ListGraphType<CategoryType>>("getAll")
                .Resolve(ctx => serviceFactory.GetService<ICategoryService>(source).GetAll());
        }
    }
}
