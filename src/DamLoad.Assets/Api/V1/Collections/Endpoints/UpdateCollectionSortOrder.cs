using DamLoad.Assets.Api.V1.Collections.Requests;
using DamLoad.Assets.Api.V1.Collections.Validators;
using DamLoad.Assets.Services;
using FastEndpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DamLoad.Assets.Api.V1.Collections.Endpoints
{
    public class UpdateCollectionSortOrder : Endpoint<UpdateSortOrderRequest>
    {
        private readonly ICollectionService _service;

        public UpdateCollectionSortOrder(ICollectionService service) => _service = service;

        public override void Configure()
        {
            Put("/api/v1/collections/{collectionId:guid}/sort-order");
            AllowAnonymous();
            Validator<UpdateSortOrderValidator>();
        }

        public override async Task HandleAsync(UpdateSortOrderRequest req, CancellationToken ct)
        {
            await _service.UpdateSortOrderAsync(req.CollectionId, req.NewSortOrder);
            await SendOkAsync(ct);
        }
    }

}
