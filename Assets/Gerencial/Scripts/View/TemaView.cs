using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class TemaView : MonoBehaviour {

    Main main;

    CtrCadastroTema cadastroTema = new CtrCadastroTema();
    CtrCadastroMateria cadastroMateria = new CtrCadastroMateria();

    List<Tema> temas;

    [SerializeField]
    TemaDinamicGrid tDG;

    [SerializeField]
    InputField nome, descricao, serie;

    [SerializeField]
    Dropdown materia;

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

    #region métodos para Consultar tema
    public void ConsultarTema()
    {
        StartCoroutine(CarregaConsultaTema());
    }

    IEnumerator CarregaConsultaTema()
    {
        //carrega o tema
        Tema umTema = new Tema();
        umTema.SetId(selecionado);
        cadastroTema.Carregar(umTema);

        yield return umTema;

        //carrega a materia relativa ao tema
        Materia umaMateria = new Materia();
        umaMateria.SetId(umTema.GetMatId());
        cadastroMateria.Carregar(umaMateria);

        yield return umaMateria;

        nome.text = umTema.GetNome();
        descricao.text = umTema.GetDescricao();
        serie.text = umTema.GetSerie();
        StartCoroutine(AtualizaDropDown());
        materia.value = EncontrarMateriaNaDropDownTrazendoValue(umaMateria.GetNome());

        nome.interactable = false;
        descricao.interactable = false;
        serie.interactable = false;
        materia.interactable = false;

        voltar.gameObject.SetActive(true);
        main.MudarGameState(10, 0);
    }
    #endregion

    #region métodos para Editar tema
    public void EditaTema()
    {
        StartCoroutine(PreencheCamposParaEditarTema());
    }

    IEnumerator PreencheCamposParaEditarTema()
    {
        //carrega o tema
        Tema umTema = new Tema();
        umTema.SetId(selecionado);
        cadastroTema.Carregar(umTema);

        yield return umTema;

        //carrega a materia relativa ao tema
        Materia umaMateria = new Materia();
        umaMateria.SetId(umTema.GetMatId());
        cadastroMateria.Carregar(umaMateria);

        yield return umaMateria;

        nome.text = umTema.GetNome();
        descricao.text = umTema.GetDescricao();
        serie.text = umTema.GetSerie();
        StartCoroutine(AtualizaDropDown());
        materia.value = EncontrarMateriaNaDropDownTrazendoValue(umaMateria.GetNome());

        atualiza.gameObject.SetActive(true);
        voltar.gameObject.SetActive(true);

        main.MudarGameState(10, 0);
    }

    public void AtualizaTemaNoBanco()
    {
        Tema umTema = new Tema();
        umTema.SetId(selecionado);
        umTema.SetNome(nome.text);
        umTema.SetDescricao(descricao.text);
        umTema.SetSerie(serie.text);
        umTema.SetMatId(EncontrarMateriaNaDropDownTrazendoId(materia.options[materia.value].text)); //banco com materia no id = 2

        cadastroTema.Alterar(umTema);

        StartCoroutine(AtualizaGrid());
        VoltaManterTema();
    }
    #endregion

    #region métodos para Incluir tema
    public void NovoTema()
    {
        ApagarTudo();
        criar.gameObject.SetActive(true);
        voltar.gameObject.SetActive(true);
        main.MudarGameState(10, 0);
        StartCoroutine(AtualizaDropDown());
    }

    public void CriarTemaNoBanco()
    {
        Tema umTema = new Tema();

        umTema.SetNome(nome.text);
        umTema.SetDescricao(descricao.text);
        umTema.SetSerie(serie.text);
        umTema.SetMatId(EncontrarMateriaNaDropDownTrazendoId(materia.options[materia.value].text));

        cadastroTema.Incluir(umTema);

        StartCoroutine(AtualizaGrid());
        VoltaManterTema();
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

    public void ApagaTemaDoBanco()
    {
        Tema umTema = new Tema();

        umTema.SetId(selecionado);
        cadastroTema.Excluir(umTema);
        excluirPopUp.SetActive(false);
        StartCoroutine(AtualizaGrid());
        selecionado = 0;

    }

    public void NaoTemCertezaPopUp()
    {
        excluirPopUp.SetActive(false);
        selecionado = 0;
    }

    #endregion

    void ApagarTudo()
    {
        criar.gameObject.SetActive(false);
        atualiza.gameObject.SetActive(false);
        voltar.gameObject.SetActive(false);
        nome.text = "";
        descricao.text = "";
        serie.text = "";
        materia.value = 0;
        selecionado = 0;
    }

    public void AtualizaTemaSelecionado(string s)
    {
        selecionado = Int32.Parse(s);
    }

    IEnumerator AtualizaGrid()
    {
        //carrega lista de alunos
        temas = cadastroTema.ListarTodos();

        //aguarda downlaod
        yield return temas;

        //passa a lista para a grid e preenche a mesma
        tDG.SetListaDeTemas(temas);
        tDG.Resize();
    }

    IEnumerator AtualizaDropDown()
    {
        List<Materia> materias = new List<Materia>();
        materias = cadastroMateria.ListarTodos();

        yield return materias;

        materia.options.Clear();

        for (int i = 0; i < materias.Count; i++)
        {
            if (materias[i] != null)
            {
                materia.options.Add(new Dropdown.OptionData(materias[i].GetNome()));
            }
        }
        materia.captionText = materia.captionText;


    }

    public void VoltaManterTema()
    {
        nome.interactable = true;
        descricao.interactable = true;
        serie.interactable = true;
        materia.interactable = true;

        ApagarTudo();
        main.MudarGameState(9, 0);
    }

    public void VoltarParaMenuFunc()
    {
        main.MudarGameState(2, 0);
    }
    int EncontrarMateriaNaDropDownTrazendoId(string s)
    {
        int mat_id = 0;

        List<Materia> listaDeMaterias = new List<Materia>();
        listaDeMaterias = cadastroMateria.ListarTodos();

        for (int i = 0; i < listaDeMaterias.Count; i++)
        {
            if (listaDeMaterias[i].GetNome() == s)
                mat_id = listaDeMaterias[i].GetId();
        }
        return mat_id;
    }

    int EncontrarMateriaNaDropDownTrazendoValue(string s)
    {
        int mat_id = 0;

        List<Materia> listaDeMaterias = new List<Materia>();
        listaDeMaterias = cadastroMateria.ListarTodos();

        for (int i = 0; i < listaDeMaterias.Count; i++)
        {
            if (listaDeMaterias[i].GetNome() == s)
                mat_id = i;
        }
        return mat_id;
    }

}
