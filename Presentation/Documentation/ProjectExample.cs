using Infrastructure.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.ProjectEndpoint;

public class ProjectExample : IExamplesProvider<Project>
{
    public Project GetExamples() => new()
    {
        Id = "proj-123",
        Image = "project-image.png",
        ProjectName = "New Web App Development",
        Description = "Developing a scalable web application for internal use.",
        StartDate = new DateTime(2025, 4, 1),
        EndDate = new DateTime(2025, 9, 30),
        Budget = 120000,

        Client = new Client
        {
            Id = "client-001",
            ClientName = "Tech Solutions AB"
        },

        User = new User
        {
            Id = "user-001",
            FirstName = "Emma",
            LastName = "Bergström"
        },

        Status = new Status
        {
            Id = 2,
            StatusName = "IN_PROGRESS"
        }
    };
}
