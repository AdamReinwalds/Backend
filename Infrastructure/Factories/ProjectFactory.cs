using Infrastructure.Data.Entities;
using Infrastructure.Models;
using Microsoft.CodeAnalysis;

namespace Infrastructure.Factories;

public class ProjectFactory
{
    public static ProjectEntity ToEntity(AddProjectForm? formData, string? newImageFileName = null)
    {

        if (formData == null) return null!;
        return new ProjectEntity
        {
            Image = newImageFileName ?? formData.Image,
            ProjectName = formData.ProjectName,
            Description = formData.Description,
            StartDate = formData.StartDate,
            EndDate = formData.EndDate,
            Budget = formData.Budget,
            ClientId = formData.ClientId,
            UserId = formData.UserId
        };
    }
    public static Project ToModel(ProjectEntity? entity)
    {
        if (entity == null) return null!;
        return new Project
            {
                Id = entity.Id,
                Image = entity.Image,
                ProjectName = entity.ProjectName,
                Description = entity.Description,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                Budget = entity.Budget,
                Client = new Client
                {
                    Id = entity.Client.Id,
                    ClientName = entity.Client.ClientName
                },
                User = new User
                {
                    Id = entity.User.Id,
                    FirstName = entity.User.FirstName!,
                    LastName = entity.User.LastName!
                },
                Status = new Status
                {
                    Id = entity.Status.Id,
                    StatusName = entity.Status.StatusName
                }
            };
    }

    public static ProjectEntity ToEntity(EditProjectForm? formData, string? newImageFileName = null)
    {
        if (formData == null) return null!;
            
        return new ProjectEntity
            {
                Id = formData.Id,
                Image = newImageFileName ?? formData.Image,
                ProjectName = formData.ProjectName,
                Description = formData.Description,
                StartDate = formData.StartDate,
                EndDate = formData.EndDate,
                Budget = formData.Budget,
                ClientId = formData.ClientId,
                UserId = formData.UserId,
                StatusId = formData.StatusId,
            };
    }
}