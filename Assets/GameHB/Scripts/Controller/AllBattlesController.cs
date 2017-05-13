using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllBattlesController : MonoBehaviour {

    GameMainHB main;

    [SerializeField]
    string[] conteudo;

    [SerializeField]
    List<GameObject> batalhas;
    List<int> numBatalha = new List<int>();
    List<GameObject> batalhasAtivadas = new List<GameObject>();

    [SerializeField]
    List<int> tempoParaBatalhas;

    //guardar os temas da avaliacao em sequencia
    List<Tema> temas = new List<Tema>();
    //PerguntaAtiva
    Pergunta perguntaAtiva = new Pergunta();

    List<Tema> temasInstanciados = new List<Tema>();
    List<List<Pergunta>> perguntasInstanciadas = new List<List<Pergunta>>();

    [SerializeField]
    Image[] backgroundDasPerguntas, backgroundDasRespostas;

    [SerializeField]
    GameObject painelDePerguntas;

    List<SingleBattleController> temasAtivados = new List<SingleBattleController>();

    [SerializeField]
    List<Sprite> relogioEmSegundos;

    [SerializeField]
    Image relogio;

    [SerializeField]
    AudioSource acabouOTempoSND, respostaCorretaSND, respostaErradaSND;

    int tema1Corretas, tema2Corretas, tema3Corretas, tema4Corretas, tema1Total,
        tema2Total, tema3Total, tema4Total;

    bool respondendo;

    TimeController timeController;

    List<int> temasEmExecucao = new List<int>();

    void Start()
    {
        timeController = GetComponent<TimeController>();
        main = GetComponent<GameMainHB>();
    }

    public void CriarBatalhas(List<Tema> temas, List<List<Pergunta>> perguntas)
    {
        List<Tema> temasNaoInstanciados = new List<Tema>();
        for (int i = 0; i < temas.Count; i++) temasNaoInstanciados.Add(temas[i]);

        List<List<Pergunta>> perguntasNaoInstanciadas = perguntas;

        //populando o vetor de indices de todas as batalhas
        for (int i = 0; i < batalhas.Count; i++) numBatalha.Add(i);

        //testando se o tema usa palavra-chave
        for (int i = temasNaoInstanciados.Count - 1; i >= 0; i--)
        {
            for (int j = 0; j < conteudo.Length; j++)
            {
                if (temasNaoInstanciados[i].GetNome().Trim().ToLower().Contains(conteudo[j]))
                {
                    temasInstanciados.Add(temasNaoInstanciados[i]);
                    temasNaoInstanciados.Remove(temasNaoInstanciados[i]);
                    perguntasInstanciadas.Add(perguntasNaoInstanciadas[i]);
                    perguntasNaoInstanciadas.Remove(perguntasNaoInstanciadas[i]);
                    StartCoroutine(AtivarBatalha(j, tempoParaBatalhas[0], j));
                    numBatalha.Remove(j);
                    tempoParaBatalhas.Remove(tempoParaBatalhas[0]);
                    break;
                }
            }
        }

        //caso nao use palavra-chave, randomiza
        for (int i = temasNaoInstanciados.Count - 1; i >= 0; i--)
        {
            temasInstanciados.Add(temasNaoInstanciados[i]);
            temasNaoInstanciados.Remove(temasNaoInstanciados[i]);
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
        //lista para controlar o numero de batalhas ativas, caso 0, gameover
        temasEmExecucao.Add(batalhas[index].GetComponent<SingleBattleController>().GetTemaId());
    }

    //chama a pergunta das batalhas ativas
    public void ChamandoPerguntaDasBatalhasAtivas()
    {
        int temaSorteado = Random.Range(0, temasAtivados.Count);
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

        perguntaAtiva = pergunta;

        respondendo = true;

        StartCoroutine(ContagemDoRelogio());
    }

    void DesativarPerguntas()
    {
        timeController.Respondido();
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

        //definindo qual tema nao foi respondido
        if (temas.Count >= 1 && perguntaAtiva.GetTemaId() == temas[0].GetId())
        {
            tema1Total++;
        }
        else if (temas.Count >= 2 && perguntaAtiva.GetTemaId() == temas[1].GetId())
        {
            tema2Total++;
        }
        else if (temas.Count >= 3 && perguntaAtiva.GetTemaId() == temas[2].GetId())
        {
            tema3Total++;
        }
        else if (temas.Count >= 4 && perguntaAtiva.GetTemaId() == temas[3].GetId())
        {
            tema4Total++;
        }

        DesativarPerguntas();
        acabouOTempoSND.Play();
        respondendo = false;
    }

    public void Respondendo(string s)
    {
        respondendo = false;
        if (s == "right")
        {
            respostaCorretaSND.Play();

            for (int i = 0; i < temasAtivados.Count; i++)
            {
                if (temasAtivados[i].GetTemaId() == perguntaAtiva.GetTemaId())
                {
                    temasAtivados[i].GanhaHP(20);
                }
            }

            //definindo qual tema foi acertado
            if (temas.Count >= 1 && perguntaAtiva.GetTemaId() == temas[0].GetId())
            {
                tema1Corretas++;
                tema1Total++;
            }
            else if (temas.Count >= 2 && perguntaAtiva.GetTemaId() == temas[1].GetId())
            {
                tema2Corretas++;
                tema2Total++;
            }
            else if (temas.Count >= 3 && perguntaAtiva.GetTemaId() == temas[2].GetId())
            {
                tema3Corretas++;
                tema3Total++;
            }
            else if (temas.Count == 4 && perguntaAtiva.GetTemaId() == temas[3].GetId())
            {
                tema4Corretas++;
                tema4Total++;
            }

        }
        else
        {
            //definindo qual tema foi errado
            respostaErradaSND.Play();
            if (temas.Count >= 1 && perguntaAtiva.GetTemaId() == temas[0].GetId())
            {
                tema1Total++;
            }
            else if (temas.Count >= 2 && perguntaAtiva.GetTemaId() == temas[1].GetId())
            {
                tema2Total++;
            }
            else if (temas.Count >= 3 && perguntaAtiva.GetTemaId() == temas[2].GetId())
            {
                tema3Total++;
            }
            else if (temas.Count >= 4 && perguntaAtiva.GetTemaId() == temas[3].GetId())
            {
                tema4Total++;
            }
        }

        //StopCoroutine(ContagemDoRelogio()); //nao funciona
        DesativarPerguntas();
    }

    public void SetTemas(List<Tema> l)
    {
        temas = l;
    }

    public void RemocaoDeTema(int i)
    {
        temasEmExecucao.Remove(i);
        if (temasEmExecucao.Count == 0)
        {
            FinalizarJogo();
        }
    }

    public void FinalizarJogo()
    {
        timeController.SetGameOver(true);
        main.FinalizarJogo(tema1Corretas, tema2Corretas, tema3Corretas, tema4Corretas,
            tema1Total, tema2Total, tema3Total, tema4Total);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Q))
        {
            main.FinalizarJogo(tema1Corretas, tema2Corretas, tema3Corretas, tema4Corretas,
                tema1Total, tema2Total, tema3Total, tema4Total);
        }
    }
}
