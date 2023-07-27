namespace DeepIn.Identity.Application.Models
{
    public class ApiResourceModel: ApiResourceDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
