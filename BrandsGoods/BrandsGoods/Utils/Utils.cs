using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace BrandsGoods.Utils
{
    /// <summary>
    /// Classe de utilidades de impressao
    /// </summary>
    public static class UPrint
    {
        /// <summary>
        /// Imprimir Datatable na Console
        /// </summary>
        /// <param name="dt"></param>
        public static void DataTableToConsole(DataTable dt)
        {
            
            int espaco = 5;

            Console.WriteLine(dt.TableName);
            Console.WriteLine();

            Dictionary<string, int> colWidths = new Dictionary<string, int>();

            foreach (DataColumn col in dt.Columns)
            {
                Console.Write(col.ColumnName);
                var maxLabelSize = dt.Rows.OfType<DataRow>()
                        .Select(m => (m.Field<object>(col.ColumnName)?.ToString() ?? "").Length)
                        .OrderByDescending(m => m).FirstOrDefault();

                colWidths.Add(col.ColumnName, maxLabelSize);
                for (int i = 0; i < maxLabelSize - col.ColumnName.Length + espaco; i++) Console.Write(" ");
            }

            Console.WriteLine();

            foreach (DataRow dataRow in dt.Rows)
            {
                for (int j = 0; j < dataRow.ItemArray.Length; j++)
                {
                    Console.Write(dataRow.ItemArray[j]);
                    for (int i = 0; i < colWidths[dt.Columns[j].ColumnName] - dataRow.ItemArray[j].ToString().Length + espaco; i++) Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
    }
    /// <summary>
    /// Classe de utilidades de tratamento de texto
    /// </summary>
    static class UText
    {
        /// <summary>
        /// Mascarar senha
        /// </summary>
        /// <param name="password"></param>
        public static void Mask(out string password)
        {
            string pass = string.Empty;

            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);

            password = pass;
        }

        /// <summary>
        /// Arrumar caracteres a esquerda
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string Left(string value, int maxLength)
        {
            maxLength = Math.Abs(maxLength);
            value = value + "" + string.Concat(Enumerable.Repeat(" ", maxLength));

            return value.Substring(0, maxLength - 3) + "   ";
        }

    }

    /// <summary>
    /// Classe de conversao de dados
    /// </summary>
    static class UConverter
    {
        /// <summary>
        /// Converter Json para DataTable
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static DataTable JsonToDataTable(string filename)
        {
            // Caminho do Arquivo
            string path = Path.GetFullPath(@"..\..\..\");
            string pathFull = Path.Combine(path, filename);

            // Leitura e conversão para texto
            StreamReader sr = new StreamReader(pathFull);
            string jsonString = sr.ReadToEnd();

            return (DataTable)JsonConvert.DeserializeObject(jsonString, (typeof(DataTable)));
        }
    }
}

