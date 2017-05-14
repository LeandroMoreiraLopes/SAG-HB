using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleBattleController : MonoBehaviour {

    AllBattlesController battleController;
    CameraController cam;

    [SerializeField]
    AudioSource destruicao;

    [SerializeField]
    GameObject fumaca;

    [SerializeField]
    Text[] hpTxt; 
    static int indexDoHP = -1;
    int meuIndex = 0;

    Tema tema = new Tema();
    List<Pergunta> perguntas = new List<Pergunta>();
    List<Pergunta> perguntasNaoFeitas = new List<Pergunta>();
    List<Pergunta> perguntasFeitas = new List<Pergunta>();

    int hp = 100;
    bool ativa = false;

    private void Start()
    {
        battleController = GameObject.FindGameObjectWithTag("Controlador").GetComponent<AllBattlesController>();

        indexDoHP++;
        meuIndex = indexDoHP;

        hpTxt[meuIndex].gameObject.SetActive(true);
        hpTxt[meuIndex].text = "Tema "+ (meuIndex+1) +" HP: " + hp;

        ativa = true;
        StartCoroutine(AtivaEPerdendoHP());
        cam = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraController>();
    }

    IEnumerator AtivaEPerdendoHP()
    {
        while (ativa)
        {
            yield return new WaitForSeconds(5);
            hp -= 2;
            hpTxt[meuIndex].text = "Tema " + (meuIndex + 1) + " HP: " + hp;
            if (hp <= 0)
            {
                ativa = false;
                destruicao.Play();
                cam.Shake();
                Instantiate(fumaca, transform.position, Quaternion.identity);
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
        hpTxt[meuIndex].text = "Tema " + (meuIndex + 1) + " HP: " + hp;
    }
}
