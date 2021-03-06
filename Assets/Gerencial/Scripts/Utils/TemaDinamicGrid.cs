﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemaDinamicGrid : MonoBehaviour {

    [SerializeField]
    GameObject gridFilho;
    int numeroDeMaterias;

    public static int selecionado;

    List<Tema> lista;

    // Use this for initialization
    public void Resize()
    {
        RectTransform parent = gameObject.GetComponent<RectTransform>();
        GridLayoutGroup grid = gameObject.GetComponent<GridLayoutGroup>();

        grid.cellSize = new Vector2(parent.rect.width, 20);

        for (int i = transform.GetChildCount() - 1; i > 0; i--)
        {
            if (transform.GetChild(i).name != "Grid - Parent")
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < lista.Count; i++)
        {
            GameObject temp = Instantiate(gridFilho, transform.position, transform.rotation) as GameObject;
            temp.transform.SetParent(gameObject.transform);
            temp.transform.GetChild(0).gameObject.GetComponent<Text>().text = lista[i].GetNome().ToString();
            temp.transform.GetChild(1).gameObject.GetComponent<Text>().text = lista[i].GetSerie().ToString();
            temp.name = lista[i].GetId().ToString();
        }

    }

    public void SetListaDeTemas(List<Tema> l)
    {
        lista = l;
    }
}
