using contactplatformweb.DTOs;
using contactplatformweb.Entities;
using contactplatformweb.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;



namespace contactplatformweb.Endpoints
{
    public static class WeekEndpoints
    {
        public static RouteGroupBuilder MapWeeks(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetWeeksController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10))).WithTags("weeks-get");
            group.MapGet("/{id}", GetWeekByIdController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10)));
            group.MapGet("/byUserId/{userId}", GetWeeksByUserIdController);
            group.MapPost("/", CreatedWeekController);
            // update week
            //group.MapPut("/{userId}", UpdatedWeekController);
             group.MapPut("/{id}", UpdatedWeekController); // Aquí se usa {id}
            // delete week
            group.MapDelete("/{id}", DeleteWeekController);
            return group;
        }
        
        static async Task<Ok<List<Week>>> GetWeeksController(IRepositoryWeek repository)
        {
            var weeks = await repository.GetWeeks();
            return TypedResults.Ok(weeks);
        }

        static async Task<Results<Ok<Week>, NotFound>> GetWeekByIdController(int id, IRepositoryWeek repository)
        {
            var week = await repository.GetWeek(id);
            if (week == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(week);
        }

        static async Task<Results<Ok<List<Week>>, NotFound>> GetWeeksByUserIdController(int userId, IRepositoryWeek repository)
        {
            var weeks = await repository.GetWeeksByUserId(userId);
            if (weeks == null || weeks.Count == 0)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(weeks);
        }

        static async Task<IResult> CreatedWeekController(WeekDTO weekDto, IRepositoryWeek repository, IOutputCacheStore outputCacheStore, ApplicationDBContext context)
        {
            var week = new Week
            {
                DateStart = weekDto.DateStart,
                DateEnd = weekDto.DateEnd,
                UserId = weekDto.UserId,
                CalendarId = weekDto.CalendarId,
                StartTime = weekDto.StartTime,
                EndTime = weekDto.EndTime,
                Monday = weekDto.Monday,
                MondayIsActive = weekDto.MondayIsActive,
                MondayStartTime = weekDto.MondayStartTime,
                MondayEndTime = weekDto.MondayEndTime,
                Tuesday = weekDto.Tuesday,
                TuesdayIsActive = weekDto.TuesdayIsActive,
                TuesdayStartTime = weekDto.TuesdayStartTime,
                TuesdayEndTime = weekDto.TuesdayEndTime,
                Wednesday = weekDto.Wednesday,
                WednesdayIsActive = weekDto.WednesdayIsActive,
                WednesdayStartTime = weekDto.WednesdayStartTime,
                WednesdayEndTime = weekDto.WednesdayEndTime,
                Thursday = weekDto.Thursday,
                ThursdayIsActive = weekDto.ThursdayIsActive,
                ThursdayStartTime = weekDto.ThursdayStartTime,
                ThursdayEndTime = weekDto.ThursdayEndTime,
                Friday = weekDto.Friday,
                FridayIsActive = weekDto.FridayIsActive,
                FridayStartTime = weekDto.FridayStartTime,
                FridayEndTime = weekDto.FridayEndTime,
                Saturday = weekDto.Saturday,
                SaturdayIsActive = weekDto.SaturdayIsActive,
                SaturdayStartTime = weekDto.SaturdayStartTime,
                SaturdayEndTime = weekDto.SaturdayEndTime,
                Sunday = weekDto.Sunday,
                SundayIsActive = weekDto.SundayIsActive,
                SundayStartTime = weekDto.SundayStartTime,
                SundayEndTime = weekDto.SundayEndTime
            };
            await repository.AddWeek(week);
            return Results.Created($"/weeks/{week.Id}", week);
        }

        static async Task<IResult> UpdatedWeekController(int id, WeekDTO weekDto, IRepositoryWeek repository, ApplicationDBContext context)
        {
            var week = await repository.GetWeek(id);
            if (week == null)
            {
                return Results.NotFound();
            }
            week.DateStart = weekDto.DateStart;
            week.DateEnd = weekDto.DateEnd;
            week.UserId = weekDto.UserId;
            week.StartTime = weekDto.StartTime;
            week.EndTime = weekDto.EndTime;
            week.CalendarId = weekDto.CalendarId;
            week.Monday = weekDto.Monday;
            week.MondayIsActive = weekDto.MondayIsActive;
            week.MondayStartTime = weekDto.MondayStartTime;
            week.MondayEndTime = weekDto.MondayEndTime;
            week.Tuesday = weekDto.Tuesday;
            week.TuesdayIsActive = weekDto.TuesdayIsActive;
            week.TuesdayStartTime = weekDto.TuesdayStartTime;
            week.TuesdayEndTime = weekDto.TuesdayEndTime;
            week.Wednesday = weekDto.Wednesday;
            week.WednesdayIsActive = weekDto.WednesdayIsActive;
            week.WednesdayStartTime = weekDto.WednesdayStartTime;
            week.WednesdayEndTime = weekDto.WednesdayEndTime;
            week.Thursday = weekDto.Thursday;
            week.ThursdayIsActive = weekDto.ThursdayIsActive;
            week.ThursdayStartTime = weekDto.ThursdayStartTime;
            week.ThursdayEndTime = weekDto.ThursdayEndTime;
            week.Friday = weekDto.Friday;
            week.FridayIsActive = weekDto.FridayIsActive;
            week.FridayStartTime = weekDto.FridayStartTime;
            week.FridayEndTime = weekDto.FridayEndTime;
            week.Saturday = weekDto.Saturday;
            week.SaturdayIsActive = weekDto.SaturdayIsActive;
            week.SaturdayStartTime = weekDto.SaturdayStartTime;
            week.SaturdayEndTime = weekDto.SaturdayEndTime;
            week.Sunday = weekDto.Sunday;
            week.SundayIsActive = weekDto.SundayIsActive;
            await repository.UpdateWeek(week);
            return Results.NoContent();
        }

        static async Task<IResult> DeleteWeekController(int id, IRepositoryWeek repository)
        {
            var exist = await repository.ExistWeek(id);
            if (!exist)
            {
                return Results.NotFound();
            }
            await repository.DeleteWeek(id);
            return Results.NoContent();
        }

        

    }
}
