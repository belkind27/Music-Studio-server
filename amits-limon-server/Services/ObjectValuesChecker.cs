using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace amits_limon_server.Services
{
    public class ObjectValuesChecker
    {
        public bool IsObjectComplete(object obj, bool isAdd = false)
        {
            foreach (PropertyInfo props in obj.GetType().GetProperties())
            {
                if (props.PropertyType == typeof(string))
                {
                    string value = (string)props.GetValue(obj);
                    if (string.IsNullOrEmpty(value))
                    {
                        return false;
                    }
                }
                if (props.PropertyType == typeof(int))
                {
                    int value = (int)props.GetValue(obj);
                    if (value <= 0 && !isAdd)
                    {
                        return false;
                    }
                }
                if (props.PropertyType == typeof(DateTime))
                {
                    DateTime value = (DateTime)props.GetValue(obj);
                    if (value == default)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
