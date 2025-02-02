using System.ComponentModel.DataAnnotations;

namespace FootballersCatalog.API.Contracts
{
    public record FootballersRequest(
        [Display(Name = "Имя")][Required(ErrorMessage = "Необходимо указать имя")]
        string FirstName,
        [Display(Name = "Фамилия")][Required(ErrorMessage = "Необходимо указать фамилию")]
        string LastName,
        [Display(Name = "Дата рождения")][Required(ErrorMessage = "Необходимо указать фамилию")]
        DateOnly Birthday,
        [Display(Name = "Пол")][Required(ErrorMessage = "Необходимо указать пол")]
        string Gender,
        [Display(Name = "Команда")][Required(ErrorMessage = "Необходимо указать команду")]
        string TeamName,
        [Display(Name = "Страна")][Required(ErrorMessage = "Необходимо указать страну")]
        string CountryName);
}
