using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Documentation;
using Presentation.Documentation.ProjectEndpoint;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController(IProjectService projectService) : ControllerBase
{
    private readonly IProjectService _projectService = projectService;

    [HttpPost]
    [SwaggerOperation(Summary = "Creates a new project.", Description ="Only 'admins' can create cliets this will require an API-key 'X-ADM-API-KEY' in the header")]
    [SwaggerRequestExample(typeof(AddProjectForm), typeof(AddProjectDataExample))]
    [SwaggerResponse(200, "Project created successfully.")]
    [SwaggerResponse(400, "Invalid request body or project creation failed.")]
    public async Task<IActionResult> Create(AddProjectForm formData)
    {
        if (!ModelState.IsValid)
            return BadRequest(formData);

        var result = await _projectService.CreateProjectAsync(formData);
        return result ? Ok(result) : BadRequest("Project creation failed");
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Retrieves all projects.")]
    [SwaggerResponse(200, "Returns a list of all projects.", typeof(List<Project>))]
    public async Task<IActionResult> GetAll()
    {
        var result = await _projectService.GetProjectsAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Gets a specific project by its ID.")]
    [SwaggerResponse(200, "Returns a project by ID", typeof(Project))]
    [SwaggerResponseExample(200, typeof(ProjectExample))]
    [SwaggerResponse(404, "Project not found")]

    public async Task<IActionResult> Get(string id)
    {
        var result = await _projectService.GetProjectByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }


    [HttpPut]
    [SwaggerOperation(Summary = "Updates an existing project.")]
    [SwaggerRequestExample(typeof(EditProjectForm), typeof(EditProjectDataExample))]
    [SwaggerResponse(200, "Project updated successfully.")]
    [SwaggerResponse(400, "Invalid request body.")]
    [SwaggerResponse(404, "Project not found.")]
    public async Task<IActionResult> Update(EditProjectForm formData)
    {
        if (!ModelState.IsValid)
            return BadRequest(formData);

        var result = await _projectService.UpdateProjectAsync(formData);
        return result ? Ok(result) : NotFound();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Deletes a specific project by its ID.")]
    [SwaggerResponse(200, "Project deleted successfully.")]
    [SwaggerResponse(404, "Project not found.")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _projectService.DeleteProjectAsync(id);
        return result ? NoContent() : NotFound();
    }
}