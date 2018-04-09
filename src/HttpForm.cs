using System;
using System.Collections;
using System.Collections.Generic;

public class HttpForm
{
    /// <summary>
    /// Holds the fields data
    /// </summary>
    Dictionary<string, object> fields = new Dictionary<string, object>();

    /// <summary>
    /// Adds a new field with value of type string
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="value"></param>
    public void AddField(string fieldName, string value)
    {
        fields.Add(fieldName, value);
    }

    /// <summary>
    /// Adds a new field with value of type integer
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="value"></param>
    public void AddField(string fieldName, int value)
    {
        fields.Add(fieldName, value);
    }

    /// <summary>
    /// Returns the formated "Raw Post" string 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        string str = String.Empty;
        foreach (KeyValuePair<string, object> field in fields)
        {
            str += string.Format("{0}={1}&", field.Key, field.Value);
        }

        //Ends in & ? lets remove it
        if (str.Substring(str.Length - 1) == "&")
            str.Remove(str.Length - 1);

        return str;
    }
}