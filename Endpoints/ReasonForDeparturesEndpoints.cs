using contactplatformweb.DTOs;
using contactplatformweb.Entities;
using contactplatformweb.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;


namespace contactplatformweb.Endpoints
{
    public static class ReasonForDeparturesEndpoints
    {

        //MapReasonsForDepartures
        public static RouteGroupBuilder MapReasonForDepartures(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetReasonForDeparturesController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10))).WithTags("reasonfordepartures-get");
            group.MapGet("/{id}", GetReasonForDepartureByIdController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10)));
            group.MapPost("/", CreatedReasonForDepartureController);
            group.MapPut("/{id}", UpdatedReasonForDepartureController);
            group.MapDelete("/{id}", DeleteReasonForDeparture);
            return group;
        }
        
        // create GetReasonForDeparturesController
        static async Task<Ok<List<Entities.ReasonForDeparture>>> GetReasonForDeparturesController(IRepositoryReasonForDepartures repository)
        {
            var reasonForDepartures = await repository.GetReasonForDepartures();
            return TypedResults.Ok(reasonForDepartures);
        }

        // create GetReasonForDepartureByIdController
        static async Task<Results<Ok<Entities.ReasonForDeparture>, NotFound>> GetReasonForDepartureByIdController(int id, IRepositoryReasonForDepartures repository)
        {
            var reasonForDeparture = await repository.GetReasonForDeparture(id);
            if (reasonForDeparture == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(reasonForDeparture);
        }

        // create CreatedReasonForDepartureController
        static async Task<IResult> CreatedReasonForDepartureController(ReasonForDepartureDTO reasonForDepartureDTO, IRepositoryReasonForDepartures repository, IOutputCacheStore outputCacheStore)
        {
            

            var reasonForDeparture = new Entities.ReasonForDeparture
            {
                Title = reasonForDepartureDTO.Title,
                Active = reasonForDepartureDTO.Active
            };

           var createdReasonForDeparture = await repository.AddReasonForDeparture(reasonForDeparture);
           await outputCacheStore.EvictByTagAsync("reasonfordepartures-get", default);
           return Results.Created($"/reasonfordepartures/{createdReasonForDeparture.Id}", createdReasonForDeparture);
        }


        // create UpdatedReasonForDepartureController
        static async Task<IResult> UpdatedReasonForDepartureController(int id, ReasonForDepartureDTO reasonForDepartureDTO, IRepositoryReasonForDepartures repository, IOutputCacheStore outputCacheStore)
        {
           var existReasonForDeparture = await repository.GetReasonForDeparture(id);
            if (existReasonForDeparture == null)
            {
                return Results.NotFound();
            }

            existReasonForDeparture.Title = reasonForDepartureDTO.Title;
            existReasonForDeparture.Active = reasonForDepartureDTO.Active;

            var updatedReasonForDeparture = await repository.UpdateReasonForDeparture(existReasonForDeparture);
            await outputCacheStore.EvictByTagAsync("reasonfordepartures-get", default);
            return Results.Ok(updatedReasonForDeparture);
        }

        // create DeleteReasonForDeparture
        static async Task<IResult> DeleteReasonForDeparture(int id, IRepositoryReasonForDepartures repository, IOutputCacheStore outputCacheStore)
        {
            var existReasonForDeparture = await repository.GetReasonForDeparture(id);
            if (existReasonForDeparture == null)
            {
                return Results.NotFound();
            }

            await repository.DeleteReasonForDeparture(existReasonForDeparture.Id);
            await outputCacheStore.EvictByTagAsync("reasonfordepartures-get", default);
            return Results.NoContent();
        }
        
        
    }
}
