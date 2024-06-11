using GraphQL.Types;
using ToDoApp.Models;

namespace ToDoApp.GraphQl.Types
{
    public class CategoryType : ObjectGraphType<Category>
    {
        public CategoryType()
        {
            Name = nameof(Category);
            
            Field(c => c.Id);
            Field(c => c.Name);
        }
    }
}
