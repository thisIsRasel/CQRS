using System;
using System.Reflection;

namespace HostService
{
    public interface IHandlerResolverService
    {
        (object handler, MethodInfo method, Type parameterType) GetHandler(string commandType);
    }
}
