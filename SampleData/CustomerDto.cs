using System.ComponentModel.DataAnnotations;

namespace SampleData;

public class CustomerDto
{
	[StringLength(5)]
	public string CustomerId { get; set; } = string.Empty;

	[Required]
	[StringLength(40)]
	public string CompanyName { get; set; } = string.Empty;

	[StringLength(30)]
	public string ContactName { get; set; } = string.Empty;

	[StringLength(30)]
	public string ContactTitle { get; set; } = string.Empty;

	[StringLength(60)]
	public string Address { get; set; } = string.Empty;

	[StringLength(15)]
	public string City { get; set; } = string.Empty;

	[StringLength(15)]
	public string Region { get; set; } = string.Empty;

	[StringLength(10)]
	public string PostalCode { get; set; } = string.Empty;

	[StringLength(15)]
	public string Country { get; set; } = string.Empty;

	[StringLength(24)]
	public string Phone { get; set; } = string.Empty;

	[StringLength(24)]
	public string Fax { get; set; } = string.Empty;

	public bool? Bool { get; set; }

	public int? Number { get; set; }
}
