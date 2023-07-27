namespace DeepIn.Identity.Application.Models
{
    public class IdentityResourceModel : IdentityResourceDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
