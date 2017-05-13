using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Collections.Generic;

public class SimuladoDisponivelView : MonoBehaviour {

    Main main;

    CtrCadastroAvaliacao cadastroAvaliacao = new CtrCadastroAvaliacao();
    CtrCadastroAluno cadastroAluno = new CtrCadastroAluno();
    CtrCadastroMateria cadastroMateria = new CtrCadastroMateria();

    List<Avaliacao> simulados;

    [SerializeField]
    SimuladoDisponivelDinamicGrid sDDG;

    public int selecionado;
    
    void Start()
    {
        main = Camera.main.gameObject.GetComponent<Main>();
    }

    public void Inicia()
    {
        StartCoroutine(AtualizaGridSimuladoDisponivel());
    }

    public void AtualizaSimuladoDisponivelSelecionada(string s)
    {
        selecionado = Int32.Parse(s);
    }

    IEnumerator AtualizaGridSimuladoDisponivel()
    {
        //carrega lista de alunos
        simulados = cadastroAvaliacao.ListarSimuladosPorAluno(PlayerPrefs.GetInt("IdUltimoAlunoLogado"));

        //aguarda downlaod
        yield return simulados;

        //passa a lista para a grid e preenche a mesma
        sDDG.SetListaDeSimulados(simulados);
        sDDG.Resize();
    }
    
    public void VoltarParaMenuJogador()
    {
        selecionado = 0;
        main.MudarGameState(19, 0);
    }

    public void IniciarSimulado()
    {
        if (selecionado == 0)
            return;

        PlayerPrefs.GetInt("IdAvaliacaoVigente", selecionado);
        selecionado = 0;
        SceneManager.LoadScene(1);
    }
}
