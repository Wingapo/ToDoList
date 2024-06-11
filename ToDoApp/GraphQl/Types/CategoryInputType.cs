using GraphQL.Types;
using ToDoApp.Models;

namespace ToDoApp.GraphQl.Types
{
    public class CategoryInputType : InputObjectGraphType<Category>
    {
        public CategoryInputType()
        {
            Name = $"{nameof(Category)}Input";

            Field(c => c.Name);
        }
    }
}
