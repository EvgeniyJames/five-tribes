namespace EJames.Models
{
    public class Resource
    {
        public ResourceType Type { get; }

        public Resource(ResourceType type)
        {
            Type = type;
        }
    }
}