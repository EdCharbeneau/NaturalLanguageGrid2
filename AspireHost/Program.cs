using Aspire.Hosting;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var exampleProject = builder.AddProject<NaturalLanguageGrid>("blazor");

builder.Build().Run();
