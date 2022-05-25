using System.ComponentModel.DataAnnotations;

namespace Logger.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}