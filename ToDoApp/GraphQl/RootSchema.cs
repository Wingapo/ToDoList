using ToDoApp.GraphQl.Mutations;
using ToDoApp.GraphQl.Queries;

namespace ToDoApp.GraphQl.Schema
{
    public class RootSchema : GraphQL.Types.Schema
    {
        public RootSchema(IServiceProvider provider)
            : base(provider)
        {
            Query = provider.GetRequiredService<RootQuery>();
            Mutation = provider.GetRequiredService<RootMutation>();
        }
    }
}
