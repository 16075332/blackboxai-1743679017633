using System.Collections.Generic;

namespace TaskManager
{
    public interface IFilterStrategy
    {
        IEnumerable<Task> ApplyFilter(IEnumerable<Task> tasks);
    }
}