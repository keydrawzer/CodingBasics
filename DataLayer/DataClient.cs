using Microsoft.Data.SqlClient;


public class DataClient
{
    private SqlConnection connection;

    public DataClient(IConfiguration configuration)
    {
        connection = new SqlConnection(configuration.GetConnectionString("localServer"));
    }

    public bool TestConnection()
    {
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT 1 FROM Person.Person", connection);
            var result = command.ExecuteNonQuery();
            connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error:{ex.Message}");
            return false;
        }
        return true;
    }

    public async Task<List<T>> GetResultsFromQuery<T>(string query, SqlParameter[]? sqlParameters = null) where T : new()
    {
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);

            if (sqlParameters != null && sqlParameters.Length > 0)
            {
                command.Parameters.AddRange(sqlParameters);
            }

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            List<T> results = new List<T>();

            while (reader.Read())
            {
                var item = Mapper.Map<T>(reader);
                results.Add(item);
            }
            return results;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error:{ex.Message}");
            throw;
        }
        finally
        {
            connection.Close();
        }
    }
}