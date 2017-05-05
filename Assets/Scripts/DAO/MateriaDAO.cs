using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

public class MateriaDAO {

    public List<Materia> PegarTodos()
    {
        List<Materia> materias = new List<Materia>();
        Materia umaMateria;
        DAOFactory daoFactory = new DAOFactory();

        //Conexão
        MySqlConnection db = Connection.getConnection();

        try
        {
            MySqlCommand mySQLcmd = db.CreateCommand();

            //setando a procedure do banco
            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "Materia_PegarTodos";

            //execução sem retorno
            MySqlDataReader rsMateria = mySQLcmd.ExecuteReader();

            //se há linhas
            if (rsMateria.HasRows)
            {
                //enquanto lê cada linha
                while (rsMateria.Read())
                {
                    //criando um aluno para cada linha
                    umaMateria = new Materia();
                    umaMateria.SetId(rsMateria.GetInt32("id"));
                    umaMateria.SetNome(rsMateria.GetString("nome"));
                    umaMateria.SetDescricao(rsMateria.GetString("descricao"));
                    
                    materias.Add(umaMateria);
                }
            }
            else
            {
                //sem resultados
            }
        }
        catch (MySqlException ex)
        {
            //rollback caso haja erro no MySQL
            //mySQLTransaction.Rollback();
            throw new ExcecaoSAG("Erro ao listar as matérias. Código " + ex.ToString());
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
        return materias;
    }

    public void Incluir(Materia materia)
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
            mySQLcmd.CommandText = "Materia_Inserir";

            //preenchendo os parametros da procedure
            mySQLcmd.Parameters.AddWithValue("LOC_NOME", materia.GetNome());
            mySQLcmd.Parameters.AddWithValue("LOC_DESCRICAO", materia.GetDescricao());
           
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
                throw new ExcecaoSAG("Erro na inclusão da Materia. Código " + ex1.ToString());
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
                throw new ExcecaoSAG("Erro na inclusão da Materia. Código " + ex1.ToString());
            }
            throw ex;
        }
        finally
        {
            db.Close();
        }

    }

    public void Alterar(Materia materia)
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
            mySQLcmd.CommandText = "Materia_Alterar";

            //preenchendo os parametros da procedure
            mySQLcmd.Parameters.AddWithValue("LOC_ID", materia.GetId());
            mySQLcmd.Parameters.AddWithValue("LOC_NOME", materia.GetNome());
            mySQLcmd.Parameters.AddWithValue("LOC_DESCRICAO", materia.GetDescricao());
 
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
            throw new ExcecaoSAG("Erro na alteração da materia. Código " + ex.ToString());
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

    public void Excluir(Materia materia)
    {
        MySqlConnection db = Connection.getConnection();
        MySqlTransaction mySQLTransaction;
        mySQLTransaction = db.BeginTransaction();

        try
        {
            MySqlCommand mySQLcmd = db.CreateCommand();

            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "Materia_Excluir";

            //preenchendo os parametros da procedure
            mySQLcmd.Parameters.AddWithValue("LOC_ID", materia.GetId());

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
                throw new ExcecaoSAG("Erro na exclusão da materia. Código " + ex1.ToString());
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
                throw new ExcecaoSAG("Erro na exclusão da materia. Código " + ex1.ToString());
            }
            throw ex;
        }
        finally
        {
            db.Close();
        }
    }

    public void Carregar(Materia umaMateria)
    {
        MySqlConnection db = Connection.getConnection();
        MySqlTransaction mySQLTransaction;
        mySQLTransaction = db.BeginTransaction();

        try
        {

            MySqlCommand mySQLcmd = db.CreateCommand();

            //setando a procedure do banco
            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "Materia_Carregar";

            mySQLcmd.Parameters.AddWithValue("LOC_ID", umaMateria.GetId());

            //ligando a transação
            mySQLcmd.Transaction = mySQLTransaction;

            //execução sem retorno
            MySqlDataReader rsMateria = mySQLcmd.ExecuteReader();

            //se há linhas
            if (rsMateria.HasRows)
            {
                while (rsMateria.Read())
                {
                    umaMateria.SetNome(rsMateria.GetString("nome"));
                    umaMateria.SetDescricao(rsMateria.GetString("descricao"));
                }
            }
            else
            {
                //aluno não carregado
                throw new ExcecaoSAG("Erro, Materia não encontrada.");
            }
        }
        catch (MySqlException ex)
        {
            throw new ExcecaoSAG("Erro ao carregar uma materia. Código " + ex.ToString());
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
