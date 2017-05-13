using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleBattleController : MonoBehaviour {

    AllBattlesController battleController;

    Tema tema = new Tema();
    List<Pergunta> perguntas = new List<Pergunta>();
    List<Pergunta> perguntasNaoFeitas = new List<Pergunta>();
    List<Pergunta> perguntasFeitas = new List<Pergunta>();

    int hp = 100;
    bool ativa;

    private void Start()
    {
        battleController = GameObject.FindGameObjectWithTag("Controlador").GetComponent<AllBattlesController>();
    }

    IEnumerator AtivaEPerdandoHP()
    {
        while (ativa)
        {
            yield return new WaitForSeconds(5);
            hp -= 5;
            if (hp <= 0)
            {
                ativa = false;
                battleController.RemocaoDeTema(tema.GetId());
            }
        }
    }

    public void SetTema(Tema t)
    {
        tema = t;
    }

    public int GetTemaId()
    {
        return tema.GetId();
    }

    public void SetPerguntas(List<Pergunta> listaDePerguntas)
    {
        perguntas = listaDePerguntas;
    }

    //retorna pergunta randomica, reiniciando a lista caso vazia
    public Pergunta PegaPerguntaNaoFeita()
    {
        if (perguntasNaoFeitas.Count == 0)
        {
            perguntasNaoFeitas = new List<Pergunta>();
            for (int i = 0; i < perguntas.Count; i++)
            {
                perguntasNaoFeitas.Add(perguntas[i]);
            }
            perguntasFeitas.Clear();
        }

        int sorteio = Random.Range(0, perguntasNaoFeitas.Count);

        perguntasFeitas.Add(perguntasNaoFeitas[sorteio]);
        perguntasNaoFeitas.Remove(perguntasNaoFeitas[sorteio]);

        return perguntasFeitas[perguntasFeitas.Count - 1];
    }

    public void GanhaHP(int pontos)
    {
        if (hp > 0)
        hp += pontos;

        if (hp > 100)
            hp = 100;
    }
}
