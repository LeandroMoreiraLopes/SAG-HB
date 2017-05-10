using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour {

    [SerializeField]
    string[] conteudo;

    [SerializeField]
    List<GameObject> batalhas;
    List<int> numBatalha = new List<int>();
    List<GameObject> batalhasAtivadas = new List<GameObject>();

    [SerializeField]
    List<int> tempoParaBatalhas;

    List<Tema> temasInstanciados = new List<Tema>();

    public void CriarBatalhas(List<Tema> temas)
    {
        List<Tema> naoInstanciadas = temas;
        for (int i = 0; i < batalhas.Count; i++) numBatalha.Add(i);
        
        for (int i = naoInstanciadas.Count - 1; i >= 0; i--)
        {
            for (int j = 0; j < conteudo.Length; j++)
            {
                if (naoInstanciadas[i].GetNome().Trim().ToLower().Contains(conteudo[j]))
                {
                    temasInstanciados.Add(naoInstanciadas[i]);
                    naoInstanciadas.Remove(naoInstanciadas[i]);
                    StartCoroutine(AtivarBatalha(j,tempoParaBatalhas[0]));
                    numBatalha.Remove(j);
                    tempoParaBatalhas.Remove(tempoParaBatalhas[0]);
                    break;
                }
            }
        }

        for (int i = naoInstanciadas.Count - 1; i >= 0; i--)
        {
            temasInstanciados.Add(naoInstanciadas[i]);
            int sorteio = numBatalha[Random.Range(0, numBatalha.Count - 1)];
            numBatalha.Remove(sorteio);
            StartCoroutine(AtivarBatalha(sorteio, tempoParaBatalhas[0]));
            tempoParaBatalhas.Remove(tempoParaBatalhas[0]);
        }
    }

    IEnumerator AtivarBatalha(int index, int time)
    {
        yield return new WaitForSeconds(time);
        batalhas[index].SetActive(true);
        batalhasAtivadas.Add(batalhas[index]);
    }
}
