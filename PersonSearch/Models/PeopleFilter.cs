namespace PersonSearch.Models
{
    public class PeopleFilter
    {
        public int PageSize { get; set; }

        public int Page { get; set; }

        public string NameFilter { get; set; }
    }
}