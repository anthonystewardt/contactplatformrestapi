using contactplatformweb.DTOs;
using contactplatformweb.Entities;
using contactplatformweb.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;


namespace contactplatformweb.Endpoints
{
    public static class CeseEndpoints
    {
        public static RouteGroupBuilder MapCeses(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetCesesController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10))).WithTags("ceses-get").RequireAuthorization();
            group.MapGet("/{id}", GetCeseByIdController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10)));
            group.MapPost("/", CreatedCeseController);
            group.MapPut("/{id}", UpdatedCeseController);
            group.MapDelete("/{id}", DeleteCeseController);
            return group;
        }
        
               static async Task<Ok<List<Cese>>> GetCesesController(IRepositoryCese repository)
                {
                    List<string> data = ["ceses-get", "data2"];
                    var ceses = await repository.GetCeses();
                    return TypedResults.Ok(ceses);
                }
        
               static async Task<Results<Ok<Cese>, NotFound>> GetCeseByIdController(int id, IRepositoryCese repository)
                {
                    var cese = await repository.GetCese(id);
                    if (cese == null)
                    {
                        return TypedResults.NotFound();
                    }
                    return TypedResults.Ok(cese);
                }
               
                static async Task<IResult> CreatedCeseController(CeseDTO ceseDto, IRepositoryCese repository, IOutputCacheStore outputCacheStore, ApplicationDBContext context)
                {
                     // get the user
                     var user = await context.Users.FindAsync(ceseDto.UserId);
                     // send error message if user not found
                     if (user == null)
                     {
                            return Results.BadRequest("User not found.");
                     }

                     

                    // get the ReasonDeparture
                     var reasonDeparture = await context.ReasonForDepartures.FindAsync(ceseDto.ReasonForDepartureId);

                    if (reasonDeparture == null)
                    {
                        return Results.BadRequest("Reason for departure not found.");
                    }

                    // change the column Active and HasCese to false
                    user.Active = false;
                    user.HasCese = true;
                    //save changes
                    await context.SaveChangesAsync();

                    var cese = new Cese
                      {
                            UserId = ceseDto.UserId,
                            User = user,
                            ReasonForDepartureId = ceseDto.ReasonForDepartureId,
                            ReasonForDeparture = reasonDeparture,
                            Resumen = ceseDto.Resumen,
                      };
                      var ceseResult = await repository.AddCese(cese);
                      //return Results.Created($"/campaigns/{createdCampaign.Id}", createdCampaign); 
                      return Results.Created($"/ceses/{ceseResult.Id}", ceseResult);
                }

                static async Task<IResult> UpdatedCeseController(int id, CeseDTO ceseDto, IRepositoryCese repository, ApplicationDBContext context)
        {
                    var cese = new Cese
                    {
                        Id = id,
                        UserId = ceseDto.UserId,
                        ReasonForDepartureId = ceseDto.ReasonForDepartureId,
                        Resumen = ceseDto.Resumen,
                    };
                    var ceseResult = await repository.UpdateCese(cese);
                    return Results.Ok(ceseResult);
                }
                
                static async Task<IResult> DeleteCeseController(int id, IRepositoryCese repository, IOutputCacheStore outputCacheStore)
        {
                    await repository.DeleteCese(id);
                    await outputCacheStore.EvictByTagAsync("ceses-get", default);
                    return Results.NoContent();
                }
            
    }
}
