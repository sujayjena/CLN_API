using System.Text.Json.Serialization;

namespace CLN.Application.Models
{
    public class PaginationParameters
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }

        [JsonIgnore]
        public int Total { get; set; }
        public string SortBy { get; set; }
        public string OrderBy { get; set; }
    }
}
