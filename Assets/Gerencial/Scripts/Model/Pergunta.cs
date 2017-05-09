public class Pergunta {

    int id, dificuldade, funcId, temaId;
    string descricao, correta, errada1, errada2, errada3;
    
    public int GetId()
    {
        return id;
    }

    public void SetId(int i)
    {
        id = i;
    }

    public string GetDescricao()
    {
        return descricao;
    }

    public void SetDescricao(string s)
    {
        descricao = s;
    }

    public string GetCorreta()
    {
        return correta;
    }

    public void SetCorreta(string s)
    {
        correta = s;
    }

    public string GetErrada1()
    {
        return errada1;
    }

    public void SetErrada1(string s)
    {
        errada1 = s;
    }

    public string GetErrada2()
    {
        return errada2;
    }

    public void SetErrada2(string s)
    {
        errada2 = s;
    }

    public string GetErrada3()
    {
        return errada3;
    }

    public void SetErrada3(string s)
    {
        errada3 = s;
    }

    public int GetDificuldade()
    {
        return dificuldade;
    }

    public void SetDificuldade(int i)
    {
        dificuldade = i;
    }

    public int GetFuncId()
    {
        return funcId;
    }

    public void SetFuncId(int i)
    {
        funcId = i;
    }

    public int GetTemaId()
    {
        return temaId;
    }

    public void SetTemaId(int i)
    {
        temaId = i;
    }
}
