﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SqlRepo.Benchmark.Select;
using SqlRepo.SqlServer.ServiceCollection;

namespace SqlRepo.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddSqlRepo();

            SqlRepoBenchmarkDbContext dbContext = new SqlRepoBenchmarkDbContext();
            dbContext.Database.Migrate();

            services.AddSingleton<ISqlLogger, NoOpSqlLogger>();
            services.AddSingleton<IBenchmarkHelpers, BenchmarkHelpers>();

            services.AddSingleton<IBenchmarkOperation, SelectAllBenchmarkOperationDapper>();
            services.AddSingleton<IBenchmarkOperation, SelectAllBenchmarkOperationSqlRepo>();
            services.AddSingleton<IBenchmarkOperation, SelectAllBenchmarkOperationEfCore>();
            services.AddSingleton<IBenchmarkOperation, SelectTop5000BenchmarkOperationDapper>();
            services.AddSingleton<IBenchmarkOperation, SelectTop5000BenchmarkOperationSqlRepo>();
            services.AddSingleton<IBenchmarkOperation, SelectTop5000BenchmarkOperationEfCore>();
            services.AddSingleton<IBenchmarkOperation, SelectWhereBetweenBenchmarkOperationDapper>();
            services.AddSingleton<IBenchmarkOperation, SelectWhereBetweenBenchmarkOperationSqlRepo>();
            services.AddSingleton<IBenchmarkOperation, SelectWhereBetweenBenchmarkOperationEfCore>();
            services.AddSingleton<IBenchmarkOperation, SelectSingleColumnBenchmarkDapper>();
            services.AddSingleton<IBenchmarkOperation, SelectSingleColumnBenchmarkSqlRepo>();
            services.AddSingleton<IBenchmarkOperation, SelectSingleColumnBenchmarkEfCore>();

            services.AddSingleton<IBenchmarkRunner, BenchmarkRunner>();

            var serviceCollection = services.BuildServiceProvider();

            serviceCollection.GetService<IBenchmarkRunner>().Run();

            Console.WriteLine("Completed All Benchmarks");

            Console.ReadLine();
            Console.ReadLine();
            Console.ReadLine();
        }
    }
}