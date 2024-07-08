using contactplatformweb.DTOs;
using contactplatformweb.Entities;
using contactplatformweb.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;


namespace contactplatformweb.Endpoints
{
    public static class ModalityEndpoints
    {
        public static RouteGroupBuilder MapModalities(this RouteGroupBuilder group)
        {
            // crud endpoints for modality
            group.MapGet("/", GetModalitiesController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10))).WithTags("modalities-get");
            group.MapGet("/{id}", GetModalityByIdController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10)));
            group.MapPost("/", CreatedModalityController);
            group.MapPut("/{id}", UpdatedModalityController);
            group.MapDelete("/{id}", DeleteModalityController);
            return group;
        }
        
        // get all modalities
        static async Task<Ok<List<Modality>>> GetModalitiesController(IRepositoryModality repository)
        {
            var modalities = await repository.GetModalities();
            return TypedResults.Ok(modalities);
        }

        // get modality by id
        static async Task<Results<Ok<Modality>, NotFound>> GetModalityByIdController(int id, IRepositoryModality repository)
        {
            var modality = await repository.GetModality(id);
            if (modality == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(modality);
        }

        // create modality
        static async Task<IResult> CreatedModalityController(ModalityDTO modalityDto, IRepositoryModality repository, IOutputCacheStore outputCacheStore)
        {
            var modality = new Modality
            {
                Name = modalityDto.Name
            };

            var createdModality = await repository.AddModality(modality);
            await outputCacheStore.EvictByTagAsync("modalities-get", default);
            return Results.Created($"/modalities/{createdModality.Id}", createdModality);
        }

        // update modality
        static async Task<IResult> UpdatedModalityController(int id, ModalityDTO modalityDto, IRepositoryModality repository, IOutputCacheStore outputCacheStore)
        {
            var existModality = await repository.GetModality(id);
            if (existModality == null)
            {
                return Results.NotFound();
            }

            existModality.Name = modalityDto.Name;
            await repository.UpdateModality(existModality);
            await outputCacheStore.EvictByTagAsync("modalities-get", default);
            return Results.NoContent();
        }

        // delete modality
        static async Task<IResult> DeleteModalityController(int id, IRepositoryModality repository, IOutputCacheStore outputCacheStore)
        {
            var existModality = await repository.GetModality(id);
            if (existModality == null)
            {
                return Results.NotFound();
            }

            await repository.DeleteModality(id);
            await outputCacheStore.EvictByTagAsync("modalities-get", default);
            return Results.NoContent();
        }



    }
}
