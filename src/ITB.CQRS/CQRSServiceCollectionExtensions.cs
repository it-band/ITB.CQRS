using System;
using System.Reflection;
using FluentValidation;
using ITB.CQRS.Abstraction;
using ITB.CQRS.Decorators;
using ITB.CQRS.Models;
using ITB.Shared.Domain;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;

namespace ITB.CQRS
{
    public static class CQRSServiceCollectionExtensions
    {
        public static IServiceCollection AddCQRS(this IServiceCollection service, Container container, Assembly[] assemblies, Action<CQRSOptions> setupAction = null)
        {
            container.Collection.Register(typeof(IAccessFilter<>), assemblies);
            container.Collection.Register(typeof(IPermissionValidator<>), assemblies);
            container.Collection.Register(typeof(IValidator<>), assemblies);

            container.RegisterSingleton<IHandlerDispatcher, HandlerDispatcher>();

            container.Register(typeof(IHandler<,>), assemblies);

            container.RegisterDecorator(typeof(IHandler<,>), typeof(TransactionHandlerDecorator<,>));

            container.RegisterDecorator(typeof(IHandler<,>), typeof(TransactionHandlerDecorator<>));

            container.RegisterDecorator(typeof(IHandler<,>), typeof(ValidationHandlerDecorator<,>));

            container.RegisterDecorator(typeof(IHandler<,>), typeof(ValidationHandlerDecorator<>));

            container.RegisterDecorator(typeof(IHandler<,>), typeof(PermissionValidationHandlerDecorator<,>));

            container.RegisterDecorator(typeof(IHandler<,>), typeof(PermissionValidationHandlerDecorator<>));

            container.RegisterDecorator(typeof(IHandler<,>), typeof(ErrorHandlerDecorator<,>));

            container.RegisterDecorator(typeof(IHandler<,>), typeof(ErrorHandlerDecorator<>));

            if (setupAction != null)
            {
                service.Configure(setupAction);
            }

            return service;
        }
    }
}
