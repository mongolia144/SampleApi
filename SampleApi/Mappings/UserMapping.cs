using SampleApi.Models;
using SampleApi.DTOs.Movies;
using System.Data.Common;
using SampleApi.DTOs.Auth;

namespace SampleApi.Mappings.MovieMapping;
static class UserMapping
{
    public  static LoginDTO MapFromUserEntityToLoginDTO(User user)
    {
        var loginDTO = new LoginDTO()
        {
            Email = user.Email,
            //Password = user.PasswordHash   
        };
        
        return loginDTO;
    }

    public static User MapFromLoginDTOToUserEntity(LoginDTO loginDTO)
    {
        var userEntity = new User()
        {
            Email = loginDTO.Email,            
        };
        return userEntity;
    }

    public  static LoginDTOAdd MapFromUserEntityToLoginDTOAdd(User userEntity)
    {
        var loginDTOAdd = new LoginDTOAdd()
        {
             //Id = movieEntity.Id,
             Email = userEntity.Email
        };
        return loginDTOAdd;
    }

    public static User MapFromLoginDTOAddToUserEntity(LoginDTOAdd loginDTOAdd)
    {
        var userEntity = new User()
        {
            Email = loginDTOAdd.Email
        };
        return userEntity;
    }

/*    public  static MovieDTOUpdate MapFromMovieEntityToMovieDTOUpdate(Movie movieEntity)
    {
        var movieDTOUpdate = new MovieDTOUpdate()
        {
            Title = movieEntity.Title,
            Year = movieEntity.Year
        };
        return movieDTOUpdate;
    }
                        
    public static Movie MapFromMovieDTOUpdateToMovieEntity(MovieDTOUpdate movieDTOUpdate)
    {
        var movieEntity = new Movie()
        {
            Title = movieDTOUpdate.Title,
            Year = movieDTOUpdate.Year
        };
        return movieEntity;
    }*/
}