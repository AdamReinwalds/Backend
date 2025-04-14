using Infrastructure.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation
{
    public class EditProjectDataExample : IExamplesProvider<EditProjectForm>
    {
        public EditProjectForm GetExamples() => new()
        {

            Id = "project-001",
            Image = "https://example.com/images/project-updated.png",
            ProjectName = "Updated ASP.NET Web App",
            Description = "Updated description for the ASP.NET project.",
            StartDate = new DateTime(2025, 4, 10),
            EndDate = new DateTime(2025, 9, 15),
            Budget = 120000m,
            ClientId = "client-123",
            UserId = "user-456",
            StatusId = 1

        };
    }
}
