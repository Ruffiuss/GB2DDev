using System.Collections.Generic;

namespace Tools
{
    public interface IRepository<Tkey, TValue>
    {
        IReadOnlyDictionary<Tkey, TValue> Content { get; }
    }
}