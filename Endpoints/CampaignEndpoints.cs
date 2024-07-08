using contactplatformweb.DTOs;
using contactplatformweb.Entities;
using contactplatformweb.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;

namespace contactplatformweb.Endpoints
{
    public static class CampaignEndpoints
    {
        public static RouteGroupBuilder MapCampaigns(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetCampaignController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10))).WithTags("campaign-get");
            group.MapGet("/{id}", GetCampaignByIdController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10)));
            group.MapPost("/", CreatedCampaignController);
            // route to get subcampaigns by campaign id
            group.MapGet("/{id}/subcampaigns", GetSubCampaignsController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10)));
            group.MapPut("/{id}", UpdatedCampaignController);
            group.MapDelete("/{id}", DeleteCampaignController);
            return group;
        }

        static async Task<Ok<List<Campaign>>> GetCampaignController(IRepositoryCampaign repository)
        {
            List<string> data = ["calendars-get", "data2"];
            var campaign = await repository.GetCampaigns();
            return TypedResults.Ok(campaign);
        }
        //static async Task<List<string>> GetCalendarsController2(IRepositoryCampaign repository)
        //{
        //    List<string> data = ["calendars-get", "data2"];
        //   var calendars = await repository.GetCalendars();
        //return TypedResults.Ok(calendars);
        //   return data;
        // }

        // create GetSubCampaignsController method
        static async Task<Ok<List<SubCampaign>>> GetSubCampaignsController(int id, IRepositoryCampaign repository)
        {
            var subCampaigns = await repository.GetSubCampaigns(id);
            return TypedResults.Ok(subCampaigns);
        }
        
        static async Task<Results<Ok<Campaign>, NotFound>> GetCampaignByIdController(int id, IRepositoryCampaign repository)
        {
            var campaign = await repository.GetCampaign(id);
            if (campaign == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(campaign);
        }

        static async Task<IResult> CreatedCampaignController(CampaignDTO campaignDto, IRepositoryCampaign repository, IOutputCacheStore outputCacheStore)
        {
            var campaign = new Campaign
            {
                Name = campaignDto.Name,
                Cco = campaignDto.Cco
            };

            var createdCampaign = await repository.AddCampaign(campaign);
            await outputCacheStore.EvictByTagAsync("campaign-get", default);
            return Results.Created($"/campaigns/{createdCampaign.Id}", createdCampaign);
        }

        static async Task<IResult> UpdatedCampaignController(int id, CampaignDTO campaignDto, IRepositoryCampaign repository, IOutputCacheStore outputCacheStore)
        {
            var existCampaign = await repository.GetCampaign(id);

            if (existCampaign == null)
            {
                return TypedResults.NotFound();
            }

            existCampaign.Name = campaignDto.Name;
            existCampaign.Cco = campaignDto.Cco;

            var campaign = await repository.UpdateCampaign(existCampaign);
            await outputCacheStore.EvictByTagAsync("campaign-get", default);

            if (campaign == null)
            {
                return Results.BadRequest("Error updating campaign.");
            }
            else
            {
                return Results.Created($"/campaings/{campaign.Id}", campaign);
            }
        }

        static async Task<Results<NoContent, NotFound>> DeleteCampaignController(int id, IRepositoryCampaign repository, IOutputCacheStore outputCacheStore)
        {
            var exist = await repository.ExistCampaign(id);

            if (exist)
            {
                await repository.DeleteCampaign(id);
                await outputCacheStore.EvictByTagAsync("calendars-get", default);
                return TypedResults.NoContent();
            }
            return TypedResults.NotFound();
        }
    }
}
