using Aspire.Hosting;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var ollama = builder.AddOllama("ollama")
					.WithDataVolume()
					.WithContainerRuntimeArgs("--gpus=all")
					//.WithOpenWebUI()
					;
//mlfoundations-dev/slim-orca_gpt-4o-mini_scale_x.25
var model = ollama.AddModel("chat", "starcoder2");

var exampleProject = builder.AddProject<NaturalLanguageGrid>("blazor")
							.WithReference(model)
							.WaitFor(model);
builder.Build().Run();
