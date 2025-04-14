using Infrastructure.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation;

public class AddProjectDataExample : IExamplesProvider<AddProjectForm>
{
    public AddProjectForm GetExamples() => new()
    {

        Image = "https://example.com/project-image.png",
        ProjectName = "E-Commerce Platform",
        Description = "A platform for managing and selling digital and physical goods.",
        StartDate = new DateTime(2025, 5, 1),
        EndDate = new DateTime(2025, 9, 30),
        Budget = 50000m,
        ClientId = "client-001",
        UserId = "user-abc123"

    };

}