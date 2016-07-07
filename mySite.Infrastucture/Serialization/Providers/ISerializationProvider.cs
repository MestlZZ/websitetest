using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySite.Infrastructure.Serialization.Providers
{
    public interface ISerializationProvider<T>
    {
        string Serialize(T data);
        T Deserialize(string data);
    }
}
