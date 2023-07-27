namespace DeepIn.Identity.Application
{
    public class ApiResourceDto
    {
        public bool Enabled { get; set; } = true;
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string AllowedAccessTokenSigningAlgorithms { get; set; }
        public bool ShowInDiscoveryDocument { get; set; } = true;
        public bool NonEditable { get; set; }
        public List<string> Scopes { get; set; }
        public List<string> UserClaims { get; set; }
    }
}
