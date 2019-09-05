using System.ComponentModel.DataAnnotations;

namespace Nca.Api.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}