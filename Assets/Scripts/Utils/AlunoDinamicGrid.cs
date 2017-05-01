using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AlunoDinamicGrid : MonoBehaviour {
    [SerializeField]
    GameObject gridFilho;
    int numeroDeAlunos;

    public static int selecionado;

    List<Aluno> lista;

    // Use this for initialization
	public void Resize () {
        RectTransform parent = gameObject.GetComponent<RectTransform>();
        GridLayoutGroup grid = gameObject.GetComponent<GridLayoutGroup>();

        grid.cellSize = new Vector2(parent.rect.width, 30);

        for (int i = 0; i < lista.Count; i++)
        {
            GameObject temp = Instantiate(gridFilho, transform.position, transform.rotation) as GameObject;
            temp.transform.SetParent(gameObject.transform);
            temp.transform.GetChild(0).gameObject.GetComponent<Text>().text = lista[i].GetMatricula().ToString();
            temp.transform.GetChild(1).gameObject.GetComponent<Text>().text = lista[i].GetNomeCompleto().ToString();
            temp.name = lista[i].GetId().ToString();
        }
        
	}

    public void SetListaDeAlunos(List<Aluno> l)
    {
        lista = l;
    }
}
