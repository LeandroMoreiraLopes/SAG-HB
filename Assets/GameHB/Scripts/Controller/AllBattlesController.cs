using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllBattlesController : MonoBehaviour {

    [SerializeField]
    string[] conteudo;

    [SerializeField]
    List<GameObject> batalhas;
    List<int> numBatalha = new List<int>();
    List<GameObject> batalhasAtivadas = new List<GameObject>();

    [SerializeField]
    List<int> tempoParaBatalhas;

    List<Tema> temasInstanciados = new List<Tema>();
    List<List<Pergunta>> perguntasInstanciadas = new List<List<Pergunta>>();

    [SerializeField]
    Image[] backgroundDasPerguntas;

    [SerializeField]
    GameObject painelDePerguntas;

    List<SingleBattleController> temasAtivados = new List<SingleBattleController>();

    [SerializeField]
    List<Sprite> relogioEmSegundos;

    [SerializeField]
    Image relogio;

    [SerializeField]
    AudioSource acabouOTempoSND, respostaCorretaSND, respostaErradaSND;

    public int corretas, total;

    bool respondendo;

    public void CriarBatalhas(List<Tema> temas, List<List<Pergunta>> perguntas)
    {
        List<Tema> TemasNaoInstanciados = temas;
        List<List<Pergunta>> perguntasNaoInstanciadas = perguntas;

        //populando o vetor de indices de todas as batalhas
        for (int i = 0; i < batalhas.Count; i++) numBatalha.Add(i);
        
        //testando se o tema usa palavra-chave
        for (int i = TemasNaoInstanciados.Count - 1; i >= 0; i--)
        {
            for (int j = 0; j < conteudo.Length; j++)
            {
                if (TemasNaoInstanciados[i].GetNome().Trim().ToLower().Contains(conteudo[j]))
                {
                    temasInstanciados.Add(TemasNaoInstanciados[i]);
                    TemasNaoInstanciados.Remove(TemasNaoInstanciados[i]);
                    perguntasInstanciadas.Add(perguntasNaoInstanciadas[i]);
                    perguntasNaoInstanciadas.Remove(perguntasNaoInstanciadas[i]);
                    StartCoroutine(AtivarBatalha(j,tempoParaBatalhas[0],j));
                    numBatalha.Remove(j);
                    tempoParaBatalhas.Remove(tempoParaBatalhas[0]);
                    break;
                }
            }
        }

        //caso nao use palavra-chave, randomiza
        for (int i = TemasNaoInstanciados.Count - 1; i >= 0; i--)
        {
            temasInstanciados.Add(TemasNaoInstanciados[i]);
            TemasNaoInstanciados.Remove(TemasNaoInstanciados[i]);
            perguntasInstanciadas.Add(perguntasNaoInstanciadas[i]);
            perguntasNaoInstanciadas.Remove(perguntasNaoInstanciadas[i]);
            int sorteio = numBatalha[Random.Range(0, numBatalha.Count - 1)];
            numBatalha.Remove(sorteio);
            StartCoroutine(AtivarBatalha(sorteio, tempoParaBatalhas[0], sorteio));
            tempoParaBatalhas.Remove(tempoParaBatalhas[0]);
        }
    }

    //ativando as batalhas de acordo com os tempos da lista
    IEnumerator AtivarBatalha(int index, int time, int perguntas)
    {
        yield return new WaitForSeconds(time);
        batalhas[index].SetActive(true);
        batalhasAtivadas.Add(batalhas[index]);
        batalhas[index].GetComponent<SingleBattleController>().SetTema(temasInstanciados[0]);
        temasInstanciados.Remove(temasInstanciados[0]);
        batalhas[index].GetComponent<SingleBattleController>().SetPerguntas(perguntasInstanciadas[0]);
        temasAtivados.Add(batalhas[index].GetComponent<SingleBattleController>());
        perguntasInstanciadas.Remove(perguntasInstanciadas[0]);
    }

    //chama a pergunta das batalhas ativas
    public void ChamandoPerguntaDasBatalhasAtivas()
    {
        int temaSorteado = Random.Range(0, temasAtivados.Count - 1);
        Pergunta pergunta = temasAtivados[temaSorteado].PegaPerguntaNaoFeita();
        
        painelDePerguntas.SetActive(true);
        //texto da pergunta
        painelDePerguntas.transform.GetChild(0).GetComponentInChildren<Text>().text = pergunta.GetDescricao();
        //referencia para sorteio das perguntas 1 - 4
        List<int> posicoes = new List<int>(); for (int i = 1; i < 5; i++) { posicoes.Add(i); }

        //randomizando resposta certa
        int sorteio = posicoes[Random.Range(0, posicoes.Count)];
        posicoes.Remove(sorteio);
        painelDePerguntas.transform.GetChild(sorteio).GetComponentInChildren<Text>().text = pergunta.GetCorreta();
        painelDePerguntas.transform.GetChild(sorteio).name = "right";

        //randomizando as respostas erradas
        sorteio = posicoes[Random.Range(0, posicoes.Count)];
        posicoes.Remove(sorteio);
        painelDePerguntas.transform.GetChild(sorteio).GetComponentInChildren<Text>().text = pergunta.GetErrada1();
        painelDePerguntas.transform.GetChild(sorteio).name = "wrong";
        sorteio = posicoes[Random.Range(0, posicoes.Count)];
        posicoes.Remove(sorteio);
        painelDePerguntas.transform.GetChild(sorteio).GetComponentInChildren<Text>().text = pergunta.GetErrada2();
        painelDePerguntas.transform.GetChild(sorteio).name = "wrong";
        sorteio = posicoes[Random.Range(0, posicoes.Count)];
        posicoes.Remove(sorteio);
        painelDePerguntas.transform.GetChild(sorteio).GetComponentInChildren<Text>().text = pergunta.GetErrada3();
        painelDePerguntas.transform.GetChild(sorteio).name = "wrong";

        respondendo = true;

        StartCoroutine(ContagemDoRelogio());
    }

    void DesativarPerguntas()
    {
        painelDePerguntas.SetActive(false);
    }

    IEnumerator ContagemDoRelogio()
    {
        relogio.sprite = relogioEmSegundos[30];
        for (int i = 29; i >= 0; i--)
        {
            if (!respondendo)
                yield break;

            yield return new WaitForSeconds(1);
            relogio.sprite = relogioEmSegundos[i];

        }
        total++;
        DesativarPerguntas();
        acabouOTempoSND.Play();
        respondendo = false;
    }

    public void Respondendo(string s)
    {
        respondendo = false;
        if (s == "right")
        {
            corretas++;
            respostaCorretaSND.Play();
        }
        else
            respostaErradaSND.Play();

        total++;
        //StopCoroutine(ContagemDoRelogio()); //nao funciona
        DesativarPerguntas();
    }
}
