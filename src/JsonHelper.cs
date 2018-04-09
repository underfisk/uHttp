using System.Collections;
using System.Collections.Generic;

public class JsonHelper 
{

    /// <summary>
    /// Returns the result of checking the string given
    /// </summary>
    /// <param name="js"></param>
    /// <returns></returns>
    public static bool isJson(string js)
    {
        //we just check if starts with { and ends with }
        if (string.IsNullOrEmpty(js)) return false;

        var c = js.Trim();
        if ((c.StartsWith("{") && c.EndsWith("}")) || //For object
        (c.StartsWith("[") && c.EndsWith("]"))) //For array
            return true;

        return false;//false by default
    }
}
