You need create a new view from database AdventureWorks

CREATE VIEW Sales.vSalesReview 
AS
SELECT Sales.vSalesPersonSalesByFiscalYears.SalesPersonID, Sales.vSalesPersonSalesByFiscalYears.FullName, Sales.vSalesPersonSalesByFiscalYears.JobTitle, Sales.vSalesPersonSalesByFiscalYears.SalesTerritory, 
Sales.vSalesPerson.TerritoryName, Sales.vSalesPerson.TerritoryGroup, Sales.vSalesPerson.SalesQuota, Sales.vSalesPerson.SalesYTD, Sales.vSalesPerson.SalesLastYear, YEAR(Sales.SalesPersonQuotaHistory.QuotaDate) as SelectDate
FROM Sales.SalesPersonQuotaHistory INNER JOIN
Sales.vSalesPerson ON Sales.SalesPersonQuotaHistory.BusinessEntityID = Sales.vSalesPerson.BusinessEntityID CROSS JOIN
Sales.vSalesPersonSalesByFiscalYears where TerritoryName is NOT null
