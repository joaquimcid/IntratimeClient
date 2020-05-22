using System.Collections.Generic;
using System.Linq;

namespace IntratimeX.Mappers
{
    public abstract class BaseMapper<T, S> : IMapper<T, S>
    {
        public abstract S Map(T dto);
        public abstract T Map(S entity);

        public virtual IEnumerable<T> Map(IEnumerable<S> list)
        {
            return list == null ? new T[0] : list.Select(Map);
        }

        public virtual IEnumerable<S> Map(IEnumerable<T> list)
        {
            return list == null ? new S[0] : list.Select(Map);
        }
    }
}