using GraphQL.Types;
using ToDoApp.Models;

namespace ToDoApp.GraphQl.Types
{
    public class NoteInputType : InputObjectGraphType<Note>
    {
        public NoteInputType()
        {
            Name = $"{nameof(Note)}Input";

            Field(n => n.Title);
            Field(n => n.Description, nullable: true);
            Field(n => n.Deadline, nullable: true);
            Field(n => n.CategoryIds, nullable: true);
        }
    }
}
