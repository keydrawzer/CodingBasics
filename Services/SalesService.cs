using System.Data;

public class SalesService
{
    private DataClient _connection;
    public SalesService(DataClient connection){
        _connection = connection;
    }
    public List<SalesModel>? GetAll(){
        try{
            var result = _connection.GetResultsFromQuery<SalesModel>("SELECT * FROM [Sales].[vSalesPerson]", Map);           
            return result;
        }catch (Exception ex){
            Console.WriteLine($"JustError: {ex.Message}");
        }
        return null;
    }

    public List<SalesModel>? GetSales()
    {
        return null;
    }

    public SalesModel Map(IDataRecord record){
        SalesModel sales = new SalesModel();
            sales.BusinessEntityId = (int)record["BusinessEntityId"];
            sales.Title = record["Title"] as string;
            sales.FirstName = record["FirstName"] as string;
            sales.MiddleName = record["MiddleName"] as string;
            sales.LastName = record["LastName"] as string;
            sales.Suffix = record["Suffix"] as string;
            sales.JobTitle = record["JobTitle"] as string;
            sales.PhoneNumber = record["PhoneNumber"] as string;
            sales.PhoneNumberType = record["PhoneNumberType"] as string;
            sales.EmailAddress = record["EmailAddress"] as string;
            sales.EmailPromotion = (int)record["EmailPromotion"];
            sales.AddressLine1 = record["AddressLine1"] as string;
            sales.AddressLine2 = record["AddressLine2"] as string;
            sales.City = record["City"] as string;
            sales.StateProvinceName = record["StateProvinceName"] as string;
            sales.PostalCode = record["PostalCode"] as string;
            sales.CountryRegionName = record["CountryRegionName"] as string;
            sales.TerritoryName = record["TerritoryName"] as string;
            sales.TerritoryGroup = record["TerritoryGroup"] as string;
            sales.SalesQuota = record["SalesQuota"] as decimal? ;
            sales.SalesYTD = record["SalesYTD"] as decimal?;
            sales.SalesLastYear = record["SalesLastYear"] as decimal?;

        return sales;
            
    }
}