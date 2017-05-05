using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtualizaGridDosAlunos : MonoBehaviour {
    [SerializeField]
    GameObject gridTotal, gridSelecionados, gridFilho;

    public void AtualizaGrid(List<Aluno> listaAluno1, List<Aluno> listaAluno2)
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

        for (int j = 0; j < listaAluno2.Count; j++)
        {
            listaAluno1 = SelecionarAlunoDaGrid.RemoverAlunoSelecionado(listaAluno2[j].GetId(), listaAluno1);
        }

        for (int i = 0; i < listaAluno1.Count; i++)
        {
            GameObject temp = Instantiate(gridFilho, gridTotal.transform.position, gridTotal.transform.rotation) as GameObject;
            temp.transform.SetParent(gridTotal.transform);
            temp.transform.GetChild(0).gameObject.GetComponent<Text>().text = listaAluno1[i].GetMatricula().ToString();
            temp.transform.GetChild(1).gameObject.GetComponent<Text>().text = listaAluno1[i].GetNomeCompleto().ToString();
            temp.name = listaAluno1[i].GetId().ToString();
        }

        for (int j = 0; j < listaAluno2.Count; j++)
        {
            GameObject temp = Instantiate(gridFilho, gridSelecionados.transform.position, gridSelecionados.transform.rotation) as GameObject;
            temp.transform.SetParent(gridSelecionados.transform);
            temp.transform.GetChild(0).gameObject.GetComponent<Text>().text = listaAluno2[j].GetMatricula().ToString();
            temp.transform.GetChild(1).gameObject.GetComponent<Text>().text = listaAluno2[j].GetNomeCompleto().ToString();
            temp.name = listaAluno2[j].GetId().ToString();
        }
    }
}
