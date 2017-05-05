using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtualizaGridDosTemas : MonoBehaviour {

    [SerializeField]
    GameObject gridTotal, gridSelecionados, gridFilho;

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

        for (int j = 0; j < listaTema2.Count; j++)
        {
            listaTema1 = SelecionarTemaDaGrid.RemoverTemaSelecionado(listaTema2[j].GetId(), listaTema1);
        }

        for (int i = 0; i < listaTema1.Count; i++)
        {
            GameObject temp = Instantiate(gridFilho, gridTotal.transform.position, gridTotal.transform.rotation) as GameObject;
            temp.transform.SetParent(gridTotal.transform);
            temp.transform.GetChild(0).gameObject.GetComponent<Text>().text = listaTema1[i].GetDescricao();
            temp.name = listaTema1[i].GetId().ToString();
        }

        for (int j = 0; j < listaTema2.Count; j++)
        {
            GameObject temp = Instantiate(gridFilho, gridSelecionados.transform.position, gridSelecionados.transform.rotation) as GameObject;
            temp.transform.SetParent(gridSelecionados.transform);
            temp.transform.GetChild(0).gameObject.GetComponent<Text>().text = listaTema2[j].GetDescricao();
            temp.name = listaTema2[j].GetId().ToString();
        }
    }
}
