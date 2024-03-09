using System.Collections;
using System.Data;
using System.Reflection;
using Microsoft.Data.SqlClient;


public class DataClient 
{
    private SqlConnection connection;

    public DataClient(IConfiguration configuration){
        connection = new SqlConnection(configuration.GetConnectionString("localServer"));
    }
    public bool TestConnection(){

        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT 1 FROM Person.Person",connection);
            var result = command.ExecuteNonQuery();            
            connection.Close();
        }catch(Exception ex)
        {
            Console.WriteLine($"Error:{ex.Message}");
            return false;
        }
        return true;
    }

    //Metodo generico
    public List<T>? GetResultsFromQuery<T>(string query, Func<IDataRecord,T> parseMethod){
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand(query,connection);
            using SqlDataReader reader = command.ExecuteReader();           
            List<T> results  = new List<T>();

            while (reader.Read())
            {
               // Map data to instances of T (you need to implement this)
                var item = parseMethod(reader);
                results.Add(item);
            }
            connection.Close();
            return results;
        }catch(Exception ex)
        {
            Console.WriteLine($"Error:{ex.Message}");
            return null;
        }
    }
}