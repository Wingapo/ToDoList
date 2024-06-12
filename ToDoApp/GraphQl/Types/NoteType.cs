using GraphQL.Types;
using ToDoApp.Models;

namespace ToDoApp.GraphQl.Types
{
    public class NoteType : ObjectGraphType<Note>
    {
        public NoteType()
        {
            Name = nameof(Note);

            Field(n => n.Id);
            Field(n => n.Title);
            Field(n => n.Description, nullable: true);
            Field(n => n.Status);
            Field(n => n.Deadline, nullable: true);
            Field(n => n.Categories, type: typeof(ListGraphType<CategoryType>));
        }
    }
}
