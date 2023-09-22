#region

using Unity.Netcode;

#endregion

namespace EJames.Serializers
{
    public struct NetworkStringSerializer : INetworkSerializable
    {
        public string Value;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref Value);
        }
    }
}