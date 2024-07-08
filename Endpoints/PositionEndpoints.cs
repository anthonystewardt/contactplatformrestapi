using contactplatformweb.DTOs;
using contactplatformweb.Entities;
using contactplatformweb.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;


namespace contactplatformweb.Endpoints
{
    public static class PositionEndpoints
    {
        public static RouteGroupBuilder MapPositions(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetPositionsController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10))).WithTags("positions-get");
            group.MapGet("/{id}", GetPositionByIdController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10)));
            group.MapPost("/", CreatedPositionController);
            group.MapPut("/{id}", UpdatedPositionController);
            group.MapDelete("/{id}", DeletePositionController);
            return group; 
        }

        static async Task<Ok<List<Position>>> GetPositionsController(IRepositoryPosition repository)
        {
            var positions = await repository.GetPositions();
            return TypedResults.Ok(positions);
        }

        static async Task<Results<Ok<Position>, NotFound>> GetPositionByIdController(int id, IRepositoryPosition repository)
        {
            var position = await repository.GetPosition(id);
            if (position == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(position);
        }

        static async Task<IResult> CreatedPositionController(PositionDTO positionDto, IRepositoryPosition repository, IOutputCacheStore outputCacheStore)
        {
            var position = new Position
            {
                Name = positionDto.Name
            };

            var createdPosition = await repository.AddPosition(position);
            await outputCacheStore.EvictByTagAsync("positions-get", default);
            return Results.Created($"/positions/{createdPosition.Id}", createdPosition);
        }


        static async Task<IResult> UpdatedPositionController(int id, PositionDTO positionDto, IRepositoryPosition repository, IOutputCacheStore outputCacheStore)
        {
            var existPosition = await repository.GetPosition(id);
            if (existPosition == null)
            {
                return Results.NotFound();
            }

            existPosition.Name = positionDto.Name;

            var updatedPosition = await repository.UpdatePosition(existPosition);
            await outputCacheStore.EvictByTagAsync("positions-get", default);
            return Results.Ok(updatedPosition);
        }


        static async Task<IResult> DeletePositionController(int id, IRepositoryPosition repository, IOutputCacheStore outputCacheStore)
        {
            //var existPosition = await repository.GetPosition(id);
            //if (existPosition == null)
            //{
            //    return Results.NotFound();
            // }

            // await repository.DeletePosition(id);
            // await outputCacheStore.EvictByTagAsync("positions-get", default);
            // return Results.NoContent();
            try
            {
                var existPosition = await repository.GetPosition(id);
                if (existPosition == null)
                {
                    return Results.NotFound(new { message = "Position not found" });
                }

                await repository.DeletePosition(id);
                await outputCacheStore.EvictByTagAsync("positions-get", default);
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception details for further investigation
                // You can use a logging framework here, for example, Serilog, NLog, etc.
                Console.WriteLine(ex.Message);
                return Results.Problem("An unexpected error occurred while deleting the position.");
            }
        }
    }
}
