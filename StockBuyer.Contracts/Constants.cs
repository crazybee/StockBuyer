using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBuyer.Contracts
{
    public class Constants
    {
        // in reality this will be consolidated in db or keyVault 
        public static string TokenSecret = "3q4t548o0gubv890u90423j09asdfon??>;,l;,;'";

        public static readonly List<string> UserList = new List<string>()
            {
                "Liu",
                "Hakan",
                "Ahmad",
                "Laurent"
            };

        public static readonly List<string> MockedCompanyList = new List<string>()
            {
                "Apple",
                "Microsoft",
                "Dell",
                "Amazon"
            };
    }
}
