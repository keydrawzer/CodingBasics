using System.Data;
using Microsoft.AspNetCore.Mvc;

public class SalePersonsService
{
    private DataClient _connection;
    public SalePersonsService(DataClient connection){
        _connection = connection;}
        
       public List<SalePersonsModel>? GetSalesByPersonAndYear(string name,int year){
        try{
            var result = _connection.GetResultsFromQuery<SalePersonsModel>(
                "select " +
                "person.BusinessEntityID BusinessEntityID," +
                "person.Title Title," +
                "person.FirstName FirstName," +
                "person.MiddleName MiddleName," +
                "person.LastName LastName," +
                "person.Suffix Suffix," +
                "person.JobTitle JobTitle," +
                "a.SalesOrderID, a.OrderDate, a.CustomerID, a.TotalDue" +
                "from sales.SalesOrderHeader a" +
                "inner join HumanResources.vEmployee person on a.SalesPersonID = person.BusinessEntityID" +
                $"where CONCAT(person.FirstName,' ',person.MiddleName,' ',person.LastName) LIKE '%{name}%' and year(OrderDate) = {year}", Map);

            return result;
        }catch (Exception ex){
            Console.WriteLine($"Error message: {ex.Message}");
        }
        return null;
    }
        
        public SalePersonsModel Map(IDataRecord record)
        {
        SalePersonsModel Sales = new SalePersonsModel();
            Sales.BusinessEntityID = (int)record["BusinessEntityID"];
            Sales.Title = record["Title"] as string;
            Sales.FirstName = record["FirstName"] as string;
            Sales.MiddleName = record["MiddleName"] as string;
            Sales.LastName = record["LastName"] as string;
            Sales.Suffix = record["Suffix"] as string;
            Sales.JobTitle = record["JobTitle"] as string;
            Sales.SalesOrderID = record["salesorderID"] as string;
            Sales.OrderDate = record["OrderDate"] as string;
            Sales.CustomerID = record ["CustomerID"] as string;
            Sales.TotalDue = record ["TotalDue"] as string;
            return Sales;
        
        
        
        
        
        
        
        }}

