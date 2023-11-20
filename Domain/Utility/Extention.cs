using System.Data;

namespace Domain.Utility
{
    public static class Extention
    {
        public static DataTable ToDataTable<T>(this List<T> list)
        {
            DataTable table = new DataTable();

            if (list.Count > 0)
            {
                var properties = typeof(T).GetProperties();

                foreach (var property in properties)
                {
                    table.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
                }

                foreach (var item in list)
                {
                    DataRow row = table.NewRow();
                    foreach (var property in properties)
                    {
                        row[property.Name] = property.GetValue(item) ?? DBNull.Value;
                    }
                    table.Rows.Add(row);
                }
            }

            return table;
        }
    }
}
