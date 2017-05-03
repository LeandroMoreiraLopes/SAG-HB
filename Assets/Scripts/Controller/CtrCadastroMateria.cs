using System.Collections.Generic;

public class CtrCadastroMateria
{

    static CtrCadastroMateria umCtrCadastroMateria;
    MateriaDAO materiaDAO;

    public CtrCadastroMateria()
    {
        DAOFactory daoFactory = new DAOFactory();
        materiaDAO = daoFactory.getMateriaDAO();
    }

    public static CtrCadastroMateria getInstance()
    {
        if (umCtrCadastroMateria == null)
        {
            umCtrCadastroMateria = new CtrCadastroMateria();
        }
        return umCtrCadastroMateria;
    }

    public List<Materia> ListarTodos()
    {
        List<Materia> lista = new List<Materia>();
        try
        {
            lista = materiaDAO.PegarTodos();
        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }
        return lista;
    }

    private bool Validar(Materia materia)
    {
        if (materia.GetNome() == null || materia.GetNome().Trim().Equals(""))
        {
            throw new ExcecaoSAG("Nome completo deve ser preenchido");
        }
        if (materia.GetDescricao() == null || materia.GetDescricao().Trim().Equals(""))
        {
            throw new ExcecaoSAG("CPF deve ser preenchido");
        }
        return true;
    }

    public void Incluir(Materia materia)
    {
        try
        {
            if (Validar(materia))
            {
                materiaDAO.Incluir(materia);
            }
        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }

    }

    public void Alterar(Materia materia)
    {
        try
        {
            if (Validar(materia))
            {
                materiaDAO.Alterar(materia);
            }

        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }

    }

    public void Excluir(Materia materia)
    {
        try
        {
            materiaDAO.Excluir(materia);
        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }

    }

    public void Carregar(Materia materia)
    {
        try
        {
            materiaDAO.Carregar(materia);
        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }
    }
}
