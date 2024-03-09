interface BaseSalesService
{
    List<SaleModel>? GetOverviewByPersons();
    List<SaleModel>? GetSalesByPersonAndYear(string person, int? year);
}
