using System.ComponentModel.DataAnnotations;

namespace PersonSearch.Data.Enums
{
    public enum State
    {
        Alabama = 1,
        Alaska,
        Arizona,
        Arkansas,
        California,
        Colorado,
        Conneticut,
        Delaware,
        Florida,
        Georgia,
        Hawaii,
        Idaho,
        Illinois,
        Indiana,
        Iowa,
        Kansas,
        Kentucky,
        Louisiana,
        Maine,
        Maryland,
        Massachusettes,
        Michigan,
        Minnesota,
        Mississippi,
        Missouri,
        Montana,
        Nebraska,
        Nevada,
        [Display(Name = nameof(NewHampshire), ResourceType = typeof(Resources.State))]
        NewHampshire,
        [Display(Name = nameof(NewJersey), ResourceType = typeof(Resources.State))]
        NewJersey,
        [Display(Name = nameof(NewMexico), ResourceType = typeof(Resources.State))]
        NewMexico,
        [Display(Name = nameof(NewYork), ResourceType = typeof(Resources.State))]
        NewYork,
        [Display(Name = nameof(NorthCarolina), ResourceType = typeof(Resources.State))]
        NorthCarolina,
        [Display(Name = nameof(NorthDakota), ResourceType = typeof(Resources.State))]
        NorthDakota,
        Ohio,
        Oklahoma,
        Oregon,
        Pensylvania,
        [Display(Name = nameof(RhodeIsland), ResourceType = typeof(Resources.State))]
        RhodeIsland,
        [Display(Name = nameof(SouthCarolina), ResourceType = typeof(Resources.State))]
        SouthCarolina,
        [Display(Name = nameof(SouthDakota), ResourceType = typeof(Resources.State))]
        SouthDakota,
        Tennessee,
        Texas,
        Utah,
        Vermont,
        Virginia,
        Washington,
        [Display(Name = nameof(WestVirginia), ResourceType = typeof(Resources.State))]
        WestVirginia,
        Wisconsin,
        Wyoming
    }
}