using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

public class FuncionarioDAO {

    public List<Funcionario> PegarTodos()
    {
        List<Funcionario> funcionarios = new List<Funcionario>();
        Funcionario umFuncionario;
        DAOFactory daoFactory = new DAOFactory();

        //Conexão
        MySqlConnection db = Connection.getConnection();

        //transação
        //MySqlTransaction mySQLTransaction;
        //mySQLTransaction = db.BeginTransaction();

        try
        {
            MySqlCommand mySQLcmd = db.CreateCommand();

            //setando a procedure do banco
            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "Funcionario_PegarTodos";

            //mySQLcmd.Transaction = mySQLTransaction;

            //execução sem retorno
            MySqlDataReader rsFuncionario = mySQLcmd.ExecuteReader();

            //se há linhas
            if (rsFuncionario.HasRows)
            {
                //enquanto lê cada linha
                while (rsFuncionario.Read())
                {
                    //criando um aluno para cada linha
                    umFuncionario = new Funcionario();
                    umFuncionario.SetId(rsFuncionario.GetInt32("id"));
                    umFuncionario.SetMatricula(rsFuncionario.GetInt64("matricula"));
                    umFuncionario.SetNomeCompleto(rsFuncionario.GetString("nomecompleto"));
                    umFuncionario.SetNascimento(rsFuncionario.GetInt32("nascimento"));
                    umFuncionario.SetCpf(rsFuncionario.GetString("cpf"));
                    umFuncionario.SetTelefone(rsFuncionario.GetInt64("telefone"));
                    umFuncionario.SetCelular(rsFuncionario.GetInt64("celular"));
                    umFuncionario.SetUsuario(rsFuncionario.GetString("usuario"));
                    umFuncionario.SetSenha(rsFuncionario.GetString("senha"));
                    umFuncionario.SetEmail(rsFuncionario.GetString("email"));

                    funcionarios.Add(umFuncionario);
                }
            }
            else
            {
                //sem resultados
            }

            //commit da transação
            //mySQLTransaction.Commit();

        }
        catch (MySqlException ex)
        {
            //rollback caso haja erro no MySQL
            //mySQLTransaction.Rollback();
            throw new ExcecaoSAG("Erro ao listar os funcionarios. Código " + ex.ToString());
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
        return funcionarios;
    }

    public void Incluir(Funcionario funcionario)
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
            mySQLcmd.CommandText = "Funcionario_Inserir";

            //preenchendo os parametros da procedure
            mySQLcmd.Parameters.AddWithValue("LOC_MATRICULA", funcionario.GetMatricula());
            mySQLcmd.Parameters.AddWithValue("LOC_NOMECOMPLETO", funcionario.GetNomeCompleto());
            mySQLcmd.Parameters.AddWithValue("LOC_NASCIMENTO", funcionario.GetNascimento());
            mySQLcmd.Parameters.AddWithValue("LOC_CPF", funcionario.GetCpf());
            mySQLcmd.Parameters.AddWithValue("LOC_TELEFONE", funcionario.GetTelefone());
            mySQLcmd.Parameters.AddWithValue("LOC_CELULAR", funcionario.GetCelular());
            mySQLcmd.Parameters.AddWithValue("LOC_USUARIO", funcionario.GetUsuario());
            mySQLcmd.Parameters.AddWithValue("LOC_SENHA", funcionario.GetSenha());
            mySQLcmd.Parameters.AddWithValue("LOC_EMAIL", funcionario.GetEmail());

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
                throw new ExcecaoSAG("Erro na inclusão do funcionario. Código " + ex1.ToString());
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
                throw new ExcecaoSAG("Erro na inclusão do funcionario. Código " + ex1.ToString());
            }
            throw ex;
        }
        finally
        {
            db.Close();
        }

    }

    public void Alterar(Funcionario funcionario)
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
            mySQLcmd.CommandText = "Funcionario_Alterar";

            //preenchendo os parametros da procedure
            mySQLcmd.Parameters.AddWithValue("LOC_MATRICULA", funcionario.GetMatricula());
            mySQLcmd.Parameters.AddWithValue("LOC_NOMECOMPLETO", funcionario.GetNomeCompleto());
            mySQLcmd.Parameters.AddWithValue("LOC_NASCIMENTO", funcionario.GetNascimento());
            mySQLcmd.Parameters.AddWithValue("LOC_CPF", funcionario.GetCpf());
            mySQLcmd.Parameters.AddWithValue("LOC_TELEFONE", funcionario.GetTelefone());
            mySQLcmd.Parameters.AddWithValue("LOC_CELULAR", funcionario.GetCelular());
            mySQLcmd.Parameters.AddWithValue("LOC_USUARIO", funcionario.GetUsuario());
            mySQLcmd.Parameters.AddWithValue("LOC_SENHA", funcionario.GetSenha());
            mySQLcmd.Parameters.AddWithValue("LOC_EMAIL", funcionario.GetEmail());
            mySQLcmd.Parameters.AddWithValue("LOC_ID", funcionario.GetId());

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
            throw new ExcecaoSAG("Erro na alteração do funcionario. Código " + ex.ToString());
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

    public void Excluir(Funcionario funcionario)
    {
        MySqlConnection db = Connection.getConnection();
        MySqlTransaction mySQLTransaction;
        mySQLTransaction = db.BeginTransaction();

        try
        {
            MySqlCommand mySQLcmd = db.CreateCommand();

            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "Funcionario_Excluir";

            //preenchendo os parametros da procedure
            mySQLcmd.Parameters.AddWithValue("LOC_ID", funcionario.GetId());

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
                throw new ExcecaoSAG("Erro na exclusão do funcionario. Código " + ex1.ToString());
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
                throw new ExcecaoSAG("Erro na inclusão do funcionario. Código " + ex1.ToString());
            }
            throw ex;
        }
        finally
        {
            db.Close();
        }
    }

    public void Carregar(Funcionario umFuncionario)
    {
        MySqlConnection db = Connection.getConnection();
        MySqlTransaction mySQLTransaction;
        mySQLTransaction = db.BeginTransaction();

        try
        {

            MySqlCommand mySQLcmd = db.CreateCommand();

            //setando a procedure do banco
            mySQLcmd.CommandType = CommandType.StoredProcedure;
            mySQLcmd.CommandText = "Funcionario_Carregar";

            mySQLcmd.Parameters.AddWithValue("LOC_ID", umFuncionario.GetId());

            //ligando a transação
            mySQLcmd.Transaction = mySQLTransaction;

            //execução sem retorno
            MySqlDataReader rsFuncionario = mySQLcmd.ExecuteReader();

            //se há linhas
            if (rsFuncionario.HasRows)
            {
                while (rsFuncionario.Read())
                {
                    //criando um aluno para cada linha
                    umFuncionario.SetMatricula(rsFuncionario.GetInt64("matricula"));
                    umFuncionario.SetNomeCompleto(rsFuncionario.GetString("nomecompleto"));
                    umFuncionario.SetNascimento(rsFuncionario.GetInt32("nascimento"));
                    umFuncionario.SetCpf(rsFuncionario.GetString("cpf"));
                    umFuncionario.SetTelefone(rsFuncionario.GetInt64("telefone"));
                    umFuncionario.SetCelular(rsFuncionario.GetInt64("celular"));
                    umFuncionario.SetUsuario(rsFuncionario.GetString("usuario"));
                    umFuncionario.SetSenha(rsFuncionario.GetString("senha"));
                    umFuncionario.SetEmail(rsFuncionario.GetString("email"));
                }
            }
            else
            {
                //aluno não carregado
                throw new ExcecaoSAG("Erro, Funcionario não encontrado.");
            }
        }
        catch (MySqlException ex)
        {
            throw new ExcecaoSAG("Erro ao carregar um funcionario. Código " + ex.ToString());
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