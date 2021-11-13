
namespace MartynasDRestAPI.Data.Dtos
{
    public record UserDto(
        int id,
        string username,
        string firstname,
        string lastname,
        string email,
        string phone
        );
}
