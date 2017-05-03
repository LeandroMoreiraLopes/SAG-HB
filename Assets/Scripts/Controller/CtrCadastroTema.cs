using System.Collections.Generic;

public class CtrCadastroTema {
    static CtrCadastroTema umCadastroTema;
    TemaDAO temaDAO;

    public CtrCadastroTema()
    {
        DAOFactory daoFactory = new DAOFactory();
        temaDAO = daoFactory.getTemaDAO();
    }

    public static CtrCadastroTema getInstance()
    {
        if (umCadastroTema == null)
        {
            umCadastroTema = new CtrCadastroTema();
        }
        return umCadastroTema;
    }

    public List<Tema> ListarTodos()
    {
        List<Tema> lista = new List<Tema>();
        try
        {
            lista = temaDAO.PegarTodos();
        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }
        return lista;
    }

    private bool Validar(Tema tema)
    {
        if (tema.GetNome() == null || tema.GetNome().Trim().Equals(""))
        {
            throw new ExcecaoSAG("Nome completo deve ser preenchido");
        }
        if (tema.GetDescricao() == null || tema.GetDescricao().Trim().Equals(""))
        {
            throw new ExcecaoSAG("Descricao deve ser preenchido");
        }
        if (tema.GetSerie() == null || tema.GetSerie().Trim().Equals(""))
        {
            throw new ExcecaoSAG("Serie deve ser preenchida");
        }
        if (tema.GetMatId() <= 0)
        {
            throw new ExcecaoSAG("Materia deve ser preenchida");
        }
        return true;
    }

    public void Incluir(Tema tema)
    {
        try
        {
            if (Validar(tema))
            {
                temaDAO.Incluir(tema);
            }
        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }

    }

    public void Alterar(Tema tema)
    {
        try
        {
            if (Validar(tema))
            {
                temaDAO.Alterar(tema);
            }

        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }

    }

    public void Excluir(Tema tema)
    {
        try
        {
            temaDAO.Excluir(tema);
        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }

    }

    public void Carregar(Tema tema)
    {
        try
        {
            temaDAO.Carregar(tema);
        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }
    }
}

