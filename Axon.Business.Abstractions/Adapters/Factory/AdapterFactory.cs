using System;
using System.Collections.Generic;
using System.Text;

namespace Axon.Business.Abstractions.Adapters.Factory
{
    public static class AdapterFactory
    {
        private static object _lock = new object();
        private static Dictionary<string, object> _adapters = new Dictionary<string, object>();
        public static ADAPTER Get<ADAPTER>()
            where ADAPTER : class, new()
        {
            var typename = typeof(ADAPTER).Name;
            try
            {
                lock (_lock)
                {
                    if (!_adapters.ContainsKey(typename))
                        _adapters.Add(typename, new ADAPTER());

                    return _adapters[typename] as ADAPTER;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
