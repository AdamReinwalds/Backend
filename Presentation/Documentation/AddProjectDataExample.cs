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
        ClientId = "423b8e58-f690-41ee-80ae-4b4d5f12bdf8",
        UserId = "8b3ca3a9-6cc7-4be7-a1fe-f1ab470ebbac"

    };

}