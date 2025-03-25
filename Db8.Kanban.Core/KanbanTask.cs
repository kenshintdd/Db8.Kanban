namespace Db8.Kanban.Core;

public enum KanbanStatus
{
    Todo,
    Ongoing,
    Finished
}

public class KanbanTask
{
    public Guid Id { get; set; }

    public string Username { get; set; }

    public string TaskTitle { get; set; }

    public DateTime Date { get; set; }
    
    public string Description { get; set; }

    public KanbanStatus Status { get; set; }
}