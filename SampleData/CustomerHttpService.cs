using SampleData;
using System.Net.Http.Json;

public class CustomerService(HttpClient http)
{
	private IEnumerable<CustomerDto>? cache;
	public async Task<IEnumerable<CustomerDto>> GetCustomers() =>
		cache ??= (await http.GetFromJsonAsync<IEnumerable<CustomerDto>>("https://demos.telerik.com/blazor-ui-service/api/Customer/GetCustomers") ?? [])
				 .Select((c, i) => { c.Number = i; return c; });
}