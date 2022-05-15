using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Tests
{
    public class Consts
    {
        //public static int CategoryId=2;
        public const string ProductName = "TestProduct name 1";
        public static int TestProduct = 3;
        public static int TestSupplierID = 4;
        public static decimal Unitprice = 100.23M;
        public enum Categories
        {
            Condiments = 2,
            Confections=3,
            Dairy=4,
            Grains=5,
            Meat=6
        }
    }
}

