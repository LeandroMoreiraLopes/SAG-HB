using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class MateriaView : MonoBehaviour {

    Main main;

    CtrCadastroMateria cadastroMateria = new CtrCadastroMateria();

    List<Materia> materias;

    [SerializeField]
    MateriaDinamicGrid mDG;

    [SerializeField]
    InputField nome, descricao;

    [SerializeField]
    Button criar, atualiza, voltar;

    [SerializeField]
    GameObject excluirPopUp;

    public int selecionado;


    void Start()
    {
        main = Camera.main.gameObject.GetComponent<Main>();
        StartCoroutine(AtualizaGrid());
    }

    #region métodos para Consultar materia
    public void ConsultarMateria()
    {
        StartCoroutine(CarregaConsultaMateria());
    }

    IEnumerator CarregaConsultaMateria()
    {
        Materia umaMateria = new Materia();
        umaMateria.SetId(selecionado);
        cadastroMateria.Carregar(umaMateria);

        yield return umaMateria;
        
        nome.text = umaMateria.GetNome();
        descricao.text = umaMateria.GetDescricao();
       
        nome.interactable = false;
        descricao.interactable = false;
       
        voltar.gameObject.SetActive(true);
        main.MudarGameState(8, 0);
    }
    #endregion

    #region métodos para Editar materia
    public void EditaMateria()
    {
        StartCoroutine(PreencheCamposParaEditarMateria());
    }

    IEnumerator PreencheCamposParaEditarMateria()
    {
        Materia umaMateria = new Materia();
        umaMateria.SetId(selecionado);
        cadastroMateria.Carregar(umaMateria);

        yield return umaMateria;

        nome.text = umaMateria.GetNome();
        descricao.text = umaMateria.GetDescricao();
        atualiza.gameObject.SetActive(true);
        voltar.gameObject.SetActive(true);
        main.MudarGameState(8, 0);
    }

    public void AtualizaMateriaNoBanco()
    {
        Materia umaMateria = new Materia();
        umaMateria.SetId(selecionado);
        umaMateria.SetNome(nome.text);
        umaMateria.SetDescricao(descricao.text);
       
        cadastroMateria.Alterar(umaMateria);

        StartCoroutine(AtualizaGrid());
        VoltaManterMateria();
    }
    #endregion

    #region métodos para Incluir materia
    public void NovaMateria()
    {
        ApagarTudo();
        criar.gameObject.SetActive(true);
        voltar.gameObject.SetActive(true);
        main.MudarGameState(8, 0);
    }

    public void CriarMateriaNoBanco()
    {
        Materia umaMateria = new Materia();

        umaMateria.SetNome(nome.text);
        umaMateria.SetDescricao(descricao.text);

        cadastroMateria.Incluir(umaMateria);

        StartCoroutine(AtualizaGrid());
        VoltaManterMateria();
    }
    #endregion

    #region métodos para Excluir materia
    public void TemCertezaPopUp()
    {
        if (selecionado != null && selecionado != 0)
        {
            excluirPopUp.SetActive(true);
        }
    }

    public void ApagaMateriaDoBanco()
    {
        Materia umaMateria = new Materia();

        umaMateria.SetId(selecionado);
        cadastroMateria.Excluir(umaMateria);
        excluirPopUp.SetActive(false);
        StartCoroutine(AtualizaGrid());

    }

    public void NaoTemCertezaPopUp()
    {
        excluirPopUp.SetActive(false);
    }

    #endregion

    void ApagarTudo()
    {
        criar.gameObject.SetActive(false);
        atualiza.gameObject.SetActive(false);
        voltar.gameObject.SetActive(false);
        nome.text = "";
        descricao.text = "";
    }

    public void AtualizaMateriaSelecionado(string s)
    {
        selecionado = Int32.Parse(s);
    }

    IEnumerator AtualizaGrid()
    {
        //carrega lista de alunos
        materias = cadastroMateria.ListarTodos();

        //aguarda downlaod
        yield return materias;

        //passa a lista para a grid e preenche a mesma
        mDG.SetListaDeMaterias(materias);
        mDG.Resize();
    }

    public void VoltaManterMateria()
    {
        nome.interactable = true;
        descricao.interactable = true;
        
        ApagarTudo();
        main.MudarGameState(7, 0);
    }

    public void VoltarParaMenuFunc()
    {
        main.MudarGameState(2, 0);
    }
}
