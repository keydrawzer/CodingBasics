using System;
using System.Collections.Generic;

namespace CodingBasics.Models;
public partial class SalesByYear
{
    public int SalesPersonId { get; set; }

    public string Name { get; set; } = null!;

    public int Year { get; set; }

    public int SalesOrderId { get; set; }

    public decimal? TotalDue { get; set; }
}