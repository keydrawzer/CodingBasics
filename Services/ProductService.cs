using Microsoft.Data.SqlClient;

public class ProductService
{
    private DataClient _connection;

    public ProductService(DataClient connection)
    {
        _connection = connection;
    }

    public async Task<IResult> GetAll()
    {
        string sqlQuery = "SELECT * FROM [Production].[vProductAndDescription]";

        List<ProductsModel> result = await _connection.GetResultsFromQuery<ProductsModel>(sqlQuery);


        return TypedResults.Ok(result);
    }

    public async Task<IResult> GetProductByName(string name)
    {
        SqlParameter[] queryParameters =
        {
            new("@NameSearch", "%" + name + "%")
        };

        string sqlQuery =
            "SELECT * FROM [Production].[vProductAndDescription] " +
            "WHERE Name LIKE @NameSearch " +
            "ORDER BY ProductID";

        List<ProductsModel> result = await _connection.GetResultsFromQuery<ProductsModel>(sqlQuery, queryParameters);


        return TypedResults.Ok(result);
    }

    public async Task<IResult> GetProductsByCategoryType(string categoryType)
    {

        SqlParameter[] queryParameters =
        {
            new("@CategoryType", categoryType)
        };

        string sqlQuery =
            "SELECT a.* FROM [Production].[vProductAndDescription] a " +
                "INNER JOIN Production.Product b " +
                "ON a.ProductID = b.ProductID " +
                "INNER JOIN Production.ProductSubcategory c " +
                "ON b.ProductSubcategoryID = c.ProductSubcategoryID " +
                "INNER JOIN Production.ProductCategory d " +
                "ON c.ProductCategoryID = d.ProductCategoryID " +
            "WHERE d.name = @CategoryType " +
            "ORDER BY a.ProductID";

        List<ProductsModel> result = await _connection.GetResultsFromQuery<ProductsModel>(sqlQuery, queryParameters);

        return TypedResults.Ok(result);
    }

    public async Task<IResult> GetByCultureID(string cultureID)
    {
        bool isCultureIDIncluded = Constants.AvailableCultureIDs.Contains(cultureID);
        string availableCultureID = isCultureIDIncluded ? cultureID : Constants.DefaultCultureID;

        SqlParameter[] queryParameters =
        {
            new("@CultureID", availableCultureID)
        };

        string sqlQuery =
            "SELECT * FROM [Production].[vProductAndDescription] " +
            "WHERE CultureID = @CultureID " +
            "ORDER BY ProductID";

        List<ProductsModel> result = await _connection.GetResultsFromQuery<ProductsModel>(sqlQuery, queryParameters);

        return TypedResults.Ok(result);
    }

    public async Task<IResult> GetByNameCultureAndCategoryType(string name, string cultureID, string categoryType)
    {
        bool isCultureIDIncluded = Constants.AvailableCultureIDs.Contains(cultureID);
        string availableCultureID = isCultureIDIncluded ? cultureID : Constants.DefaultCultureID;


        SqlParameter[] queryParameters =
        {   new("@Name", name),
            new("@NameSearch", "%" + name + "%"),
            new("@CultureID", availableCultureID),
            new("@CategoryType", categoryType)
        };

        string sqlQuery =
            "SELECT a.* FROM [Production].[vProductAndDescription] a " +
                "INNER JOIN Production.Product b " +
                "ON a.ProductID = b.ProductID " +
                "INNER JOIN Production.ProductSubcategory c " +
                "ON b.ProductSubcategoryID = c.ProductSubcategoryID " +
                "INNER JOIN Production.ProductCategory d " +
                "ON c.ProductCategoryID = d.ProductCategoryID " +
            "WHERE (@Name ='' OR @Name IS NULL OR a.Name LIKE @NameSearch) " +
                "AND " +
                "a.CultureID = @CultureID " +
                "AND " +
                "(@CategoryType ='' OR @CategoryType IS NULL OR d.name = @CategoryType) " +
            "ORDER BY a.ProductID";

        List<ProductsModel> result = await _connection.GetResultsFromQuery<ProductsModel>(sqlQuery, queryParameters);

        return TypedResults.Ok(result);
    }
}
