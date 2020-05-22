using System.Collections.Generic;

namespace IntratimeX.Mappers
{
    public interface IMapper<T, S>
    {
        S Map(T t);
        T Map(S s);
        IEnumerable<T> Map(IEnumerable<S> list);
        IEnumerable<S> Map(IEnumerable<T> list);
    }
}
