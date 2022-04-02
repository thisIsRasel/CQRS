using System;
using System.Reflection;

namespace HostService
{
    public interface IHandlerResolverService
    {
        (object handler, MethodInfo method, Type parameter) GetHandler(string commandType);
    }
}
