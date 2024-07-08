using contactplatformweb.DTOs;
using contactplatformweb.Entities;
using contactplatformweb.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;

namespace contactplatformweb.Endpoints
{
    public static class CalendarEndpoints
    {
        public static RouteGroupBuilder MapCalendars(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetCalendarsController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10))).WithTags("calendars-get").RequireAuthorization();
            group.MapGet("/{id}", GetCalendarByIdController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10)));
            group.MapPost("/", CreatedCalendarController);
            group.MapPut("/{id}", UpdatedCalendarController);
            group.MapDelete("/{id}", DeleteCalendarController);
            return group;
        }

        static async Task<Ok<List<Calendar>>> GetCalendarsController(IRepositoryCalendar repository)
        {
            List<string> data = ["calendars-get", "data2"];
            var calendars = await repository.GetCalendars();
            return TypedResults.Ok(calendars);
        }
        static async Task<List<string>> GetCalendarsController2(IRepositoryCalendar repository)
        {
            List<string> data = ["calendars-get", "data2"];
            var calendars = await repository.GetCalendars();
            //return TypedResults.Ok(calendars);
            return data;
        }

        static async Task<Results<Ok<Calendar>, NotFound>> GetCalendarByIdController(Guid id, IRepositoryCalendar repository)
        {
            var calendar = await repository.GetCalendar(id);
            if (calendar == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(calendar);
        }

        static async Task<IResult> CreatedCalendarController(CalendarDTO calendarDto, IRepositoryCalendar repository, IOutputCacheStore outputCacheStore)
        {
            var calendar = new Calendar
            {
                Name = calendarDto.Name,
                Monday = calendarDto.Monday,
                Tuesday = calendarDto.Tuesday,
                Wednesday = calendarDto.Wednesday,
                Thursday = calendarDto.Thursday,
                Friday = calendarDto.Friday,
                Saturday = calendarDto.Saturday,
                Sunday = calendarDto.Sunday,
                StartHour = calendarDto.StartHour,
                EndHour = calendarDto.EndHour,
            };

            var createdCalendar = await repository.AddCalendar(calendar);
            await outputCacheStore.EvictByTagAsync("calendars-get", default);
            return Results.Created($"/calendars/{createdCalendar.Id}", createdCalendar);
        }

        static async Task<IResult> UpdatedCalendarController(Guid id, CalendarDTO calendarDto, IRepositoryCalendar repository, IOutputCacheStore outputCacheStore)
        {
            var existingCalendar = await repository.GetCalendar(id);

            if (existingCalendar == null)
            {
                return TypedResults.NotFound();
            }
            existingCalendar.Name = calendarDto.Name;
            existingCalendar.Monday = calendarDto.Monday;
            existingCalendar.Tuesday = calendarDto.Tuesday;
            existingCalendar.Wednesday = calendarDto.Wednesday;
            existingCalendar.Thursday = calendarDto.Thursday;
            existingCalendar.Friday = calendarDto.Friday;
            existingCalendar.Saturday = calendarDto.Saturday;
            existingCalendar.Sunday = calendarDto.Sunday;
            existingCalendar.StartHour = calendarDto.StartHour;
            existingCalendar.EndHour = calendarDto.EndHour;

            var calendar = await repository.UpdateCalendar(existingCalendar);
            await outputCacheStore.EvictByTagAsync("calendars-get", default);

            if (calendar == null)
            {
                return Results.BadRequest("Error updating calendar.");
            }
            else
            {
                return Results.Created($"/calendars/{calendar.Id}", calendar);
            }
        }

        static async Task<Results<NoContent, NotFound>> DeleteCalendarController(Guid id, IRepositoryCalendar repository, IOutputCacheStore outputCacheStore)
        {
            var exist = await repository.ExistCalendar(id);

            if (exist)
            {
                await repository.DeleteCalendar(id);
                await outputCacheStore.EvictByTagAsync("calendars-get", default);
                return TypedResults.NoContent();
            }
            return TypedResults.NotFound();
        }
    }
}
