using Azure.AI.OpenAI;
using Azure;
using Microsoft.Extensions.AI;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Xunit.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Meziantou.Extensions.Logging.Xunit;
using Bunit;
using Telerik.Blazor.Components;
using Bunit.Rendering;
using SampleData;
using TelerikAIExtensions.NaturalLanguage;

namespace TelerikAIExtensions.Tests;

public static class TestHelpers
{
	public static List<CustomerDto> GetFakeCustomers() => new List<CustomerDto>() {
		new() {CustomerId = "1", Address= "Address 1", City = "City 1", CompanyName = "Company Name 1",  Country = "USA" },
		new() {CustomerId = "2", Address= "Address 2", City = "City 2", CompanyName = "Company Name 2",  Country = "USA" },
		new() {CustomerId = "3", Address= "Address 3", City = "City 3", CompanyName = "Company Name 3",  Country = "USA" },
		new() {CustomerId = "4", Address= "Address 4", City = "City 4", CompanyName = "Company Name 4",  Country = "USA" },
		new() {CustomerId = "5", Address= "Address 5", City = "City 5", CompanyName = "Company Name 5",  Country = "USA" },
	};
	public static NaturalLanguageGridService CreateNaturalLanguageGridService(ITestOutputHelper testOutput)
	{
		IConfiguration config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

		ILogger<NaturalLanguageGridService> logger = XUnitLogger.CreateLogger<NaturalLanguageGridService>(testOutput);

		var service = new NaturalLanguageGridService(
			GetChatClient(config),
			logger
		);
		service.Initialize();
		return service;
	}

	private static IChatClient GetChatClient(IConfiguration config)
	{
		// 🌐 The Uri of your provider
		var endpoint = config["Chat:AzureOpenAI:Endpoint"] ?? throw new InvalidOperationException("Missing configuration: Endpoint. See the README for details.");
		// 🔑 The API Key for your provider
		var apikey = config["Chat:AzureOpenAI:Key"] ?? throw new InvalidOperationException("Missing configuration: ApiKey. See the README for details.");
		// 🧠 The model name or azure deployment name
		var model = "gpt-4o-mini";

		var innerClient = new AzureOpenAIClient(
				new Uri(endpoint),
				new AzureKeyCredential(apikey)
			);

		//var innerClient = new OpenAIClient(credential, openAIOptions);

		IChatClient client = innerClient.GetChatClient(model).AsIChatClient();
		return client;

	}
	public static TelerikGrid<CustomerDto> FindGridInstance(this IRenderedComponent<ContainerFragment> c) =>
		c.FindComponent<TelerikGrid<CustomerDto>>().Instance;


}
