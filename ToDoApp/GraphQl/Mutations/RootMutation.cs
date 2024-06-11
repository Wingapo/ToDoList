using GraphQL.Types;

namespace ToDoApp.GraphQl.Mutations
{
    public class RootMutation : ObjectGraphType
    {
        public RootMutation()
        {
            Name = "mutation";

            Field<CategoryMutation>("categoryM").Resolve(ctx => new {});
            Field<NoteMutation>("noteM").Resolve(ctx => new {});
        }
    }
}
