using contactplatformweb.DTOs;
using contactplatformweb.Entities;
using contactplatformweb.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Components.Web;

namespace contactplatformweb.Endpoints
{
    public static class UserEndpoints
    {
        private static IConfiguration _configuration;
        public static RouteGroupBuilder MapUsers(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetUsersController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10))).WithTags("users-get");
            // get users inactives
            group.MapGet("/inactives", GetUserInactivesController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10))).WithTags("users-get");
            // Get users when has true in the column isPostulate
            group.MapGet("/postulate", GetUserPostulateController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10))).WithTags("users-get");
            group.MapGet("/{id}", GetUserByIdController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10)));

            // get all user if the column isSupervisor is true
            group.MapGet("/supervisor", GetUsersSupervisorController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10))).WithTags("users-get");

            // get all users by filter, for example, by name but regular expresion:  /users?name=^A
            group.MapGet("/search", GetUsersControllerByExpressionRegular).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10))).WithTags("users-get");


            group.MapPost("/", CreateduserController);
            // update user
            group.MapPut("/{id:int}", UpdatedUserController);
            // delete user
            group.MapDelete("/{id:int}", DeleteUserController);

            // get cese when user has cese
            group.MapGet("/{id}/cese", GetCeseByUserController).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(10))).WithTags("ceses-get");

            // delete multiple users
            group.MapDelete("/multiple-users", DeleteMultipleUsersController)
                                         .WithName("DeleteMultipleUsers")
                                         .WithTags("users-delete");

            // deactivate multiple users
            group.MapPost("/deactivate-users", DeactivateUsersController)
                                         .WithName("DeactivateUsers")
                                         .WithTags("users-deactivate");
            // add multiple users to week entity
            //group.MapPost("/add-users-to-week", AddUsersToWeekController)
             //                            .WithName("AddUsersToWeek")
             //                            .WithTags("users-add-to-week");

            // Activar multiple usuarios
            group.MapPost("/activate-users", ActivateUsersController)
                                         .WithName("ActivateUsers")
                                         .WithTags("users-activate");

            // add multiple users to team
            group.MapPost("/add-users-to-team", AddUsersToTeamController)
                                         .WithName("AddUsersToTeam")
                                         .WithTags("users-add-to-team");
            // add multiple users to capa
            group.MapPost("/add-users-to-capa", AddUsersToCapaController)
                                         .WithName("AddUsersToCapa")
                                         .WithTags("users-add-to-capa");

            
            // add calendar to user
            group.MapPost("/{userId}/calendar/{calendarId}", AddCalendarToUserController);

            //login
            group.MapPost("/login", LoginController);
            return group;
        }

        // create GetUsersSupervisorController method
        static async Task<Ok<List<User>>> GetUsersSupervisorController(IRepositoryUser repository)
        {
            var users = await repository.GetUsersSupervisor();
            return TypedResults.Ok(users);
        }

        // Método para obtener todos los usuarios por filtro, por ejemplo, por nombre pero expresión regular
        static async Task<Ok<List<User>>> GetUsersControllerByExpressionRegular([FromQuery] string name, IRepositoryUser repository)
        {
            var users = await repository.GetUsersByExpressionRegular(name);
            return TypedResults.Ok(users);
        }

        // Método para obtener todos los usuarios que tengan true en la columna isPostulate GetUserPostulateController method
        static async Task<Ok<List<User>>> GetUserPostulateController(IRepositoryUser repository)
        {
            var users = await repository.GetUserPostulate();
            return TypedResults.Ok(users);
        }

        // Método para obtener todos los ceses de un usuario
        static async Task<Ok<List<Cese>>> GetCeseByUserController(int id, IRepositoryCese repository)
        {
            var ceses = await repository.GetCesesByUser(id);
            return TypedResults.Ok(ceses);
        }


        // Método para agregar múltiples usuarios a un capa por lista de IDs (Entitdad Capa)
        static async Task<Results<NoContent, NotFound>> AddUsersToCapaController([FromBody] List<int> ids, int capaId, IRepositoryUser repository, IRepositoryCapa repositoryCapa, IOutputCacheStore outputCacheStore)
        {
            if (ids == null || ids.Count == 0)
            {
                return TypedResults.NotFound();
            }

            var capa = await repositoryCapa.GetCapa(capaId);
            if (capa == null)
            {
                return TypedResults.NotFound();
            }

            foreach (var id in ids)
            {
                var exist = await repository.ExistUser(id);
                var user = await repository.GetUser(id);
                if (exist)
                {
                    capa.Users.Add(user);
                    await repositoryCapa.UpdateCapa(capa);
                }
            }

            // Limpiar la caché
            await outputCacheStore.EvictByTagAsync("users-get", default);
            return TypedResults.NoContent();
        }



        // Método para eliminar múltiples usuarios por lista de IDs
        static async Task<Results<NoContent, NotFound>> DeleteMultipleUsersController([FromBody] List<int> ids, IRepositoryUser repository, IOutputCacheStore outputCacheStore)
        {
            if (ids == null || ids.Count == 0)
            {
                return TypedResults.NotFound();
            }

            foreach (var id in ids)
            {
                var exist = await repository.ExistUser(id);
                if (exist)
                {
                    await repository.DeleteUser(id);
                }
            }

            // Limpiar la caché
            await outputCacheStore.EvictByTagAsync("users-get", default);

            return TypedResults.NoContent();
        }


         // Método para agregar múltiples usuarios a un equipo por lista de IDs (Entitdad Team)
         static async Task<Results<NoContent, NotFound>> AddUsersToTeamController([FromBody] List<int> ids, int teamId, IRepositoryUser repository, IRepositoryTeam repositoryTeam, IOutputCacheStore outputCacheStore)
        {
             if (ids == null || ids.Count == 0)
            {
                 return TypedResults.NotFound();
             }
 
             var team = await repositoryTeam.GetTeam(teamId);
             if (team == null)
            {
                 return TypedResults.NotFound();
             }
 
             foreach (var id in ids)
            {
                 var exist = await repository.ExistUser(id);
                 var user = await repository.GetUser(id);
                 if (exist)
                {

                    team.Users.Add(user);
                    await repositoryTeam.UpdateTeam(team);
                 }
             }
 
             // Limpiar la caché
             await outputCacheStore.EvictByTagAsync("users-get", default);
             return TypedResults.NoContent();
         } 

        // Método para activar múltiples usuarios por lista de IDs
        static async Task<Results<NoContent, NotFound>> ActivateUsersController([FromBody] List<int> ids, IRepositoryUser repository, IOutputCacheStore outputCacheStore)
        {
            if (ids == null || ids.Count == 0)
            {
                return TypedResults.NotFound();
            }

            foreach (var id in ids)
            {
                var exist = await repository.ExistUser(id);
                if (exist)
                {
                    var user = await repository.GetUser(id);
                    user.IsPostulate = false;
                    user.Active = true;
                    await repository.UpdateUser(user);
                }
            }

            // Limpiar la caché
            await outputCacheStore.EvictByTagAsync("users-get", default);

            return TypedResults.NoContent();
        }


         // Método para desactivar múltiples usuarios por lista de IDs
         static async Task<Results<NoContent, NotFound>> DeactivateUsersController([FromBody] List<int> ids, IRepositoryUser repository, IOutputCacheStore outputCacheStore)
        {
             if (ids == null || ids.Count == 0)
            {
                 return TypedResults.NotFound();
             }
 
             foreach (var id in ids)
            {
                 var exist = await repository.ExistUser(id);
                 if (exist)
                {
                     var user = await repository.GetUser(id);
                     user.Active = false;
                     await repository.UpdateUser(user);
                 }
             }
 
             // Limpiar la caché
             await outputCacheStore.EvictByTagAsync("users-get", default);
 
             return TypedResults.NoContent();
         }

        static async Task<Results<Ok<UserResponse>, NotFound>> LoginController(LoginDTO userDto, IRepositoryUser repository, IConfiguration configuration)
        {
            var user = await repository.Login(userDto.Username, userDto.Password);
            if (user == null)
            {
                return TypedResults.NotFound();
            }
            // Generar token JWT
            var token = GenerateJwtToken(user.Id.ToString(), user.Email, configuration);

            var userResponse = new UserResponse
            {
                Token = token,
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                DNI = user.DNI,
                Username = user.Username,
                UserCampaign = user.UserCampaign,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address,
                City = user.City,
                Country = user.Country,
                Ip = user.Ip,
                IsPostulate = user.IsPostulate,
                Profession = user.Profession,
                ProfessionState = user.ProfessionState,
                JobPositionState = user.JobPositionState,
                Company = user.Company,
                UrlImage = user.UrlImage,
                Role = user.Role,
                PositionId = user.PositionId,
            };

            // Devolver el token como respuesta
           // return Ok(new { token });
           return TypedResults.Ok(userResponse);
        }

        public class UserResponse
        {
            public string Token { get; set; }
            public int Id { get; set; }
            public string Name { get; set; }

            public string LastName { get; set; }

            public string DNI { get; set; }

            public string Username { get; set; }

            public string UserCampaign { get; set; }

            public string Email { get; set; }

            public string Phone { get; set; }

            public string Address { get; set; }

            public string City { get; set; }

            public string Country { get; set; }


            public string Ip { get; set; }

            public string Profession { get; set; }

            public string ProfessionState { get; set; }


            public string JobPositionState { get; set; }

            public string Company { get; set; }

            public string UrlImage { get; set; }

            public string Role { get; set; }

            public bool? IsPostulate { get; set; }

            public int? PositionId { get; set; }

            public int? ModalityId { get; set; }
            // Agrega aquí otros campos que desees retornar del usuario
        }

        private static Results<Ok<User>, NotFound> Ok(object value)
        {
            throw new NotImplementedException();
        }

        private static object GenerateJwtToken(string v)
        {
            throw new NotImplementedException();
        }

        static async Task<Ok<List<User>>> GetUsersController(IRepositoryUser repository)
        {
            var users = await repository.GetUsers();
            return TypedResults.Ok(users);
        }


        // GetUserInactivesController method
        static async Task<Ok<List<User>>> GetUserInactivesController(IRepositoryUser repository)
        {
            var users = await repository.GetUserInactives();
            return TypedResults.Ok(users);
        }


        static async Task<Results<Ok<User>, NotFound>> GetUserByIdController(int id, IRepositoryUser repository)
        {
            var user = await repository.GetUser(id);
            if (user == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(user);
        }

        //static async Task<Created<User>> CreateduserController(User user, IRepositoryUser repository, IOutputCacheStore outputCacheStore)
        //{
        //    var id = await repository.AddUser(user);
        // clean cache
        //   await outputCacheStore.EvictByTagAsync("users-get", default);
        //   return TypedResults.Created($"/users/{id}", id);
        //}
        static async Task<Created<User>> CreateduserController(UserDTO userDto, IRepositoryUser repository, IOutputCacheStore outputCacheStore)
        {

            var hashedPassword = "";
            // if exist password, hash it
            if(userDto.Password != null)
            {
               hashedPassword = HashPassword(userDto.Password);
            }


            //var hashedPassword = HashPassword(userDto.Password);

            var user = new User
            {
                Name = userDto.Name,
                LastName = userDto.LastName,
                DNI = userDto.DNI,
                Username = userDto.Username,
                UserCampaign = userDto.UserCampaign,
                Email = userDto.Email,
                Phone = userDto.Phone,
                Address = userDto.Address,
                City = userDto.City,
                Country = userDto.Country,
                Ip = userDto.Ip,
                Profession = userDto.Profession,
                ProfessionState = userDto.ProfessionState,
                JobPosition = userDto.JobPosition,
                JobPositionState = userDto.JobPositionState,
                Active = userDto.Active,
                HasCese = userDto.HasCese,
                Company = userDto.Company,
                UrlImage = userDto.UrlImage,
                Role = userDto.Role,
                Cese = userDto.Cese,
                ConditionId = userDto.ConditionId,
                CampaignId = userDto.CampaignId,
                StateId = userDto.StateId,
                PositionId = userDto.PositionId,
                IsPostulate = userDto.IsPostulate,
                ReasonForDepartureId = userDto.ReasonForDepartureId,
                DateStart = userDto.DateStart,
                DateEnd = userDto.DateEnd,
                DateCese = userDto.DateCese,
                userVpn = userDto.userVpn,
                emailCompany = userDto.emailCompany,
                Password = hashedPassword,
                IsSupervisor = userDto.IsSupervisor,
                IsTrainer = userDto.IsTrainer,
                CalendarId = userDto.CalendarId,
                CapaId = userDto.CapaId,
                SubCampaignId = userDto.SubCampaignId,
                ModalityId = userDto.ModalityId
            };

            var id = await repository.AddUser(user);
            await outputCacheStore.EvictByTagAsync("users-get", default);
            return TypedResults.Created($"/users/{id}", user);
        }

        // updated
        // En UserEndpoints.cs
        static async Task<Results<NoContent, NotFound>> UpdatedUserController(int id, UserDTO userDto, IRepositoryUser repository, IOutputCacheStore outputCacheStore)
        {
            var exist = await repository.ExistUser(id);

            if (!exist)
            {
                return TypedResults.NotFound();
            }

            var user = await repository.GetUser(id);

            // Actualiza todos los campos proporcionados en el DTO
            foreach (var property in typeof(UserDTO).GetProperties())
            {
                // Obtén el nombre de la propiedad y su valor del DTO
                var propertyName = property.Name;
                var propertyValue = property.GetValue(userDto);

                // Verifica si el valor de la propiedad no es nulo y no es una propiedad que no deseas actualizar, como el ID o el correo electrónico
                if (propertyValue != null && propertyName != "Id" && propertyName != "Email")
                {
                    // Si la propiedad es la contraseña, hashea el valor antes de actualizarla
                    if (propertyName == "Password")
                    {
                        var hashedPassword = HashPassword(propertyValue.ToString());
                        user.Password = hashedPassword;
                    }
                    else
                    {
                        // Intenta obtener la propiedad correspondiente en la entidad User
                        var userProperty = typeof(User).GetProperty(propertyName);

                        if (userProperty != null)
                        {
                            // Actualiza el valor de la propiedad en la entidad User
                            userProperty.SetValue(user, propertyValue);
                        }
                    }
                }
            }

            await repository.UpdateUser(user);
            // Limpia la caché
            await outputCacheStore.EvictByTagAsync("users-get", default);

            return TypedResults.NoContent();
        }

        static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
        // delete
        static async Task<Results<NoContent, NotFound>> DeleteUserController(int id, IRepositoryUser repository, IOutputCacheStore outputCacheStore)
        {
            var exist = await repository.ExistUser(id);

            if (exist)
            {
                await repository.DeleteUser(id);
                // clean cache
                await outputCacheStore.EvictByTagAsync("users-get", default);
                return TypedResults.NoContent();
            }
            return TypedResults.NotFound();
        }

        // add calendar to user
        static async Task<IResult> AddCalendarToUserController(int userId, Guid calendarId, IRepositoryUser repository, IOutputCacheStore outputCacheStore)
        {
            // Obtener el usuario y el calendario
            var user = await repository.GetUser(userId);
            var calendar = await repository.GetCalendar(calendarId);

            // Verificar si el usuario y el calendario existen
            if (user == null || calendar == null)
            {
                return TypedResults.NotFound();
            }

            // Asociar el calendario al usuario
            user.CalendarId = calendarId;
            await repository.UpdateUser(user);

            // Limpiar la caché
            await outputCacheStore.EvictByTagAsync("users-get", default);

            // Retornar una respuesta exitosa
            return TypedResults.Created($"/users/{userId}/calendar/{calendarId}", user);
        }

        
        private static string GenerateJwtToken(string userId, string email, IConfiguration configuration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var expiryMinutes = Convert.ToInt32(configuration["Jwt:ExpiryMinutes"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Email, email)
            }),
                Expires = DateTime.UtcNow.AddMinutes(expiryMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = issuer,
                Audience = audience
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
