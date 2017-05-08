using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMainHB : MonoBehaviour {

    int id_aluno, id_avaliacao;

    CtrCadastroAluno cadastroAluno = new CtrCadastroAluno();
    CtrCadastroAvaliacao cadastroAvaliacao = new CtrCadastroAvaliacao();
    CtrCadastroTema cadastroTema = new CtrCadastroTema();
    CtrCadastroPergunta cadastroPergunta = new CtrCadastroPergunta();

    Aluno aluno = new Aluno();
    Avaliacao avaliacao = new Avaliacao();
    List<Tema> temas = new List<Tema>();
    List<Pergunta>[] perguntas = new List<Pergunta>[4];

    // Use this for initialization
    void Start () {
        id_aluno = PlayerPrefs.GetInt("IdUltimoAlunoLogado");
        id_avaliacao = PlayerPrefs.GetInt("IdAvaliacaoVigente");

        aluno.SetId(id_aluno);
        cadastroAluno.Carregar(aluno);

        avaliacao.SetId(id_avaliacao);
        cadastroAvaliacao.Carregar(avaliacao);

        temas = cadastroTema.ListarTodosPorAvaliacao(id_avaliacao);
        for (int i = 0; i < temas.Count; i++)
        {
            perguntas[i] = cadastroPergunta.ListarTodosPorTema(temas[i].GetId());
        }
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
