using System;
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
        s = data[2] + data[1] + data[0];
        return int.Parse(s);
    }

    public static bool AntesDaDataInicial(int i)
    {
        bool b = false;

        DateTime hoje = DateTime.Now;
        string dataHoje = hoje.ToShortDateString();
        string[] dh = dataHoje.Split('/');

        if (dh[1].Length == 1) dh[1] = "0" + dh[1];
        if (dh[0].Length == 1) dh[0] = "0" + dh[0];


        dataHoje = dh[2] + dh[1] + dh[0];

        int dataHj = int.Parse(dataHoje);

        if (dataHj < i)
            b = true;        

        return b;
    }
}
