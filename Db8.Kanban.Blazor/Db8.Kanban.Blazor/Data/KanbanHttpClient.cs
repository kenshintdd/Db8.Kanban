namespace Db8.Kanban.Blazor;

public class KanbanHttpClient(HttpClient client)
{
    public async Task<HttpResponseMessage> Register(string email, string password)
    {
       return await client.PostAsJsonAsync("register", new { email, password} );
    }

    public async Task<HttpResponseMessage> Login(string email, string password)
    {
        return await client.PostAsJsonAsync("login", new { email, password });
    }

    public async Task AddTask(KanbanTask task)
    {
        await client.PostAsJsonAsync("kanban-tasks", task);
    }

    public async Task<List<KanbanTask>?> GetTasks(string username)
    {
        return await client.GetFromJsonAsync<List<KanbanTask>>($"kanban-tasks/list/{username}");
    }
}
