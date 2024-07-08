using contactplatformweb.DTOs;
using contactplatformweb.Entities;
using contactplatformweb.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;

namespace contactplatformweb.Endpoints
{
    public static class TrainerEndpoints
    {
        public static RouteGroupBuilder MapTrainers(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetTrainersController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10))).WithTags("trainers-get");
            group.MapGet("/{id}", GetTrainerByIdController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10)));
            group.MapPost("/", CreatedTrainerController);
            group.MapPut("/{id}", UpdatedTrainerController);
            group.MapDelete("/{id}", DeleteTrainerController);
            return group;
        }
        
        static async Task<Ok<List<Trainer>>> GetTrainersController(IRepositoryTrainer repository)
        {
            var trainers = await repository.GetTrainers();
            return TypedResults.Ok(trainers);
        }
        
        static async Task<Results<Ok<Trainer>, NotFound>> GetTrainerByIdController(int id, IRepositoryTrainer repository)
        {
            var trainer = await repository.GetTrainer(id);
            if (trainer == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(trainer);
        }
        
        static async Task<IResult> CreatedTrainerController(TrainerDTO trainerDto, IRepositoryTrainer repository, IOutputCacheStore outputCacheStore, ApplicationDBContext context)
        {
            // check if user exists
            var user = await context.Users.FindAsync(trainerDto.UserId);

            if (user == null)
            {
                return Results.BadRequest("User not found.");
            }

            var trainer = new Trainer
            {
                UserId = trainerDto.UserId,
                User = user
            };

            var createdTrainer = await repository.AddTrainer(trainer);
            await outputCacheStore.EvictByTagAsync("trainers-get", default);
            return Results.Created($"/trainers/{createdTrainer.Id}", createdTrainer);
        }
        
        static async Task<IResult> UpdatedTrainerController(int id, TrainerDTO trainerDto, IRepositoryTrainer repository, IOutputCacheStore outputCacheStore)
        {
            var existTrainer = await repository.GetTrainer(id);
            if (existTrainer == null)
            {
                return Results.NotFound();
            }

            existTrainer.UserId = trainerDto.UserId;

            return Results.Ok(await repository.UpdateTrainer(existTrainer));
        }

        static async Task<IResult> DeleteTrainerController(int id, IRepositoryTrainer repository, IOutputCacheStore outputCacheStore)
        {
            var existTrainer = await repository.GetTrainer(id);
            if (existTrainer == null)
            {
                return Results.NotFound();
            }

            await repository.DeleteTrainer(id);
            await outputCacheStore.EvictByTagAsync("trainers-get", default);
            return Results.NoContent();
        }

        
    }
}
