using GraphQL;
using GraphQL.Types;
using ToDoApp.Data;
using ToDoApp.Data.Enums;
using ToDoApp.Data.Services;
using ToDoApp.GraphQl.Types;

namespace ToDoApp.GraphQl.Queries
{
    public class NoteQuery : ObjectGraphType
    {
        public NoteQuery(ServiceFactory serviceFactory)
        {
            Name = "noteQ";

            StorageSource source = StorageSource.Header;

            Field<NoteType>("get")
                .Argument<IdGraphType>("id")
                .Resolve(ctx =>
                {
                    int id = ctx.GetArgument<int>("id");
                    return serviceFactory.GetService<INoteService>(source).Get(id);
                });

            Field<ListGraphType<NoteType>>("getAll")
                .Resolve(ctx => serviceFactory.GetService<INoteService>(source).GetAll());
        }
    }
}
