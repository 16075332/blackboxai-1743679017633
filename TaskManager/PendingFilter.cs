using System.Collections.Generic;
using System.Linq;

namespace TaskManager
{
    public class PendingFilter : IFilterStrategy
    {
        public IEnumerable<Task> ApplyFilter(IEnumerable<Task> tasks)
        {
            return tasks.Where(t => !t.Completed);
        }
    }
}