using Newtonsoft.Json;

namespace mySite.Infrastructure.Serialization.Providers
{
    public class SerializationProvider<T> : ISerializationProvider<T>
    {
        public string Serialize(T data)
        {
            return JsonConvert.SerializeObject(data);
        }

        public T Deserialize(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
