namespace TelerikAIExtensions.NaturalLanguage;

public partial class NaturalLanguageGridService
{
	private const string jsonSchema = """ 
{
    "$schema": "http://json-schema.org/draft-07/schema#",
    "type": "object",
    "properties": {
"gridState": {
"type": "object",
"properties": {
      "GroupDescriptors": {
        "type": "array",
        "items": {
          "type": "object",
          "properties": {
            "DisplayContent": { "type": "string" },
            "AggregateFunctions": { "type": "array", "items": { "type": "string" } },
            "Member": { "type": "string" },
            "SortDirection": { "type": "integer" }
          },
          "required": ["DisplayContent", "AggregateFunctions", "Member", "SortDirection"],
          "additionalProperties": false
        }
      },
      "CollapsedGroups": {
        "type": "array",
        "items": { "type": "string" }
      },
      "ColumnStates": {
        "type": "array",
        "items": {
          "type": "object",
          "properties": {
            "Index": { "type": "integer" },
            "Width": { "type": ["number", "null"] },
            "Visible": { "type": ["boolean", "null"] },
            "Locked": { "type": "boolean" },
            "Id": { "type": ["string", "null"] },
            "Field": { "type": "string" }
          },
          "required": ["Index", "Width", "Visible", "Locked", "Id", "Field"],
          "additionalProperties": false
		}
      },
      "ExpandedItems": {
        "type": "array",
        "items": { "type": "string" }
      },
      "FilterDescriptors": {
        "type": "array",
        "items": {
          "type": "object",
          "properties": {
            "LogicalOperator": { "type": "integer" },
            "FilterDescriptors": {
              "type": "array",
              "items": {
                "type": "object",
                "properties": {
                  "Member": { "type": "string" },
                  "Operator": { "type": "integer" },
                  "Value": { "type": ["number", "string", "null", "boolean"] }
                },
                "required": ["Member", "Operator", "Value"],
                "additionalProperties": false
              }
            }
          },
          "required": ["LogicalOperator", "FilterDescriptors"],
          "additionalProperties": false
        }
      },
      "SortDescriptors": {
        "type": "array",
        "items": {
          "type": "object",
          "properties": {
            "Member": { "type": "string" },
            "SortDirection": { "type": "integer" }
          },
          "required": ["Member", "SortDirection"],
          "additionalProperties": false
        }
      },
      "SearchFilter": {
        "type": ["string", "null"]
      },
      "Page": {
        "type": "integer"
      },
      "Skip": {
        "type": "integer"
      },
      "SelectedItems": {
        "type": "array",
        "items": { "type": "string" }
      },
      "OriginalEditItem": {
        "type": ["string", "null"]
      },
      "EditItem": {
        "type": ["string", "null"]
      },
      "EditField": {
        "type": ["string", "null"]
      },
      "InsertedItem": {
        "type": ["string", "null"]
      },
      "TableWidth": {
        "type": ["number", "null"]
      }
    },
    "required": ["GroupDescriptors", "CollapsedGroups", "ColumnStates", "ExpandedItems", "FilterDescriptors", "SortDescriptors", "Page", "Skip", "SelectedItems", "OriginalEditItem", "EditItem", "EditField", "InsertedItem", "TableWidth"],
    "additionalProperties": false
  },
  "columnOrdering": {
    "type": "object",
    "properties": {
      "columns": {
        "type": "array",
        "items": { "type": "string" }
      }
    },
    "required": ["columns"]
  }
},
"required": ["gridState", "columnOrdering"]
  }
""";

}
