using System.Collections.Generic;

public class CtrCadastroPergunta {

    static CtrCadastroPergunta umCadastroPergunta;
    PerguntaDAO perguntaDAO;

    public CtrCadastroPergunta()
    {
        DAOFactory daoFactory = new DAOFactory();
        perguntaDAO = daoFactory.getPerguntaDAO();
    }

    public static CtrCadastroPergunta getInstance()
    {
        if (umCadastroPergunta == null)
        {
            umCadastroPergunta = new CtrCadastroPergunta();
        }
        return umCadastroPergunta;
    }

    public List<Pergunta> ListarTodos()
    {
        List<Pergunta> lista = new List<Pergunta>();
        try
        {
            lista = perguntaDAO.PegarTodos();
        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }
        return lista;
    }

    public List<Pergunta> ListarTodosPorTema(int tema_id)
    {
        List<Pergunta> lista = new List<Pergunta>();
        try
        {
            lista = perguntaDAO.PegarTodosPorTema(tema_id);
        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }
        return lista;
    }

    private bool Validar(Pergunta pergunta)
    {
        if (pergunta.GetDescricao() == null || pergunta.GetDescricao().Trim().Equals(""))
        {
            throw new ExcecaoSAG("Descricao deve ser preenchido");
        }
        if (pergunta.GetCorreta() == null || pergunta.GetCorreta().Trim().Equals(""))
        {
            throw new ExcecaoSAG("Resposta correta deve ser preenchido");
        }
        if (pergunta.GetErrada1() == null || pergunta.GetErrada1().Trim().Equals(""))
        {
            throw new ExcecaoSAG("Resposta errada 1 deve ser preenchido");
        }
        if (pergunta.GetErrada2() == null || pergunta.GetErrada2().Trim().Equals(""))
        {
            throw new ExcecaoSAG("Resposta errada 2 deve ser preenchido");
        }
        if (pergunta.GetErrada3() == null || pergunta.GetErrada3().Trim().Equals(""))
        {
            throw new ExcecaoSAG("Resposta errada 3 deve ser preenchido");
        }
        if (pergunta.GetDificuldade() < 0 || pergunta.GetDificuldade() > 3)
        {
            throw new ExcecaoSAG("Dificuldade deve ser preenchida entre 1 e 3");
        }
        if (pergunta.GetFuncId() <= 0)
        {
            throw new ExcecaoSAG("Funcionário deve ser preenchid0");
        }
        if (pergunta.GetTemaId() <= 0)
        {
            throw new ExcecaoSAG("Tema deve ser preenchido");
        }
        return true;
    }

    public void Incluir(Pergunta pergunta)
    {
        try
        {
            if (Validar(pergunta))
            {
                perguntaDAO.Incluir(pergunta);
            }
        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }

    }

    public void Alterar(Pergunta pergunta)
    {
        try
        {
            if (Validar(pergunta))
            {
                perguntaDAO.Alterar(pergunta);
            }

        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }

    }

    public void Excluir(Pergunta pergunta)
    {
        try
        {
            perguntaDAO.Excluir(pergunta);
        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }

    }

    public void Carregar(Pergunta pergunta)
    {
        try
        {
            perguntaDAO.Carregar(pergunta);
        }
        catch (ExcecaoSAG ex)
        {
            throw new ExcecaoSAG(ex.getMsg());
        }
    }
}

