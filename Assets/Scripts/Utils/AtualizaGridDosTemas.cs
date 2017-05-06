using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtualizaGridDosTemas : MonoBehaviour {

    [SerializeField]
    GameObject gridTotal, gridSelecionados, gridFilho;

    int selecionadoDaEsquerda, selecionadoDaDireita;

    public void AtualizaGrid(List<Tema> listaTema1, List<Tema> listaTema2)
    {
        RectTransform parent = gridTotal.GetComponent<RectTransform>();
        GridLayoutGroup grid = gridTotal.GetComponent<GridLayoutGroup>();

        grid.cellSize = new Vector2(parent.rect.width, 20);

        for (int i = gridTotal.transform.GetChildCount() - 1; i > 0; i--)
        {
            if (gridTotal.transform.GetChild(i).name != "Grid - Parent")
            {
                Destroy(gridTotal.transform.GetChild(i).gameObject);
            }
        }

        for (int j = gridSelecionados.transform.GetChildCount() - 1; j > 0; j--)
        {
            if (gridSelecionados.transform.GetChild(j).name != "Grid - Parent")
            {
                Destroy(gridSelecionados.transform.GetChild(j).gameObject);
            }
        }

        for (int k = 0; k < listaTema2.Count; k++)
        {
            listaTema1 = RemoverTemaSelecionado(listaTema2[k].GetId(), listaTema1);
        }

        for (int l = 0; l < listaTema1.Count; l++)
        {
            GameObject temp = Instantiate(gridFilho, gridTotal.transform.position, gridTotal.transform.rotation) as GameObject;
            temp.transform.SetParent(gridTotal.transform);
            temp.transform.GetChild(0).gameObject.GetComponent<Text>().text = listaTema1[l].GetNome();
            temp.name = listaTema1[l].GetId().ToString();
        }

        for (int m = 0; m < listaTema2.Count; m++)
        {
            GameObject temp = Instantiate(gridFilho, gridSelecionados.transform.position, gridSelecionados.transform.rotation) as GameObject;
            temp.transform.SetParent(gridSelecionados.transform);
            temp.transform.GetChild(0).gameObject.GetComponent<Text>().text = listaTema2[m].GetNome();
            temp.name = listaTema2[m].GetId().ToString();
        }
    }

    public List<int> GetIDsDosTemasSelecionados()
    {
        List<int> ids = new List<int>();
        for (int n = gridSelecionados.transform.GetChildCount() - 1; n > 0; n--)
        {
            int i = int.Parse(gridSelecionados.transform.GetChild(n).gameObject.name);
            ids.Add(i);
        }
        return ids;
    }

    List<Tema> RemoverTemaSelecionado(int id, List<Tema> lista)
    {
        List<Tema> l = lista;
        for (int i = lista.Count - 1; i >= 0; i--)
        {
            if (id == l[i].GetId())
                l.Remove(l[i]);
        }

        return l;
    }

    public void AtualizaTemaDaEsquerda(string s)
    {
        selecionadoDaEsquerda = int.Parse(s);
        selecionadoDaDireita = 0;
    }

    public void AtualizaTemaDaDireita(string s)
    {
        selecionadoDaDireita = int.Parse(s);
        selecionadoDaEsquerda = 0;
    }

    public void AdicionaTemaSelecionado()
    {
        if ((selecionadoDaEsquerda != 0) && (gridSelecionados.transform.childCount < 5))
        {
            for (int j = gridTotal.transform.GetChildCount() - 1; j > 0; j--)
            {
                if (gridTotal.transform.GetChild(j).name == selecionadoDaEsquerda.ToString())
                {
                    gridTotal.transform.GetChild(j).gameObject.transform.parent = gridSelecionados.transform;
                    selecionadoDaDireita = selecionadoDaEsquerda;
                    selecionadoDaEsquerda = 0;
                    return;
                }
            }
        }
    }

    public void RemoveTemaSelecionado()
    {
        if (selecionadoDaDireita != 0)
        {
            for (int j = gridSelecionados.transform.GetChildCount() - 1; j > 0; j--)
            {
                if (gridSelecionados.transform.GetChild(j).name == selecionadoDaDireita.ToString())
                {
                    gridSelecionados.transform.GetChild(j).gameObject.transform.parent = gridTotal.transform;
                    selecionadoDaEsquerda = selecionadoDaDireita;
                    selecionadoDaDireita = 0;
                    return;
                }
            }
        }
    }
}
