using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelerikAIExtensions.NaturalLanguage
{
	public partial class NaturalLanguageGridService
	{
		private const string example = """
			   {
			  "groupDescriptors": [],
			  "collapsedGroups": [],
			  "columnStates": [
			    {
			      "index": 0,
			      "width": null,
			      "visible": null,
			      "locked": false,
			      "id": null,
			      "field": "exampleColumn"
			    }
			  ],
			  "expandedItems": [],
			  "filterDescriptors": [
				{
				"logicalOperator":0,
				"filterDescriptors":[{"member":"exampleColumn","operator":6,"value":"sample"}]}],
				"sortDescriptors": [{
					"field": "exampleColumn",
					"sortDirection": 0
				}],
			  "searchFilter": null,
			  "page": 5,
			  "skip": 40,
			  "selectedItems": [],
			  "originalEditItem": null,
			  "editItem": null,
			  "editField": null,
			  "insertedItem": null,
			  "tableWidth": null
			}
			""";
	}
}
