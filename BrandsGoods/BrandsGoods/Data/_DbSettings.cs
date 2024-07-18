using System.IO;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace BrandsGoods.Data
{
    static class DbSettings
    {
        public static string ConnectionString = GetConnectionString();
        private static string GetConnectionString()
        {
            string strJson = "";

            // Caminho do Arquivo
            string path = Path.GetFullPath(@"..\..\..\") + "appsettings.json";

            try
            {
                // read file and get JSON value to a string;
                JObject jo = JObject.Parse(File.ReadAllText(path));
                strJson = (string)jo["ConnectionStrings"];
            }
            catch
            {
                strJson = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=BrandsGoodsDB;Trusted_Connection=true;";
            }

            return strJson;
        }
    }
}
