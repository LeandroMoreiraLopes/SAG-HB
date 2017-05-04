using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

public class PerguntaDAO {

    public List<Pergunta> PegarTodos()
    {
        List<Pergunta> perguntas = new List<Pergunta>();
        Pergunta umaPergunta;
        DAOFactory daoFactory = new DAOFactory();

        //Conexão
        MySqlConnection db = Connection.getConnection();

        try
        {
            MySqlCommand mySQLcmd = db.CreateCommand();

            //setando a procedure do banco
            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "Pergunta_PegarTodos";

            //execução sem retorno
            MySqlDataReader rsPergunta = mySQLcmd.ExecuteReader();

            //se há linhas
            if (rsPergunta.HasRows)
            {
                //enquanto lê cada linha
                while (rsPergunta.Read())
                {
                    //criando um aluno para cada linha
                    umaPergunta = new Pergunta();
                    umaPergunta.SetId(rsPergunta.GetInt32("id"));
                    umaPergunta.SetDescricao(rsPergunta.GetString("descricao"));
                    umaPergunta.SetCorreta(rsPergunta.GetString("correta"));
                    umaPergunta.SetErrada1(rsPergunta.GetString("errada1"));
                    umaPergunta.SetErrada2(rsPergunta.GetString("errada2"));
                    umaPergunta.SetErrada3(rsPergunta.GetString("errada3"));
                    umaPergunta.SetDificuldade(rsPergunta.GetInt32("dificuldade"));
                    umaPergunta.SetSimulado(rsPergunta.GetBoolean("simulado"));
                    umaPergunta.SetFuncId(rsPergunta.GetInt32("funcionario_id"));
                    umaPergunta.SetTemaId(rsPergunta.GetInt32("tema_id"));

                    perguntas.Add(umaPergunta);
                }
            }
            else
            {
                //sem resultados
            }
        }
        catch (MySqlException ex)
        {
            throw new ExcecaoSAG("Erro ao listar as perguntas. Código " + ex.ToString());
        }
        catch (ExcecaoSAG ex)
        {
            throw ex;
        }
        finally
        {
            db.Close();
        }
        //retorna a lista de alunos
        return perguntas;
    }

    public void Incluir(Pergunta pergunta)
    {
        //conexão
        MySqlConnection db = Connection.getConnection();

        //transação
        MySqlTransaction mySQLTransaction;
        mySQLTransaction = db.BeginTransaction();

        try
        {
            //comando na conexão para execução da procedure
            MySqlCommand mySQLcmd = db.CreateCommand();

            //setando a procedure do banco
            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "Pergunta_Inserir";

            //preenchendo os parametros da procedure
            mySQLcmd.Parameters.AddWithValue("LOC_DESCRICAO", pergunta.GetDescricao());
            mySQLcmd.Parameters.AddWithValue("LOC_CORRETA", pergunta.GetCorreta());
            mySQLcmd.Parameters.AddWithValue("LOC_ERRADA1", pergunta.GetErrada1());
            mySQLcmd.Parameters.AddWithValue("LOC_ERRADA2", pergunta.GetErrada2());
            mySQLcmd.Parameters.AddWithValue("LOC_ERRADA3", pergunta.GetErrada3());
            mySQLcmd.Parameters.AddWithValue("LOC_DIFICULDADE", pergunta.GetDificuldade());
            mySQLcmd.Parameters.AddWithValue("LOC_SIMULADO", pergunta.GetSimulado());
            mySQLcmd.Parameters.AddWithValue("LOC_FUNCIONARIO_ID", pergunta.GetFuncId());
            mySQLcmd.Parameters.AddWithValue("LOC_TEMA_ID", pergunta.GetTemaId());

            //ligando a transação
            mySQLcmd.Transaction = mySQLTransaction;

            //execução sem retorno
            mySQLcmd.ExecuteNonQuery();

            //commit da transação
            mySQLTransaction.Commit();
        }
        catch (MySqlException ex)
        {
            try
            {
                //rollback caso haja erro no MySQL
                mySQLTransaction.Rollback();
            }
            catch (MySqlException ex1)
            {
                throw new ExcecaoSAG("Erro na inclusão da pergunta. Código " + ex1.ToString());
            }
        }
        catch (ExcecaoSAG ex)
        {
            try
            {
                //rollback caso haja erro na aplicação
                mySQLTransaction.Rollback();
            }
            catch (MySqlException ex1)
            {
                throw new ExcecaoSAG("Erro na inclusão da pergunta. Código " + ex1.ToString());
            }
            throw ex;
        }
        finally
        {
            db.Close();
        }

    }

