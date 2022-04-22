using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Domain.Handlers;

namespace HostService
{
    public class HandlerResolverService : IHandlerResolverService
    {
        private readonly IEnumerable<IHandler> _handlers;

        public HandlerResolverService(IEnumerable<IHandler> handlers)
        {
            _handlers = handlers;
        }

        public (object handler, MethodInfo method, Type parameterType) GetHandler(string commandType)
        {
            foreach (var handler in _handlers)
            {
                var method = handler.GetType().GetMethod("HandleAsync");
                if (method is null)
                {
                    continue;
                }

                var parameter = method.GetParameters().FirstOrDefault();
                if (parameter is null)
                {
                    continue;
                }

                var parameterType = parameter.ParameterType.ToString();
                if (parameterType == commandType)
                {
                    return (handler, method, parameter.ParameterType);
                }
            }

            throw new InvalidOperationException($"No handler found for the command: {commandType}");
        }
    }
}
