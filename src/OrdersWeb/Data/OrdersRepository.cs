using Microsoft.Data.SqlClient;

namespace OrdersWeb.Data;

public sealed class OrdersRepository
{
    private readonly string _connStr = "Server=(localdb)\\MSSQLLocalDB;Database=Orders;Trusted_Connection=True;";

    // VULNERABLE: concatenation into SQL command text
    public async Task<List<(int Id, string Customer)>> SearchOrdersVulnerable(string customer)
    {
        var results = new List<(int, string)>();

        await using var conn = new SqlConnection(_connStr);
        await conn.OpenAsync();

        var sql = "SELECT Id, Customer FROM Orders WHERE Customer = '" + customer + "'";
        await using var cmd = new SqlCommand(sql, conn);

        await using var rdr = await cmd.ExecuteReaderAsync();
        while (await rdr.ReadAsync())
            results.Add((rdr.GetInt32(0), rdr.GetString(1)));

        return results;
    }

    // SAFE: parameterized query
    public async Task<List<(int Id, string Customer)>> SearchOrdersSafe(string customer)
    {
        var results = new List<(int, string)>();

        await using var conn = new SqlConnection(_connStr);
        await conn.OpenAsync();

        await using var cmd = new SqlCommand(
            "SELECT Id, Customer FROM Orders WHERE Customer = @customer", conn);
        cmd.Parameters.AddWithValue("@customer", customer);

        await using var rdr = await cmd.ExecuteReaderAsync();
        while (await rdr.ReadAsync())
            results.Add((rdr.GetInt32(0), rdr.GetString(1)));

        return results;
    }
}
