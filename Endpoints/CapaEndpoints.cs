using contactplatformweb.DTOs;
using contactplatformweb.Entities;
using contactplatformweb.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;



namespace contactplatformweb.Endpoints
{
    public static class CapaEndpoints
    {
        public static RouteGroupBuilder MapCapas(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetCapasController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10))).WithTags("capas-get").RequireAuthorization();
            group.MapGet("/{id}", GetCapaByIdController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10)));
            group.MapPost("/", CreatedCapaController);
            group.MapPut("/{id}", UpdatedCapaController);
            // remove an user from a capa
            group.MapDelete("/{id}/user/{idUser}", RemoveUserFromCapaController);
            group.MapPost("/{id}/user/{idUser}", AddUserToCapaController);
            group.MapDelete("/{id}", DeleteCapaController);
            return group;
        }

        static async Task<Ok<List<Capa>>> GetCapasController(IRepositoryCapa repository)
        {
            List<string> data = ["capas-get", "data2"];
            var capas = await repository.GetCapas();
            return TypedResults.Ok(capas);
        }

        static async Task<Results<Ok<Capa>, NotFound>> GetCapaByIdController(int id, IRepositoryCapa repository)
        {
            var capa = await repository.GetCapa(id);
            if (capa == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(capa);
        }

        static async Task<IResult> CreatedCapaController(CapaDTO capaDto, IRepositoryCapa repository, IOutputCacheStore outputCacheStore)
        {
            var capa = new Capa
            {
                Name = capaDto.Name,
                DateStart = capaDto.DateStart,
                DateEnd = capaDto.DateEnd,
                Active = capaDto.Active,
                CampaignId = capaDto.CampaignId,
                SubcampaignId = capaDto.SubcampaignId,
                trainerId = capaDto.TrainerId,
                supervisorId = capaDto.SupervisorId,
            };
            var capaResult = await repository.AddCapa(capa);
            //return Results.Created($"/campaigns/{createdCampaign.Id}", createdCampaign); 
            return Results.Created($"/capas/{capaResult.Id}", capaResult);
        }

        static async Task<IResult> UpdatedCapaController(int id, CapaDTO capaDto, IRepositoryCapa repository)
        {
            // get superisor
            var supervisor = await repository.GetUser(capaDto.SupervisorId);
            var capa = new Capa
            {
                Id = id,
                Name = capaDto.Name,
                DateStart = capaDto.DateStart,
                DateEnd = capaDto.DateEnd,
                Active = capaDto.Active,
                CampaignId = capaDto.CampaignId,
                SubcampaignId = capaDto.SubcampaignId,
                trainerId = capaDto.TrainerId,
                supervisorId = capaDto.SupervisorId,
            };
            var capaResult = await repository.UpdateCapa(capa);
            return Results.Ok(capaResult);
        }

        static async Task<IResult> DeleteCapaController(int id, IRepositoryCapa repository, IOutputCacheStore outputCacheStore)
        {
            await repository.DeleteCapa(id);
            await outputCacheStore.EvictByTagAsync("capas-get", default);
            return Results.NoContent();
        }

        // create AddUserToCapaController method where you can add a user to a capa  in the Users list
        static async Task<IResult> AddUserToCapaController(int id, int idUser, IRepositoryCapa repository)
        {
            var capa = await repository.GetCapa(id);
            if (capa == null)
            {
                return TypedResults.NotFound();
            }

            var user = await repository.GetUser(idUser);
            if (user == null)
            {
                return TypedResults.NotFound();
            }

            // Verificar si la lista de usuarios en la capa es null y crearla si es necesario
            if (capa.Users == null)
            {
                capa.Users = new List<User>();
            }

            // Verificar si el usuario ya está asociado a la capa
            if (capa.Users.Any(u => u.Id == idUser))
            {
                return Results.BadRequest("El usuario ya está asociado a esta capa.");
            }

            // Agregar el usuario a la capa
            capa.Users.Add(user);

            try
            {
                await repository.UpdateCapa(capa);
                return Results.Ok(capa);
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir al guardar
                return Results.BadRequest("Error al agregar el usuario a la capa.");
            }
        }

        // create RemoveUserFromCapaController method where you can remove a user from a capa  in the Users list
        static async Task<IResult> RemoveUserFromCapaController(int id, int idUser, IRepositoryCapa repository)
        {
            var capa = await repository.GetCapa(id);
            if (capa == null)
            {
                return TypedResults.NotFound();
            }

            var user = await repository.GetUser(idUser);
            if (user == null)
            {
                return TypedResults.NotFound();
            }

            // Verificar si la lista de usuarios en la capa es null y crearla si es necesario
            if (capa.Users == null)
            {
                capa.Users = new List<User>();
            }

            // Verificar si el usuario ya está asociado a la capa
            if (!capa.Users.Any(u => u.Id == idUser))
            {
                return Results.BadRequest("El usuario no está asociado a esta capa.");
            }

            // Remover el usuario de la capa
            capa.Users.Remove(user);

            try
            {
                await repository.UpdateCapa(capa);
                return Results.Ok(capa);
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir al guardar
                return Results.BadRequest("Error al remover el usuario de la capa.");
            }
        }

    }
}
