﻿using System.Collections.Generic;

public class CtrCadastroAvaliacao {

    static CtrCadastroAvaliacao umCadastroAvaliacao;
    AvaliacaoDAO avaliacaoDAO;

    public CtrCadastroAvaliacao()
    {
        DAOFactory daoFactory = new DAOFactory();
        avaliacaoDAO = daoFactory.getAvaliacaoDAO();
    }

    public static CtrCadastroAvaliacao getInstance()
    {
        if (umCadastroAvaliacao == null)
        {
            umCadastroAvaliacao = new CtrCadastroAvaliacao();
        }
        return umCadastroAvaliacao;
    }

    public List<Avaliacao> ListarTodos()
    {
        List<Avaliacao> lista = new List<Avaliacao>();
        try
        {
            lista = avaliacaoDAO.PegarTodos();
        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }
        return lista;
    }

    public List<Avaliacao> ListarTodosPorAluno(int alunoId)
    {
        List<Avaliacao> lista = new List<Avaliacao>();
        try
        {
            lista = avaliacaoDAO.PegarTodosPorAluno(alunoId);
        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }
        return lista;
    }

    public List<Avaliacao> ListarSimuladosPorAluno(int alunoId)
    {
        List<Avaliacao> lista = new List<Avaliacao>();
        try
        {
            lista = avaliacaoDAO.PegarSimuladosPorAluno(alunoId);
        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }
        return lista;
    }

    private bool Validar(Avaliacao avaliacao)
    {
        if (avaliacao.GetDescricao() == null || avaliacao.GetDescricao().Trim().Equals(""))
        {
            throw new ExcecaoSAG("Descricao deve ser preenchida");
        }
        if (avaliacao.GetTemas() == null || avaliacao.GetTemas().Count <= 0)
        {
            throw new ExcecaoSAG("Temas devem ser preenchidos");
        }
        if (avaliacao.GetAlunos() == null || avaliacao.GetAlunos().Count <=0)
        {
            throw new ExcecaoSAG("Alunos devem ser preenchidos");
        }
        if (avaliacao.GetMateria() == null)
        {
            throw new ExcecaoSAG("Materia deve ser preenchida");
        }
        if (avaliacao.GetFuncionarioAutor() == null)
        {
            throw new ExcecaoSAG("Funcionario Autor deve ser preenchido");
        }
        if (avaliacao.GetDataInicio() <= 0)
        {
            throw new ExcecaoSAG("Data de Inicio deve ser preenchida e tem que ser após a data de hoje");
        }
        if (avaliacao.GetDataFim() <= 0 && avaliacao.GetDataFim() < avaliacao.GetDataInicio())
        {
            throw new ExcecaoSAG("Data de Término deve ser preenchida e tem que ser maior ou igual a data de início");
        }
        
        return true;
    }

    public void Incluir(Avaliacao avaliacao)
    {
        try
        {
            if (Validar(avaliacao) && FormatarData.AntesOuIgualDaDataInicial(avaliacao.GetDataInicio()))
            {
                avaliacaoDAO.Incluir(avaliacao);
            }
        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }

    }

    public void Alterar(Avaliacao avaliacao)
    {
        try
        {
            if (Validar(avaliacao) && FormatarData.AntesDaDataInicial(avaliacao.GetDataInicio()))
            {
                avaliacaoDAO.Alterar(avaliacao);
            }

        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }

    }

    public void Excluir(Avaliacao avaliacao)
    {
        try
        {
            if (FormatarData.AntesDaDataInicial(avaliacao.GetDataInicio()))
                avaliacaoDAO.Excluir(avaliacao);
        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }

    }

    public void Carregar(Avaliacao avaliacao)
    {
        try
        {
            avaliacaoDAO.Carregar(avaliacao);
        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }
    }
}
