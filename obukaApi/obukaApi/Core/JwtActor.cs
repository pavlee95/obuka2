namespace obukaApi.Core
{
    public class JwtActor
    {
        public int Id { get; set; }

        public string Identity { get; set; }

        public IEnumerable<int> AllowedRoles { get; set; }
    }
}
