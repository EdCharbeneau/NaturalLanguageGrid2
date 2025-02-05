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

//builder.Services.AddSingleton(
//    new AzureOpenAIClient(
//        new Uri(builder.Configuration["AI:AzureOpenAI:Endpoint"] ??
//            throw new InvalidOperationException("The required AzureOpenAI endpoint was not configured for this application.")),
//        new AzureKeyCredential(builder.Configuration["AI:AzureOpenAI:Key"] ??
//            throw new InvalidOperationException("The required AzureOpenAI Key was not configured for this application."))
//    ));

//builder.Services.AddChatClient(services => services.GetRequiredService<AzureOpenAIClient>()
//    .AsChatClient(builder.Configuration["AI:AzureOpenAI:Chat:ModelId"] ?? "gpt-4o-mini"));

builder.AddOllamaSharpChatClient("deepseek");

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
