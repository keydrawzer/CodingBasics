using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc;

public class PersonService
{
    private readonly DataClient _connection;

    public PersonService(DataClient connection)
    {
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
    }

    public List<PersonModel>? GetAll()
    {
        try
        {
            string query = "SELECT * FROM [HumanResources].[vEmployee]";
            var result = _connection.GetResultsFromQuery<PersonModel>(query, Map);
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving all persons: {ex.Message}");
        }
        return null;
    }

    public List<PersonModel>? GetPersonByName(string name)
    {
        try
        {
            string query = $"SELECT * FROM [AdventureWorks2019].[HumanResources].[vEmployee] " +
                           $"WHERE CONCAT(FirstName, ' ', MiddleName, ' ', LastName) LIKE '%{name}%'";
            var result = _connection.GetResultsFromQuery<PersonModel>(query, Map);
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving persons by name: {ex.Message}");
        }
        return null;
    }

    public List<PersonModel>? GetPersonByPersonType(string personType)
    {
        try
        {
            string query = $"SELECT A.* FROM [AdventureWorks2019].[HumanResources].[vEmployee] A " +
                           "INNER JOIN Person.Person B ON A.BusinessEntityID = B.BusinessEntityID " +
                           $"WHERE B.PersonType = '{personType}'";
            var result = _connection.GetResultsFromQuery<PersonModel>(query, Map);
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving persons by person type: {ex.Message}");
        }
        return null;
    }

    public List<PersonModel>? GetPersonByNameAndPersonType(string name, string personType)
    {
        try
        {
            string query = $"SELECT * FROM [AdventureWorks2019].[HumanResources].[vEmployee] A " +
                           $"INNER JOIN Person.Person B ON A.BusinessEntityID = B.BusinessEntityID " +
                           $"WHERE " +
                           $"    ('{name}' ='' OR '{name}' IS NULL OR CONCAT(A.FirstName, ' ', A.MiddleName, ' ', A.LastName) LIKE '%{name}%') " +
                           $"    AND " +
                           $"    ('{personType}' = '' OR '{personType}' IS NULL OR B.PersonType = '{personType}')";
            var result = _connection.GetResultsFromQuery<PersonModel>(query, Map);
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving persons by name and person type: {ex.Message}");
        }
        return null;
    }

    public PersonModel Map(IDataRecord record)
    {
        return new PersonModel
        {
            BusinessEntityID = (int)record["BusinessEntityID"],
            Title = record["Title"] as string,
            FirstName = record["FirstName"] as string,
            MiddleName = record["MiddleName"] as string,
            LastName = record["LastName"] as string,
            Suffix = record["Suffix"] as string,
            JobTitle = record["JobTitle"] as string,
            PhoneNumber = record["PhoneNumber"] as string,
            PhoneNumberType = record["PhoneNumberType"] as string,
            EmailAddress = record["EmailAddress"] as string,
            EmailPromotion = (int)record["EmailPromotion"],
            AddressLine1 = record["AddressLine1"] as string,
            AddressLine2 = record["AddressLine2"] as string,  // Corregido el nombre del campo
            City = record["City"] as string,
            StateProvinceName = record["StateProvinceName"] as string,
            PostalCode = record["PostalCode"] as string,
            CountryRegionName = record["CountryRegionName"] as string,
            AdditionalContactInfo = record["AdditionalContactInfo"] as string
        };
    }
}
