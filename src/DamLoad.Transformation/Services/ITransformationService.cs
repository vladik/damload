using DamLoad.Transformation.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DamLoad.Transformation.Services
{
    public interface ITransformationService
    {
        ITransformationProvider? GetProvider(string providerName);
        Task<TransformationResult> TransformAsync(TransformationRequest request);
    }
}
