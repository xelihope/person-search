using System.ComponentModel.DataAnnotations;

namespace PersonSearch.Data.Enums
{
    public enum HobbyType
    {
        [Display(Name = nameof(Reading), ResourceType = typeof(Resources.HobbyType))]
        Reading = 1,
        [Display(Name = nameof(Writing), ResourceType = typeof(Resources.HobbyType))]
        Writing = 2,
        [Display(Name = nameof(Exercising), ResourceType = typeof(Resources.HobbyType))]
        Exercising = 3,
        [Display(Name = nameof(Programming), ResourceType = typeof(Resources.HobbyType))]
        Programming = 4,
        [Display(Name = nameof(VideoGames), ResourceType = typeof(Resources.HobbyType))]
        VideoGames = 5,
        [Display(Name = nameof(Television), ResourceType = typeof(Resources.HobbyType))]
        Television = 6,
        [Display(Name = nameof(Movies), ResourceType = typeof(Resources.HobbyType))]
        Movies = 7,
        [Display(Name = nameof(Art), ResourceType = typeof(Resources.HobbyType))]
        Art = 8,
        [Display(Name = nameof(PerformingArts), ResourceType = typeof(Resources.HobbyType))]
        PerformingArts = 9,
        [Display(Name = nameof(Sports), ResourceType = typeof(Resources.HobbyType))]
        Sports = 10
    }
}