namespace TwoCS.TimeTracker.Data.Mongo
{
    using Core.Repositories;
    using Domain.Models;

    public class ProjectRepository : MongoRepository<Project>, IProjectRepository
    {
        
    }
}
