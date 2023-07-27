namespace DeepIn.Identity.Application.Models
{
    public class ClientModel: ClientDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
