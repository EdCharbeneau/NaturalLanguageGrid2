using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Telerik.Blazor.Components;
using Telerik.DataSource.Extensions;

namespace TelerikAIExtensions.NaturalLanguage;
public partial class NaturalLanguageGridService
{
	private readonly IChatClient _chatClient;
	private readonly ILogger<NaturalLanguageGridService> _logger;

	private ChatMessage? AIContext { get; set; }

	public NaturalLanguageGridService(IChatClient chatClient, ILogger<NaturalLanguageGridService> logger)
	{
		_chatClient = chatClient;
		_logger = logger;
	}

	public void Initialize()
	{
        string systemPrompt = string.Format(gridPrompt, jsonSchema);
		AIContext = new(ChatRole.System, systemPrompt);
	}

	public async Task<GridState<T>?> ProcessGridRequest<T>(string query, GridState<T> state)
	{
		if (AIContext is null) throw new InvalidOperationException("The Initalize method must be called before proccessing requests");

		// Extract the column names from the current state
		// The model understands them better if they are added to the prompt directly as an array
		string[] columns = state.ColumnStates.Select(x => x.Field).ToArray();

		// Add the columns to a new state object
		NaturalLanguageGridState<T> currentState = new(state, new ColumnOrdering(columns));

		// Serialize to JSON for prompting
		string currentJsonState = JsonSerializer.Serialize(state, new JsonSerializerOptions(JsonSerializerDefaults.Web));
		
		_logger.LogInformation($"Current State: {currentJsonState}");

		return await TryProcessingGridState(query, currentJsonState);

		// Augment the user's prompt with some sugar so that the LLM understands it better.
		// The current state of the grid is added so the LLM is aware of the avialable columns and what state they are in.
		/// Creates a new chat message with the user's request and the current state of the grid
		ChatMessage CreateChatMessage(string query, string currentJsonState) => new(ChatRole.User, $"""
			**User Request**: Update the given the Current GridState with my request. {query}
			**Current GridState as JSON**: {currentJsonState}
			**Current Column Order from left to right**: "ColumnOrdering":{currentState.ColumnOrdering}
			""");

		/// Remaps the columns in the new state to match the original state's ordering
		GridState<T>? RemapColumns(NaturalLanguageGridState<T> newState)
		{
			string[]? newColumns = newState?.ColumnOrdering.Columns;

			// Has Reordered columns
			if (newColumns is { Length: > 0 } && newState is not null)
			{
				// Columns are a simple array of Field names. They need to be mapped to an object
				var mappedColumns = newColumns.Select((c, i) => new { Field = c, Index = i });

				// Reorder the columns based on the original state's ordering, this will align the Index values so they can be updated properly
				var remappedColumns = mappedColumns.OrderBy(x => state.ColumnStates.First(y => y.Field == x.Field).Index).ToArray();

				// rewrite the column states with the new indexes
				for (int i = 0; i < newState.GridState.ColumnStates.Count; i++)
				{
					newState.GridState.ColumnStates.ElementAt(i).Index = remappedColumns[i].Index;
				}
			}
			return newState?.GridState;
		}

		/// Attempts to process the user's request and returns the new grid state
		async Task<GridState<T>?> TryProcessingGridState(string query, string currentJsonState)
		{
			// ChatOptions chatOptions = new() { ResponseFormat = ChatResponseFormat.Json };

			ChatMessage UserMessage = CreateChatMessage(query, currentJsonState);

			var response = await _chatClient.GetResponseAsync<NaturalLanguageGridState<T>>([AIContext, UserMessage]);

			return response.TryGetResult(out var newState) ? RemapColumns(newState) : null;
		}
	}

	/// <summary>
	/// A wrapper that contains a Telerik GridState<typeparamref name="T"/> and the current column ordering.
	/// An adapter object that allows the AI to properly hand column ordering requests. 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="GridState"></param>
	/// <param name="ColumnOrdering"></param>
	private record NaturalLanguageGridState<T>(GridState<T> GridState,
		ColumnOrdering ColumnOrdering);

	/// <summary>
	/// Columns ordered from left to right.
	/// </summary>
	/// <param name="Columns"></param>
	public record ColumnOrdering(string[] Columns);


}