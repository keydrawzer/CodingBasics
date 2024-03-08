using Microsoft.Data.SqlClient;

public class PersonService
{
    private DataClient _connection;

    public PersonService(DataClient connection)
    {
        _connection = connection;
    }

    public async Task<IResult> GetAll()
    {
        string sqlQuery =
            "SELECT * FROM [HumanResources].[vEmployee] " +
            "ORDER BY BusinessEntityID";

        var result = await _connection.GetResultsFromQuery<PersonModel>(sqlQuery);

        return TypedResults.Ok(result);
    }

    public async Task<IResult> GetPersonByName(string name)
    {
        SqlParameter[] queryParameters =
        {
            new("@NameSearch", "%" + name + "%")
        };

        string sqlQuery =
            "SELECT * " +
            "FROM [AdventureWorks2022].[HumanResources].[vEmployee] " +
            "WHERE CONCAT(FirstName,' ',MiddleName,' ',LastName) LIKE @NameSearch " +
            "ORDER BY BusinessEntityID";

        var result = await _connection.GetResultsFromQuery<PersonModel>(sqlQuery, queryParameters);

        return TypedResults.Ok(result);
    }

    public async Task<IResult> GetPersonByPersonType(string personType)
    {
        SqlParameter[] queryParameters =
        {
            new("@PersonType", personType)
        };

        string sqlQuery =
            "SELECT A.* " +
            "FROM [AdventureWorks2022].[HumanResources].[vEmployee] A " +
                "INNER JOIN Person.Person B " +
                "ON a.BusinessEntityID = b.BusinessEntityID " +
            "WHERE B.PersonType = @PersonType " +
            "ORDER BY A.BusinessEntityID";

        var result = await _connection.GetResultsFromQuery<PersonModel>(sqlQuery, queryParameters);

        return TypedResults.Ok(result);
    }

    public async Task<IResult> GetPersonByNameAndPersonType(string name, string personType)
    {
        SqlParameter[] queryParameters =
      {
            new("@Name", name),
            new("@NameSearch", "%"+ name+ "%"),
            new("@PersonType", personType)
        };

        string sqlQuery =
            "SELECT * " +
            "FROM [AdventureWorks2022].[HumanResources].[vEmployee] A " +
                "INNER JOIN Person.Person B " +
                "ON A.BusinessEntityID = B.BusinessEntityID " +
            "WHERE " +
                "(@Name ='' OR @Name IS NULL OR CONCAT(A.FirstName, ' ', A.MiddleName, ' ', A.LastName) LIKE @NameSearch) " +
                "AND " +
                "(@PersonType = '' OR @PersonType IS NULL OR B.PersonType = @PersonType) " +
            "ORDER BY A.BusinessEntityID";

        var result = await _connection.GetResultsFromQuery<PersonModel>(sqlQuery, queryParameters);

        return TypedResults.Ok(result);
    }
}
