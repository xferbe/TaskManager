namespace TaskManager.API.Data.Repositories
{
    public interface ITasksRepository
    {
        void Add(Models.Task task);

        void Update(string id, Models.Task taskUpdated);
        
        IEnumerable<Models.Task> Get();
        
        Models.Task Get(string id);

        void Delete(string id);
    }
}
