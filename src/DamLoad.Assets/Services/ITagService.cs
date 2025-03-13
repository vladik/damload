using DamLoad.Assets.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DamLoad.Assets.Services
{
    public interface ITagService
    {
        Task<List<TagEntity>> GetAllAsync();
        Task AddAsync(TagEntity tag);
        Task RenameAsync(Guid tagId, string newName);
        Task DeleteAsync(Guid tagId);
    }
}
