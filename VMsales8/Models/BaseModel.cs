using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMSales8.Logic;

namespace VMSales8.Models
{
    public class BaseModel : PropertyChangedBase
    {
        public string? Action { get; set; }  // This will hold Insert, Update, Delete actions

        protected static string SetDataBase()
        {

            // See app.config for database setting
            string? filePath = ConfigurationManager.AppSettings["DatabaseFilePath"];

            // if doesn't exist or not set
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                //test
                filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testsales2.db");
                //prod
                //filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sales.db");
            }

            // Check if the file exists, and throw an exception if it doesn't
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Database file not found.", filePath);
            }

            return filePath;
        }

        public static IDatabaseProvider getprovider()
        {
            string filepath = SetDataBase();
            SQLiteDatabase dataBaseProvider = new SQLiteDatabase(filepath);
            return dataBaseProvider;
        }
    }
}
