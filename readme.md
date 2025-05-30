# Natural Language AI-Powered Smart UI

This project uses commercial licenses that have free tiers or trials and OSS solutions. Be sure to have the proper prerequsites before testing.

## Prerequsites

- Telerik UI for Blazor (Free trial 30 days). Source code may need modification when using the trial version due to dependency naming convetions in the Trial version.
- Azure Open AI deployment of gpt-o4-mini
- .NET with the Web workload

## User Secrets

Because this project uses API keys, be sure to set your user secrets. This is the schema used in the project.

```
{
  "AI": {
    "AzureOpenAI": {
      "Key": "your-api-key",
      "Endpoint": "https://your-deployment-endpoint.openai.azure.com",
      "ModelId": "gpt-4o-mini"
    }
  }
}
```
