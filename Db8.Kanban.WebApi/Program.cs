using Db8.Kanban.Core;
using Db8.Kanban.WebApi;
using Db8.Kanban.WebApi.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<KanbanDao>(opt => opt.UseInMemoryDatabase("KanbanTasks"));

builder.Services.AddCors(
    options => options.AddDefaultPolicy(
        policy => policy.WithOrigins([builder.Configuration["BackendUrl"] ?? "https://localhost:7153",
                builder.Configuration["FrontendUrl"] ?? "https://localhost:7247"])
            .AllowAnyMethod()
            .AllowAnyHeader()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseInMemoryDatabase("Db8.Kanban"));

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorization();

var app = builder.Build();

app.MapIdentityApi<IdentityUser>();

app.MapPost("/logout", async (SignInManager<IdentityUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.Ok();
});
    //.WithOpenApi()
    //.RequireAuthorization();

app.MapGet("/kanban-tasks/list/{username}", async (string username, KanbanDao db) =>
    await db.KanbanTasks.Where(kt => kt.Username.Equals(username)).ToListAsync());
    //.WithOpenApi()
    //.RequireAuthorization();

    app.MapGet("/kanban-tasks/{id}", async (int id, KanbanDao db) =>
        await db.KanbanTasks.FindAsync(id)
            is { } kanbanTask
            ? Results.Ok(kanbanTask)
            : Results.NotFound());
    //.WithOpenApi()
    //.RequireAuthorization();

app.MapPost("/kanban-tasks", async (KanbanTask task, KanbanDao db) =>
{
    db.KanbanTasks.Add(task);
    await db.SaveChangesAsync();

    return Results.Created($"/kanban-tasks/{task.Id}", task);
});
//.WithOpenApi()
//.RequireAuthorization();

app.MapPut("/kanban-tasks/{id}", async (Guid id, KanbanTask taskUpdate, KanbanDao db) =>
{
    var task = await db.KanbanTasks.FindAsync(id);

    if (task is null) return Results.NotFound();

    task.TaskTitle = taskUpdate.TaskTitle;
    task.Date = taskUpdate.Date;
    task.Description = taskUpdate.Description;
    task.Status = taskUpdate.Status;

    await db.SaveChangesAsync();

    return Results.NoContent();
});
//.WithOpenApi()
//.RequireAuthorization();

app.MapDelete("/kanban-tasks/{id}", async (Guid id, KanbanDao db) =>
{
    if (await db.KanbanTasks.FindAsync(id) is { } kanbanTask)
    {
        db.KanbanTasks.Remove(kanbanTask);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});
//.WithOpenApi()
//.RequireAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();

