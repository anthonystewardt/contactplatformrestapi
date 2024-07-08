using contactplatformweb.DTOs;
using contactplatformweb.Entities;
using contactplatformweb.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;

namespace contactplatformweb.Endpoints
{
    public static class TeamEndpoints
    {
        public static RouteGroupBuilder MapTeams(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetTeamsController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10))).WithTags("teams-get");
            group.MapGet("/{id}", GetTeamByIdController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10)));
            group.MapPost("/", CreatedTeamController);
            group.MapPut("/{id}", UpdatedTeamController);
            group.MapDelete("/{id}", DeleteTeamController);
            group.MapPost("/{id}/user/{idUser}", AddUserToTeamController);

            return group;
        }

        static async Task<Ok<List<Team>>> GetTeamsController(IRepositoryTeam repository)
        {
            var teams = await repository.GetTeams();
            return TypedResults.Ok(teams);
        }

        static async Task<Results<Ok<Team>, NotFound>> GetTeamByIdController(int id, IRepositoryTeam repository)
        {
            var team = await repository.GetTeam(id);
            if (team == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(team);
        }

        static async Task<IResult> CreatedTeamController(TeamDTO teamDto, IRepositoryTeam repository, IOutputCacheStore outputCacheStore, ApplicationDBContext context)
        {
            // check if user exists
            var supervsorExist = await context.Users.FindAsync(teamDto.SupervisorId);

            // get the campaign
            var campaign = await context.Campaigns.FindAsync(teamDto.CampaignId);
            
            // get the ReasonDeparture
            var reasonDeparture = await context.ReasonForDepartures.FindAsync(teamDto.ReasonDepartureId);


            if (supervsorExist == null)
            {
                return Results.BadRequest("Supervisor not found.");
            }

            var team = new Team
            {
                SupervisorId = teamDto.SupervisorId,
                Supervisor = supervsorExist,
                Name = teamDto.Name,
                Body = teamDto.Body,
                CampaignId = teamDto.CampaignId,
                Campaign = campaign,
                ReasonDepartureId = teamDto.ReasonDepartureId,
                ReasonForDeparture = reasonDeparture,
                DateCese = teamDto.DateCese,

            };

            var createdTeam = await repository.AddTeam(team);
            await outputCacheStore.EvictByTagAsync("teams-get", default);
            return Results.Created($"/teams/{createdTeam.Id}", createdTeam);
        }

        static async Task<IResult> UpdatedTeamController(int id, TeamDTO teamDto, IRepositoryTeam repository, ApplicationDBContext context)
        {
            var team = await repository.GetTeam(id);
            if (team == null)
            {
                return Results.NotFound();
            }

            // check if user exists
            var supervsorExist = await context.Users.FindAsync(teamDto.SupervisorId);

            // get the campaign
            var campaign = await context.Campaigns.FindAsync(teamDto.CampaignId);
            
            // get the ReasonDeparture
            var reasonDeparture = await context.ReasonForDepartures.FindAsync(teamDto.ReasonDepartureId);

            if (supervsorExist == null)
            {
                return Results.BadRequest("Supervisor not found.");
            }

            team.SupervisorId = teamDto.SupervisorId;
            team.Supervisor = supervsorExist;
            team.Name = teamDto.Name;
            team.Body = teamDto.Body;
            team.CampaignId = teamDto.CampaignId;
            team.Campaign = campaign;
            team.ReasonDepartureId = teamDto.ReasonDepartureId;
            team.ReasonForDeparture = reasonDeparture;
            team.DateCese = teamDto.DateCese;

            return Results.Ok(await repository.UpdateTeam(team));
        }

        static async Task<IResult> DeleteTeamController(int id, IRepositoryTeam repository, IOutputCacheStore outputCacheStore)
        {
            var team = await repository.GetTeam(id);
            if (team == null)
            {
                return Results.NotFound();
            }

            await repository.DeleteTeam(id);
            await outputCacheStore.EvictByTagAsync("teams-get", default);
            return Results.NoContent();
        }

        // create AddUserToTeamController method where you can add a user to a team  in the Users list
        static async Task<IResult> AddUserToTeamController(int id, int idUser, IRepositoryTeam repository)
        {
            var team = await repository.GetTeam(id);
            if (team == null)
            {
                return TypedResults.NotFound();
            }

            var user = await repository.GetUser(idUser);  

            if (user == null)
            {
                return Results.NotFound();
            }

            team.Users.Add(user);
            await repository.UpdateTeam(team);
            return Results.NoContent();
        }

    }
}
