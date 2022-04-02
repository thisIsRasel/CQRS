using Application.CreateStudent;
using Application.CreateTeacher;
using Application.GetStudents;
using Domain;
using Domain.Aggregates.StudentAggregate;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(@"Server=THISISRASEL;Database=SchoolDB;Trusted_Connection=True;"));

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            services.AddTransient<IStudentReadRepository, StudentRepository>();
            services.AddTransient<IStudentWriteRepository, StudentRepository>();

            services.AddTransient<ICommandDispatcher, CommandDispatcher>();
            services.AddTransient<IQueryDispatcher, QueryDispatcher>();

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

        }
    }
}
