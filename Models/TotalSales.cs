using System;
using System.Collections.Generic;

namespace CodingBasics.Models;

public partial class TotalSales
{
    public int SalesPersonId { get; set; }

    public string PersonName { get; set; } = null!;

    public decimal? Total { get; set; }
}

