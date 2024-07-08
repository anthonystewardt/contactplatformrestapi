using contactplatformweb.DTOs;
using contactplatformweb.Entities;
using contactplatformweb.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;

namespace contactplatformweb.Endpoints
{
    public static class SubCampaignEndpoints
    {
        public static RouteGroupBuilder MapSubCampaigns(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetSubCampaignsController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10))).WithTags("subcampaigns-get");
            group.MapGet("/{id}", GetSubCampaignByIdController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10)));
            group.MapPost("/", CreatedSubCampaignController);
            // update subcampaign
            group.MapPut("/{id}", UpdatedSubCampaignController);
            // delete subcampaign
            group.MapDelete("/{id}", DeleteSubCampaignController);
            return group;
        }

        static async Task<Ok<List<SubCampaign>>> GetSubCampaignsController(IRepositorySubCampaign repository)
        {
            var subCampaigns = await repository.GetSubCampaigns();
            return TypedResults.Ok(subCampaigns);
        }

        static async Task<Results<Ok<SubCampaign>, NotFound>> GetSubCampaignByIdController(int id, IRepositorySubCampaign repository)
        {
            var subCampaign = await repository.GetSubCampaign(id);
            if (subCampaign == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(subCampaign);
        }

        static async Task<IResult> CreatedSubCampaignController(SubCampaignDTO subCampaignDto, IRepositorySubCampaign repository, IOutputCacheStore outputCacheStore, ApplicationDBContext context)
        {
            // check if campaign exists
            var campaign = await context.Campaigns.FindAsync(subCampaignDto.CampaignId);

            if (campaign == null)
            {
                return Results.BadRequest("Campaign not found.");
            }

            var subCampaign = new SubCampaign
            {
                Name = subCampaignDto.Name,
                CampaignId = subCampaignDto.CampaignId,
                Campaign = campaign
                
            };
            var newSubCampaign = await repository.AddSubCampaign(subCampaign);
            return TypedResults.Ok(newSubCampaign);
        }

        static async Task<IResult> UpdatedSubCampaignController(int id, SubCampaignDTO subCampaignDto, IRepositorySubCampaign repository, ApplicationDBContext context)
        {
            var subCampaign = await repository.GetSubCampaign(id);
            if (subCampaign == null)
            {
                return Results.NotFound();
            }
            subCampaign.Name = subCampaignDto.Name;
            subCampaign.CampaignId = subCampaignDto.CampaignId;
            var updatedSubCampaign = await repository.UpdateSubCampaign(subCampaign);
            return TypedResults.Ok(updatedSubCampaign);
        }


        static async Task<IResult> DeleteSubCampaignController(int id, IRepositorySubCampaign repository)
        {
            await repository.DeleteSubCampaign(id);
            return Results.NoContent();
        }


    }
}
