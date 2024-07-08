using contactplatformweb.DTOs;
using contactplatformweb.Entities;
using contactplatformweb.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;


namespace contactplatformweb.Endpoints
{
    public static class StateEndpoints
    {
        public static RouteGroupBuilder MapStates(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetStatesController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10))).WithTags("states-get");
            group.MapGet("/{id}", GetStateByIdController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10)));
            group.MapPost("/", CreatedStateController);
            // update state
            group.MapPut("/{id}", UpdatedStateController);
            // delete state
            group.MapDelete("/{id}", DeleteStateController);
            return group;
        }

        static async Task<Ok<List<State>>> GetStatesController(IRepositoryState repository)
        {
            var states = await repository.GetStates();
            return TypedResults.Ok(states);
        }

        static async Task<Results<Ok<State>, NotFound>> GetStateByIdController(int id, IRepositoryState repository)
        {
            var state = await repository.GetState(id);
            if (state == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(state);
        }

        static async Task<IResult> CreatedStateController(StateDTO stateDto, IRepositoryState repository, IOutputCacheStore outputCacheStore, ApplicationDBContext context)
        {
            var state = new State
            {
                Name = stateDto.Name
            };
            var newState = await repository.AddState(state);
            return TypedResults.Ok(newState);
        }

        static async Task<IResult> UpdatedStateController(int id, StateDTO stateDto, IRepositoryState repository, ApplicationDBContext context)
        {
            var state = await repository.GetState(id);
            if (state == null)
            {
                return Results.NotFound();
            }
            state.Name = stateDto.Name;
            var updatedState = await repository.UpdateState(state);
            return TypedResults.Ok(updatedState);
        }

        static async Task<IResult> DeleteStateController(int id, IRepositoryState repository)
        {
            var exist = await repository.ExistState(id);
            if (!exist)
            {
                return Results.NotFound();
            }
            await repository.DeleteState(id);
            return Results.NoContent();
        }
    }
}
