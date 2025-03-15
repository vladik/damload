using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DamLoad.Transformation.Services
{
    public interface ITransformationService
    {
        Task<TransformationResult> TransformAsync(TransformationRequest request);
    }
}
