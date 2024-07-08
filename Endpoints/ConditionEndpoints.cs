using contactplatformweb.DTOs;
using contactplatformweb.Entities;
using contactplatformweb.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;

namespace contactplatformweb.Endpoints
{
    public static class ConditionEndpoints
    {
        public static RouteGroupBuilder MapConditions(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetConditionsController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10))).WithTags("conditions-get");
            group.MapGet("/{id}", GetConditionByIdController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10)));
            group.MapPost("/", CreatedConditionController);
            group.MapPut("/{id}", UpdatedConditionController);
            group.MapDelete("/{id}", DeleteConditionController);
            return group;
        }


        static async Task<Ok<List<Condition>>> GetConditionsController(IRepositoryCondition repository)
        {
            var conditions = await repository.GetConditions();
            return TypedResults.Ok(conditions);
        }

        static async Task<Results<Ok<Condition>, NotFound>> GetConditionByIdController(int id, IRepositoryCondition repository)
        {
            var condition = await repository.GetCondition(id);
            if (condition == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(condition);
        }

        static async Task<IResult> CreatedConditionController(ConditionDTO conditionDto, IRepositoryCondition repository, IOutputCacheStore outputCacheStore)
        {
            var condition = new Condition
            {
                Title = conditionDto.Title
            };

            var createdCondition = await repository.AddCondition(condition);
            await outputCacheStore.EvictByTagAsync("conditions-get", default);
            return Results.Created($"/conditions/{createdCondition.Id}", createdCondition);
        }

        static async Task<IResult> UpdatedConditionController(int id, ConditionDTO conditionDto, IRepositoryCondition repository, IOutputCacheStore outputCacheStore)
        {
            var existCondition = await repository.GetCondition(id);
            if (existCondition == null)
            {
                return Results.NotFound();
            }

            existCondition.Title = conditionDto.Title;
            await repository.UpdateCondition(existCondition);
            await outputCacheStore.EvictByTagAsync("conditions-get", default);
            return Results.NoContent();
        }

        static async Task<IResult> DeleteConditionController(int id, IRepositoryCondition repository, IOutputCacheStore outputCacheStore)
        {
            var existCondition = await repository.GetCondition(id);
            if (existCondition == null)
            {
                return Results.NotFound();
            }

            await repository.DeleteCondition(id);
            await outputCacheStore.EvictByTagAsync("conditions-get", default);
            return Results.NoContent();
        }


    }
}
