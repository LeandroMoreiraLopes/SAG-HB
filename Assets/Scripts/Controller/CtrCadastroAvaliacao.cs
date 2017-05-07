using System.Collections.Generic;

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

    private bool Validar(Avaliacao avaliacao)
    {
        if (avaliacao.GetDescricao() == null || avaliacao.GetDescricao().Trim().Equals(""))
        {
            throw new ExcecaoSAG("Descricao deve ser preenchido");
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
            throw new ExcecaoSAG("Materia deve ser preenchido");
        }
        if (avaliacao.GetFuncionarioAutor() == null)
        {
            throw new ExcecaoSAG("FuncionarioAutor deve ser preenchido");
        }
        if (avaliacao.GetDataInicio() <= 0)
        {
            throw new ExcecaoSAG("Data de Inicio deve ser preenchida");
        }
        if (avaliacao.GetDataFim() <= 0)
        {
            throw new ExcecaoSAG("Data de Término deve ser preenchida");
        }
        
        return true;
    }

    public void Incluir(Avaliacao avaliacao)
    {
        try
        {
            if (Validar(avaliacao))
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
            if (Validar(avaliacao))
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

