using System;
using System.Collections.Generic;
using System.Text;

namespace LinqProject
{
    class Product
    {
        
            public int ProductId { get; set; }
            public int CategoryId { get; set; } //Burda bir kotegory belirlediğimizde ismini değilde Id sini kullanırız çünkü daha spesifik.
            public string ProductName { get; set; }
            public string QuantityPerUnit { get; set; }
            public decimal UnitPrice { get; set; }
            public int UnitsInStock { get; set; }
       
    }
}