    public void Alterar(Pergunta pergunta)
    {
        //conexão
        MySqlConnection db = Connection.getConnection();

        //transação
        MySqlTransaction mySQLTransaction;
        mySQLTransaction = db.BeginTransaction();

        try
        {
            //comando na conexão para execução da procedure
            MySqlCommand mySQLcmd = db.CreateCommand();

            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "Pergunta_Alterar";

            //preenchendo os parametros da procedure
            mySQLcmd.Parameters.AddWithValue("LOC_ID", pergunta.GetId());
            mySQLcmd.Parameters.AddWithValue("LOC_DESCRICAO", pergunta.GetDescricao());
            mySQLcmd.Parameters.AddWithValue("LOC_CORRETA", pergunta.GetCorreta());
            mySQLcmd.Parameters.AddWithValue("LOC_ERRADA1", pergunta.GetErrada1());
            mySQLcmd.Parameters.AddWithValue("LOC_ERRADA2", pergunta.GetErrada2());
            mySQLcmd.Parameters.AddWithValue("LOC_ERRADA3", pergunta.GetErrada3());
            mySQLcmd.Parameters.AddWithValue("LOC_DIFICULDADE", pergunta.GetDificuldade());
            mySQLcmd.Parameters.AddWithValue("LOC_SIMULADO", pergunta.GetSimulado());
            mySQLcmd.Parameters.AddWithValue("LOC_FUNCIONARIO_ID", pergunta.GetFuncId());
            mySQLcmd.Parameters.AddWithValue("LOC_TEMA_ID", pergunta.GetTemaId());

            //ligando a transação
            mySQLcmd.Transaction = mySQLTransaction;

            //execução sem retorno
            mySQLcmd.ExecuteNonQuery();

            //commit da transação
            mySQLTransaction.Commit();
        }
        catch (MySqlException ex)
        {
            mySQLTransaction.Rollback();
            throw new ExcecaoSAG("Erro na alteração da pergunta. Código " + ex.ToString());
        }
        catch (ExcecaoSAG ex)
        {
            mySQLTransaction.Rollback();
            throw ex;
        }
        finally
        {
            db.Close();
        }
    }

    public void Excluir(Pergunta pergunta)
    {
        MySqlConnection db = Connection.getConnection();
        MySqlTransaction mySQLTransaction;
        mySQLTransaction = db.BeginTransaction();

        try
        {
            MySqlCommand mySQLcmd = db.CreateCommand();

            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "Pergunta_Excluir";

            //preenchendo os parametros da procedure
            mySQLcmd.Parameters.AddWithValue("LOC_ID", pergunta.GetId());

            //ligando a transação
            mySQLcmd.Transaction = mySQLTransaction;

            //execução sem retorno
            mySQLcmd.ExecuteNonQuery();

            //commit da transação
            mySQLTransaction.Commit();
        }
        catch (MySqlException ex)
        {
            try
            {
                //rollback caso haja erro no MySQL
                mySQLTransaction.Rollback();
            }
            catch (MySqlException ex1)
            {
                throw new ExcecaoSAG("Erro na exclusão da pergunta. Código " + ex1.ToString());
            }
        }
        catch (ExcecaoSAG ex)
        {
            try
            {
                //rollback caso haja erro na aplicação
                mySQLTransaction.Rollback();
            }
            catch (MySqlException ex1)
            {
                throw new ExcecaoSAG("Erro na exclusão da pergunta. Código " + ex1.ToString());
            }
            throw ex;
        }
        finally
        {
            db.Close();
        }
    }

    public void Carregar(Pergunta umaPergunta)
    {
        MySqlConnection db = Connection.getConnection();
        MySqlTransaction mySQLTransaction;
        mySQLTransaction = db.BeginTransaction();

        try
        {

            MySqlCommand mySQLcmd = db.CreateCommand();

            //setando a procedure do banco
            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "Pergunta_Carregar";

            mySQLcmd.Parameters.AddWithValue("LOC_ID", umaPergunta.GetId());

            //ligando a transação
            mySQLcmd.Transaction = mySQLTransaction;

            //execução sem retorno
            MySqlDataReader rsPergunta = mySQLcmd.ExecuteReader();

            //se há linhas
            if (rsPergunta.HasRows)
            {
                while (rsPergunta.Read())
                {
                    umaPergunta.SetDescricao(rsPergunta.GetString("descricao"));
                    umaPergunta.SetCorreta(rsPergunta.GetString("correta"));
                    umaPergunta.SetErrada1(rsPergunta.GetString("errada1"));
                    umaPergunta.SetErrada2(rsPergunta.GetString("errada2"));
                    umaPergunta.SetErrada3(rsPergunta.GetString("errada3"));
                    umaPergunta.SetDificuldade(rsPergunta.GetInt32("dificuldade"));
                    umaPergunta.SetSimulado(rsPergunta.GetBoolean("simulado"));
                    umaPergunta.SetFuncId(rsPergunta.GetInt32("funcionario_id"));
                    umaPergunta.SetTemaId(rsPergunta.GetInt32("tema_id"));
                }
            }
            else
            {
                //aluno não carregado
                throw new ExcecaoSAG("Erro, Pergunta não encontrado.");
            }
        }
        catch (MySqlException ex)
        {
            throw new ExcecaoSAG("Erro ao carregar uma Pergunta. Código " + ex.ToString());
        }
        catch (ExcecaoSAG ex)
        {
            throw ex;
        }
        finally
        {
            db.Close();
        }
    }

}