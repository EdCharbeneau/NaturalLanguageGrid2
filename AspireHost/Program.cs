using Aspire.Hosting;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var ollama = builder
	// 🦙 Add an ollama container
	.AddOllama("ollama") 
	// ⚡Speed up inference
	.WithGPUSupport() 
	// ♻️ Save the model to the data volume
	.WithDataVolume() 
	// 🌐 Enable prebuilt Web Interface
	.WithOpenWebUI(); 

// 🤖 Add a model
var deepseek = ollama.AddModel(
	name: "deepseek", // 🏷️ Name referenced by your application
	modelName: "mistral"
	);

var exampleProject = builder.AddProject<NaturalLanguageGrid>("blazor")
						.WithReference(deepseek)
						.WaitFor(deepseek);

builder.Build().Run();
