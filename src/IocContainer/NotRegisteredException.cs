using System;

namespace IntrepidProducts.IocContainer
{
    public class NotRegisteredException : Exception
    {
        public NotRegisteredException(Type t, string msgAppendix = null)
            : base($"Type {t.FullName} is not registered {msgAppendix}")
        {}
    }
}