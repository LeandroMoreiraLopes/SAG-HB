using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Collections.Generic;

public class AvaliacaoDisponivelView : MonoBehaviour {

    Main main;

    CtrCadastroAvaliacao cadastroAvaliacao = new CtrCadastroAvaliacao();
    CtrCadastroAluno cadastroAluno = new CtrCadastroAluno();
    CtrCadastroMateria cadastroMateria = new CtrCadastroMateria();

    List<Avaliacao> avaliacoes;

    [SerializeField]
    AvaliacaoDisponivelDinamicGrid aDDG;

    public int selecionado;
    
    void Start()
    {
        main = Camera.main.gameObject.GetComponent<Main>();
    }

    public void Inicia()
    {
        StartCoroutine(AtualizaGridAvaliacaoDisponivel());
    }

    public void AtualizaAvaliacaoDisponivelSelecionada(string s)
    {
        selecionado = Int32.Parse(s);
    }

    IEnumerator AtualizaGridAvaliacaoDisponivel()
    {
        //carrega lista de alunos
        avaliacoes = cadastroAvaliacao.ListarTodosPorAluno(PlayerPrefs.GetInt("IdUltimoAlunoLogado"));

        //aguarda downlaod
        yield return avaliacoes;

        //passa a lista para a grid e preenche a mesma
        aDDG.SetListaDeAvaliacoes(avaliacoes);
        aDDG.Resize();
    }
    
    public void VoltarParaMenuJogador()
    {
        selecionado = 0;
        main.MudarGameState(18, 0);
    }

    public void IniciarAvaliacao()
    {
        if (selecionado == 0)
            return;

        PlayerPrefs.SetInt("IdAvaliacaoVigente", selecionado);
        selecionado = 0;
        SceneManager.LoadScene("gameHB");
    }
}
