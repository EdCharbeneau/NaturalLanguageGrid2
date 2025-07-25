using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

var client = new OllamaChatClient(new Uri("deepseek"), "deepseek");

builder.Services.AddChatClient(client!).UseLogging();

builder.Services.AddScoped<App>();

var host = builder.Build();

App app = host.Services.GetService<App>() ?? throw new InvalidOperationException("App was not provided to the service collection.");



await app.Run();

public class App(IChatClient chat)
{
	public async Task Run()
	{
		ChatOptions chatOptions = new()
		{
			AdditionalProperties = new AdditionalPropertiesDictionary()
			{
				{ "type", "json_object" },
			}
		};

		var response = await chat.CompleteAsync([
			new ChatMessage(ChatRole.System, "You are a helpful AI assistant. Respond only in JSON objects."),
			new ChatMessage(ChatRole.User, "Name the five largest states in the United States.")
			], chatOptions);

		Console.WriteLine(response.Choices[0].Text);
	}
}
