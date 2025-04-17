using Infrastructure.Factories;
using Infrastructure.Handlers;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Services;

public interface IProjectService
{
    Task<bool> CreateProjectAsync(AddProjectForm formData, string defaultStatus = "started");
    Task<bool> DeleteProjectAsync(string id);
    Task<Project> GetProjectByIdAsync(string id);
    Task<IEnumerable<Project>> GetProjectsAsync();
    Task<bool> UpdateProjectAsync(EditProjectForm formData);
}

public class ProjectService(IProjectRepository projectRepository, IStatusService statusService, ICacheHandler<IEnumerable<Project>> cacheHandler) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly IStatusService _statusService = statusService;
    private readonly ICacheHandler<IEnumerable<Project>> _cacheHandler = cacheHandler;
    private const string _cacheKey = "Projects";


    public async Task<bool> CreateProjectAsync(AddProjectForm formData, string defaultStatus = "STARTED")
    {
        if (formData == null)
            return false;


        var projectEntity = ProjectFactory.ToEntity(formData);
        var status = await _statusService.GetStatusByStatusNameAsync(defaultStatus);

        if (status != null && status.Id != 0)
            projectEntity.StatusId = status.Id;
        else
            return false;

        var result = await _projectRepository.AddAsync(projectEntity);
        if (result)
        {
            var updatedProjects = await UpdateCacheAsync();
            var createdProject = updatedProjects.FirstOrDefault(x => x.ProjectName == formData.ProjectName);
            return createdProject != null;
        }
        return false;
    }


    public async Task<IEnumerable<Project>> GetProjectsAsync()
    {
        var cachedProjects = _cacheHandler.GetFromCache(_cacheKey);

        if (cachedProjects != null)
            return cachedProjects;


        var entites = await _projectRepository.GetAllAsync(
            orderByDescending: true,
            sortBy: x => x.Created,
            filterBy: null,
            i => i.User,
            i => i.Client,
            i => i.Status
        );

        var projects = entites.Select(ProjectFactory.ToModel).ToList();
        _cacheHandler.SetCache(_cacheKey, projects);
        return projects;
    }

    public async Task<Project> GetProjectByIdAsync(string id)
    {
        var cached = _cacheHandler.GetFromCache(_cacheKey);

        var match = cached?.FirstOrDefault(x => x.Id == id);
        if (match != null)
            return match;

        var entity = await _projectRepository.GetAsync(

            x => x.Id == id,
            i => i.User,
            i => i.Client,
            i => i.Status
        );

        await UpdateCacheAsync();
   
        return ProjectFactory.ToModel(entity);
    }

    public async Task<bool> UpdateProjectAsync(EditProjectForm formData)
    {

        if (formData == null)
            return false;

        if (!await _projectRepository.ExistsAsync(x => x.Id == formData.Id))
            return false;

        var projectEntity = ProjectFactory.ToEntity(formData);
        var result = await _projectRepository.UpdateAsync(projectEntity);
        await UpdateCacheAsync();
        return result;
    }


    public async Task<bool> DeleteProjectAsync(string id)
    {
        var result = await _projectRepository.DeleteAsync(x => x.Id == id);
        await UpdateCacheAsync();
        return result;
    }
    private async Task<IEnumerable<Project>> UpdateCacheAsync()
    {
        var projects = await _projectRepository.GetAllAsync(orderByDescending: true,
            sortBy: x => x.Created,
            filterBy: null,
            i => i.User,
            i => i.Client,
            i => i.Status);
        var models = projects.Select(ProjectFactory.ToModel).ToList();
        _cacheHandler.SetCache(_cacheKey, models);
        return models;
    }
}