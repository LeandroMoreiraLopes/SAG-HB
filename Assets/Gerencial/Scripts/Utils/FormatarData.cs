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
        if (data.Length == 3)
        {
            if (data[1].Length == 1) data[1] = "0" + data[1];
            if (data[0].Length == 1) data[0] = "0" + data[0];

            s = data[2] + data[1] + data[0];
            return int.Parse(s);
        }
        else
            return 0;
    }

    public static bool AntesDaDataInicial(int i)
    {
        bool b = false;

        DateTime hoje = DateTime.Now;
        string dataHoje = hoje.ToShortDateString();
        string[] dh = dataHoje.Split('/');

        if (dh[1].Length == 1) dh[1] = "0" + dh[1];
        if (dh[0].Length == 1) dh[0] = "0" + dh[0];


        dataHoje = dh[2] + dh[0] + dh[1];

        int dataHj = int.Parse(dataHoje);

        if (dataHj < i)
            b = true;        

        return b;
    }

    public static bool AntesOuIgualDaDataInicial(int i)
    {
        bool b = false;

        DateTime hoje = DateTime.Now;
        string dataHoje = hoje.ToShortDateString();
        string[] dh = dataHoje.Split('/');

        if (dh[1].Length == 1) dh[1] = "0" + dh[1];
        if (dh[0].Length == 1) dh[0] = "0" + dh[0];


        dataHoje = dh[2] + dh[0] + dh[1];

        int dataHj = int.Parse(dataHoje);

        if (dataHj <= i)
            b = true;

        return b;
    }



    public static bool DepoisDaDataFinal(int i)
    {
        bool b = false;

        DateTime hoje = DateTime.Now;
        string dataHoje = hoje.ToShortDateString();
        string[] dh = dataHoje.Split('/');

        if (dh[1].Length == 1) dh[1] = "0" + dh[1];
        if (dh[0].Length == 1) dh[0] = "0" + dh[0];


        dataHoje = dh[2] + dh[0] + dh[1];

        int dataHj = int.Parse(dataHoje);

        if (dataHj > i)
            b = true;

        return b;
    }
}
