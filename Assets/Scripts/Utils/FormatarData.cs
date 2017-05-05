using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormatarData : MonoBehaviour {

    public static string FormatToString(int i)
    {
        try
        {
            string s = i.ToString();
            string ano = s.Substring(0, 4);
            string mes = s.Substring(4, 2);
            string dia = s.Substring(6, 2);
            s = dia + "/" + mes + "/" + ano;

            return s;
        }
        catch{
            return "0";
        }
        
        
    }

    public static int FormatToInt(string s)
    {
        string[] data = s.Split('/');
        s = data[2] + data[1]+data[0];
        return int.Parse(s);
    }
}
