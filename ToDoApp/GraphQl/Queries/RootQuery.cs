using GraphQL.Types;

namespace ToDoApp.GraphQl.Queries
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery()
        {
            Name = "query";
            
            Field<CategoryQuery>("categoryQ").Resolve(ctx => new {});
            Field<NoteQuery>("noteQ").Resolve(ctx => new {});
        }
    }
}
