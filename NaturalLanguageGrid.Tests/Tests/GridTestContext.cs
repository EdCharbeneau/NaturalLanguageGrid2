using Bunit;
using Telerik.Blazor.Components;
using Xunit.Abstractions;

namespace TelerikAIExtensions.Tests;

public class GridTestContext : BunitContext
{
	public ITestOutputHelper TestOutput { get; private set; }
	public GridTestContext(ITestOutputHelper output)
	{
		TestOutput = output;
		RenderTree.Add<TelerikRootComponent>();
		JSInterop.Mode = JSRuntimeMode.Loose;
		Services.AddTelerikBlazor();
	}

}