using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Common.Helpers
{
    public class DataModelHelper
    {
        public static T CreateObject<T>(DataTable dataTable)
        {
            try
            {
                Type ItemType = typeof(T);
                T TempObject = (T)Activator.CreateInstance<T>();

                for (int i = 0; i < dataTable.Columns.Count - 1; i++)
                {
                    string colName = Convert.ToString(dataTable.Columns[i]);
                    PropertyInfo prop = ItemType.GetProperty(colName);

                    if (prop == null)
                    {
                        continue;
                    }
                    prop.SetValue(TempObject, Convert.ChangeType(dataTable.Rows[0][colName], prop.PropertyType), null);
                }

                return TempObject;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static T CreateObject<T>(IDataReader dataRender)
        {
            dataRender.Read();

            var fieldNameLists = new List<string>();
            for (int i = 0; i < dataRender.FieldCount; i++)
            {
                fieldNameLists.Add(dataRender.GetName(i).ToString());
            }

            T Item = (T)Activator.CreateInstance<T>();
            foreach (PropertyInfo propertyInfo in Item.GetType().GetProperties())
            {
                if (fieldNameLists.Contains(propertyInfo.Name))
                {
                    var value = dataRender[propertyInfo.Name];
                    if (value.GetType().Name == "DBNull")
                    {
                        propertyInfo.SetValue(Item, null, null);
                    }
                    else
                    {
                        propertyInfo.SetValue(Item, value);
                    }
                }

            }

            return Item;
        }

        public static List<T> CreateList<T>(DataTable dataTable)
        {
            try
            {
                Type ItemType = typeof(T);
                List<T> ObjectList = new List<T>();

                for (int i = 0; i < dataTable.Rows.Count - 1; i++)
                {
                    T TempObject = (T)Activator.CreateInstance<T>();
                    for (int j = 0; j < dataTable.Columns.Count - 1; j++)
                    {
                        string colName = Convert.ToString(dataTable.Columns[j]);
                        PropertyInfo prop = ItemType.GetProperty(colName);

                        if (prop == null)
                        {
                            continue;
                        }

                        prop.SetValue(TempObject, Convert.ChangeType(dataTable.Rows[i][colName], prop.PropertyType), null);
                    }
                    ObjectList.Add(TempObject);
                }

                return ObjectList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<T> CreateList<T>(IDataReader dataReader)
        {
            List<T> list = new List<T>();

            var fieldNameLists = new List<string>();
            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                fieldNameLists.Add(dataReader.GetName(i).ToString());
            }

            while (dataReader.Read())
            {
                T item = Activator.CreateInstance<T>();
                foreach (PropertyInfo propertyInfo in item.GetType().GetProperties())
                {
                    if (fieldNameLists.Contains(propertyInfo.Name))
                    {
                        var value = dataReader[propertyInfo.Name];
                        if (value.GetType().Name == "DBNull")
                        {
                            propertyInfo.SetValue(item, null, null);
                        }
                        else
                        {
                            propertyInfo.SetValue(item, value);
                        }
                    }
                }
                list.Add(item);
            }

            return list;
        }
    }
}
