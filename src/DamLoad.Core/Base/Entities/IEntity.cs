﻿namespace DamLoad.Core.Base.Entities
{
    public interface IEntity : IBaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
}
}
