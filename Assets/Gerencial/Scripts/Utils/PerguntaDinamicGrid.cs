﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerguntaDinamicGrid : MonoBehaviour {

    [SerializeField]
    GameObject gridFilho;
    int numeroDePerguntas;

    public static int selecionado;

    List<Pergunta> lista;

    // Use this for initialization
    public void Resize()
    {
        RectTransform parent = gameObject.GetComponent<RectTransform>();
        GridLayoutGroup grid = gameObject.GetComponent<GridLayoutGroup>();

        grid.cellSize = new Vector2(260, 20);

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
            temp.transform.GetChild(0).gameObject.GetComponent<Text>().text = lista[i].GetDescricao().ToString();
            temp.transform.GetChild(1).gameObject.GetComponent<Text>().text = lista[i].GetDificuldade().ToString();
            temp.name = lista[i].GetId().ToString();
        }

    }

    public void SetListaDePerguntas(List<Pergunta> l)
    {
        lista = l;
    }
}