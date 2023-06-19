using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTechPharma.Auxiliares
{
    static class AuxiliarDAO
    {
        public static string GetString(MySqlDataReader reader, string column_name)
        {
            string text = string.Empty;

            if (!reader.IsDBNull(reader.GetOrdinal(column_name)))
                text = reader.GetString(column_name);

            return text;
        }

        public static int GetInt(MySqlDataReader reader, string column_name)
        {
            int value = 0;

            if (!reader.IsDBNull(reader.GetOrdinal(column_name)))
                value = reader.GetInt32(column_name);

            return value;
        }

        public static double GetDouble(MySqlDataReader reader, string column_name)
        {
            double value = 0.0;

            if (!reader.IsDBNull(reader.GetOrdinal(column_name)))
                value = reader.GetDouble(column_name);

            return value;
        }

        public static float GetFloat(MySqlDataReader reader, string column_name)
        {
            float value = 0.0f;

            if (!reader.IsDBNull(reader.GetOrdinal(column_name)))
                value = reader.GetFloat(column_name);

            return value;
        }

        public static DateTime? GetDateTime(MySqlDataReader reader, string column_name)
        {
            DateTime? value = null;

            if (!reader.IsDBNull(reader.GetOrdinal(column_name)))
                value = reader.GetDateTime(column_name);

            return value;
        }

        public static bool IsNull(MySqlDataReader reader, string column_name)
        {
            return reader.IsDBNull(reader.GetOrdinal(column_name));
        }
    }
}
