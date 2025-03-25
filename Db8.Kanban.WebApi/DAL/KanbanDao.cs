using Db8.Kanban.Core;
using Microsoft.EntityFrameworkCore;

namespace Db8.Kanban.WebApi.DAL;

class KanbanDao : DbContext
{
    public KanbanDao(DbContextOptions<KanbanDao> options)
        : base(options) { }

    public DbSet<KanbanTask> KanbanTasks => Set<KanbanTask>();
}
