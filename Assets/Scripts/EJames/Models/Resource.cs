namespace EJames.Models
{
    public class Resource
    {
        public Resource(ResourceType type)
        {
            Type = type;
        }

        public ResourceType Type { get; }
    }
}