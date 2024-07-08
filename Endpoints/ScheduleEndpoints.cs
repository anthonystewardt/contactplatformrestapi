using contactplatformweb.DTOs;
using contactplatformweb.Entities;
using contactplatformweb.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;

namespace contactplatformweb.Endpoints
{
    public static class ScheduleEndpoints
    {
        public static RouteGroupBuilder MapSchedules(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetSchedulesController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10))).WithTags("schedules-get");
            group.MapGet("/{id}", GetScheduleByIdController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10)));
            group.MapPost("/", CreatedScheduleController);
            // update schedule
            group.MapPut("/{id}", UpdatedScheduleController);
            // delete schedule
            group.MapDelete("/{id}", DeleteScheduleController);
            return group;
        }

        static async Task<Ok<List<Schedule>>> GetSchedulesController(IRepositorySchedule repository)
        {
            var schedules = await repository.GetSchedules();
            return TypedResults.Ok(schedules);
        }

        static async Task<Results<Ok<Schedule>, NotFound>> GetScheduleByIdController(Guid id, IRepositorySchedule repository)
        {
            var schedule = await repository.GetSchedule(id);
            if (schedule == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(schedule);
        }

        static async Task<IResult> CreatedScheduleController(ScheduleDTO scheduleDto, IRepositorySchedule repository, IOutputCacheStore outputCacheStore, ApplicationDBContext context)
        {
            var user = await context.Users.FindAsync(scheduleDto.UserId);
            if (user == null)
            {
                return Results.BadRequest("User not found.");
            }

            var schedule = new Schedule
            {
                Monday = scheduleDto.Monday,
                Tuesday = scheduleDto.Tuesday,
                Wednesday = scheduleDto.Wednesday,
                Thursday = scheduleDto.Thursday,
                Friday = scheduleDto.Friday,
                Saturday = scheduleDto.Saturday,
                Sunday = scheduleDto.Sunday,
                StartHour = scheduleDto.StartHour,
                EndHour = scheduleDto.EndHour,
                HourOfDay = scheduleDto.HourOfDay,
                UserId = scheduleDto.UserId,
                User = user
            };

            var createdSchedule = await repository.AddSchedule(schedule);
            await outputCacheStore.EvictByTagAsync("schedules-get", default);
            return Results.Created($"/schedules/{createdSchedule.Id}", createdSchedule);
        }

        // updated
        static async Task<IResult> UpdatedScheduleController(Guid id, ScheduleDTO scheduleDto, IRepositorySchedule repository, IOutputCacheStore outputCacheStore)
        {
            var existingSchedule = await repository.GetSchedule(id);

            if (existingSchedule == null)
            {
                return TypedResults.NotFound();
            }

            // Actualiza las propiedades del schedule existente con los valores del DTO
            existingSchedule.Monday = scheduleDto.Monday;
            existingSchedule.Tuesday = scheduleDto.Tuesday;
            existingSchedule.Wednesday = scheduleDto.Wednesday;
            existingSchedule.Thursday = scheduleDto.Thursday;
            existingSchedule.Friday = scheduleDto.Friday;
            existingSchedule.Saturday = scheduleDto.Saturday;
            existingSchedule.Sunday = scheduleDto.Sunday;
            existingSchedule.StartHour = scheduleDto.StartHour;
            existingSchedule.EndHour = scheduleDto.EndHour;
            existingSchedule.HourOfDay = scheduleDto.HourOfDay;

            var schedule = await repository.UpdateSchedule(existingSchedule);

            // Limpiar la caché
            await outputCacheStore.EvictByTagAsync("schedules-get", default);

            if(schedule == null)
            {
                return Results.BadRequest("Error updating schedule.");
            }else
            {
                return Results.Created($"/schedules/{schedule.Id}", schedule);
            }

        }

        static async Task<Results<NoContent, NotFound>> DeleteScheduleController(Guid id, IRepositorySchedule repository, IOutputCacheStore outputCacheStore)
        {
            var exist = await repository.ExistSchedule(id);

            if (exist)
            {
                await repository.DeleteSchedule(id);
                // clean cache
                await outputCacheStore.EvictByTagAsync("schedules-get", default);
                return TypedResults.NoContent();
            }
            return TypedResults.NotFound();
        }
    }
}
