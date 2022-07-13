using System.Reflection;
using Application.CreateStudent;
using Application.GetStudents;
using Domain;
using Domain.Aggregates.StudentAggregate;
using Domain.Dispatchers;
using Domain.Handlers;
using Infrastructure.Dispatchers;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(optionBuilder =>
            {
                optionBuilder.UseSqlServer(
                    "Server=THISISRASEL;Database=SchoolDB;Trusted_Connection=True;TrustServerCertificate=True;");
            });

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            services.AddTransient<ICommandDispatcher, CommandDispatcher>();
            services.AddTransient<IQueryDispatcher, QueryDispatcher>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IStudentReadRepository, StudentRepository>();
            services.AddTransient<IStudentWriteRepository, StudentRepository>();

            //services.AddTransient<ICommandHandler<CreateStudentCommand, bool>, CreateStudentCommandHandler>();
            //services.AddTransient<ICommandHandler<CreateTeacherCommand, string>, CreateTeacherCommandHandler>();

            services.Scan(scan =>
            {
                scan
                 .FromAssemblyOf<CreateStudentCommandHandler>()
                 .AddClasses(classes =>
                 {
                     classes.AssignableTo(typeof(ICommandHandler<,>));
                 })
                 .AsImplementedInterfaces()
                 .WithTransientLifetime();

                scan
                 .FromAssemblyOf<GetStudentsQueryHandler>()
                 .AddClasses(classes =>
                 {
                     classes.AssignableTo(typeof(IQueryHandler<,>));
                 })
                 .AsImplementedInterfaces()
                 .WithTransientLifetime();
            });

            return services;
        }
    }
}
