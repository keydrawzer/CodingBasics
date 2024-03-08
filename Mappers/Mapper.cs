using System.Reflection;
using System.Data;

public static class Mapper
{
    public static T Map<T>(IDataRecord reader) where T : new()
    {
        var resultObject = new T();
        Type resultObjectType = typeof(T);

        for (int i = 0; i < reader.FieldCount; i++)
        {
            string columnName = reader.GetName(i);
            PropertyInfo? resultObjectProperty = resultObjectType.GetProperty(columnName);

            if (resultObjectProperty != null)
            {
                object? columnValue = reader[columnName];
                if (columnValue != DBNull.Value)
                {
                    resultObjectProperty.SetValue(resultObject, columnValue);
                }
            }
        }

        return resultObject;
    }
}