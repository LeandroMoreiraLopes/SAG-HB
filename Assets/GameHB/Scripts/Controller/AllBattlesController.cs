using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        perguntasInstanciadas.Remove(perguntasInstanciadas[0]);
    }
}
