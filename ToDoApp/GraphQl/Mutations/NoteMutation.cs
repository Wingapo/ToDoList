using GraphQL;
using GraphQL.Types;
using ToDoApp.Data;
using ToDoApp.Data.Enums;
using ToDoApp.Data.Services;
using ToDoApp.GraphQl.Types;
using ToDoApp.Models;

namespace ToDoApp.GraphQl.Mutations
{
    public class NoteMutation : ObjectGraphType
    {
        public NoteMutation(ServiceFactory serviceFactory)
        {
            Name = "noteM";

            StorageSource source = StorageSource.Header;

            Field<NoteType>("add")
                .Argument<NonNullGraphType<NoteInputType>>("note")
                .Resolve((ctx) =>
                {
                    Note note = ctx.GetArgument<Note>("note");

                    ICategoryService categoryService = serviceFactory.GetService<ICategoryService>(source);

                    foreach (int categoryId in note.CategoryIds)
                    {
                        Category? category = categoryService.Get(categoryId);

                        if (category != null)
                        {
                            note.Categories.Add(category);
                        }

                    }
                    note.Id = serviceFactory.GetService<INoteService>(source).Add(note);

                    return note;
                });

            Field<NoteType>("complete")
                .Argument<NonNullGraphType<IdGraphType>>("id")
                .Resolve(ctx =>
                {
                    INoteService service = serviceFactory.GetService<INoteService>(source);

                    Note? note = service.Get(ctx.GetArgument<int>("id"));

                    if (note != null)
                    {
                        note.Status = NoteStatus.Completed;
                        service.Update(note);
                    }
                    return note;
                });

            Field<NoteType>("delete")
                .Argument<NonNullGraphType<IdGraphType>>("id")
                .Resolve(ctx =>
                {
                    INoteService service = serviceFactory.GetService<INoteService>(source);

                    Note? note = service.Get(ctx.GetArgument<int>("id"));
                    if (note != null)
                    {
                        service.Delete(note.Id);
                    }
                    return note;
                });
        }
    }
}
