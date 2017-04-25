using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormatarData : MonoBehaviour {

    public static string FormatToString(int i)
    {
        string s = i.ToString();
        string ano = s.Substring(0, 4);
        string mes = s.Substring(5, 2);
        string dia = s.Substring(7, 2);
        s = dia + "/" + mes + "/" + ano;
        return s;
    }

    public static int FormatToInt(string s)
    {
        string[] data = s.Split('/');
        s = data[2] + data[1]+data[0];
        return int.Parse(s);
    }
}
