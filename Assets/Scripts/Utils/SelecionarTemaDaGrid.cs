using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelecionarTemaDaGrid {
   	// Use this for initialization
	public static List<Tema> AdicionarTemaSelecionado (int id, List<Tema> listaTema1, List<Tema> listaTema2 ) {

        for (int i = 0; i < listaTema1.Count; i++)
        {
            if (listaTema1[i].GetId() == id)
            {
                listaTema2.Add(listaTema1[i]);
            }
        }

        return listaTema2;
	}

    public static List<Tema> RemoverTemaSelecionado(int id, List<Tema> listaTema)
    {
        for (int i = 0; i < listaTema.Count; i++)
        {
            if (listaTema[i].GetId() == id)
            {
                listaTema.Remove(listaTema[i]);
            }
        }

        return listaTema;
    }
}
