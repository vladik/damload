﻿using DamLoad.Core.Base.Entities;

namespace DamLoad.Assets.Entities
{
    public class FolderEntity : IBaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public Guid? ParentId { get; set; }
    }
}
