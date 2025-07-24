using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.AI;
using NaturalLanguageGrid.Components;
using TelerikAIExtensions.NaturalLanguage;
var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

builder.Services.AddTelerikBlazor();

// Add HttpClient to the service container
builder.Services.AddHttpClient();
builder.Services.AddScoped<CustomerService>();
//build the IChatClient

/* Configuration Schema
 {
  "AI": {
    "AzureOpenAI": {
      "Key": "YOUR_SUBSCRIPTION_KEY",
      "Endpoint": "YOUR_ENDPOINT"
    }
  }
}
*/

#region ChatClient
// 🌐 The Uri of your provider
var endpoint = builder.Configuration["Chat:AzureOpenAI:Endpoint"] ?? throw new InvalidOperationException("Missing configuration: Endpoint. See the README for details.");
// 🔑 The API Key for your provider
var apikey = builder.Configuration["Chat:AzureOpenAI:Key"] ?? throw new InvalidOperationException("Missing configuration: ApiKey. See the README for details.");
// 🧠 The model name or azure deployment name
var model = "gpt-o4-mini";

var innerClient = new AzureOpenAIClient(
        new Uri(endpoint),
        new AzureKeyCredential(apikey)
    );

//var innerClient = new OpenAIClient(credential, openAIOptions);

IChatClient client = innerClient.GetChatClient(model).AsIChatClient();

builder.Services.AddChatClient(client).UseFunctionInvocation().UseLogging();

#endregion

builder.Services.AddScoped<NaturalLanguageGridService>();
builder.Services.AddSpeechRecognitionServices();
// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();


app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();
