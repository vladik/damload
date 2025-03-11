using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DamLoad.Core.Base.Entities
{
    public interface ISoftDeletedEntity
    {
        DateTime? DeletedAt { get; set; }
    }
}
