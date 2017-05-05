using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelecionarAlunoDaGrid {
   	// Use this for initialization
	public static List<Aluno> AdicionarAlunoSelecionado (int id, List<Aluno> listaAluno1, List<Aluno> listaAluno2) {

        for (int i = 0; i < listaAluno1.Count; i++)
        {
            if (listaAluno1[i].GetId() == id)
            {
                listaAluno2.Add(listaAluno1[i]);
            }
        }

        return listaAluno2;
	}

    public static List<Aluno> RemoverAlunoSelecionado(int id, List<Aluno> listaAluno)
    {
        for (int i = 0; i < listaAluno.Count; i++)
        {
            if (listaAluno[i].GetId() == id)
            {
                listaAluno.Remove(listaAluno[i]);
            }
        }

        return listaAluno;
    }
}
