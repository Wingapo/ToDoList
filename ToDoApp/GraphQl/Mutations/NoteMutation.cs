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

            serviceFactory.Location = StorageTypeLocation.Header;

            Field<NoteType>("add")
                .Argument<NonNullGraphType<NoteInputType>>("note")
                .Argument<ListGraphType<NonNullGraphType<IdGraphType>>>("categoryIds")
                .Resolve((ctx) =>
                {
                    Note note = ctx.GetArgument<Note>("note");
                    IEnumerable<int> categoryIds = ctx.GetArgument<IEnumerable<int>>("categoryIds") ?? new List<int>();

                    ICategoryService categoryService = serviceFactory.GetService<ICategoryService>();

                    foreach (var categoryId in categoryIds)
                    {
                        Category? category = categoryService.Get(categoryId);

                        if (category != null)
                        {
                            note.Note_Categories.Add(new Note_Category()
                            {
                                CategoryId = category.Id,
                                Category = category
                            });
                        }

                    }
                    note.Id = serviceFactory.GetService<INoteService>().Add(note);

                    return note;
                });

            Field<NoteType>("complete")
                .Argument<NonNullGraphType<IdGraphType>>("id")
                .Resolve(ctx =>
                {
                    INoteService service = serviceFactory.GetService<INoteService>();

                    Note? note = service.Get(ctx.GetArgument<int>("id"));

                    if (note != null)
                    {
                        note.Status = NoteStatus.Completed;
                        service.Update(note.Id, note);
                    }
                    return note;
                });

            Field<NoteType>("delete")
                .Argument<NonNullGraphType<IdGraphType>>("id")
                .Resolve(ctx =>
                {
                    INoteService service = serviceFactory.GetService<INoteService>();

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
