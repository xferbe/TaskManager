using MongoDB.Driver;
using TaskManager.API.Data.Configurations;

namespace TaskManager.API.Data.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly IMongoCollection<Models.Task> _tasksCollection;

        public TasksRepository(IDatabaseConfig databaseConfig)
        {
            var client = new MongoClient(databaseConfig.ConnectionString);
            var database = client.GetDatabase(databaseConfig.DatabaseName);

            _tasksCollection = database.GetCollection<Models.Task>("tasks");
        }

        public void Add(Models.Task task)
        {
            _tasksCollection.InsertOne(task);
        }

        public void Update(string id, Models.Task taskUpdated)
        {
            _tasksCollection.ReplaceOne(task => task.Id == id, taskUpdated);
        }

        public Models.Task Get(string id)
        {
            return _tasksCollection.Find(task => task.Id == id).FirstOrDefault();
        }

        public IEnumerable<Models.Task> Get()
        {
            return _tasksCollection.Find(task => true).ToList();
        }

        public void Delete(string id)
        {
            _tasksCollection.DeleteOne(task => task.Id == id);
        }
    }
}
