
using System.Data;
using MySql.Data.MySqlClient;

public class LoginDAO {

    public int Login(Aluno umaPessoa)
    {
        MySqlConnection db = Connection.getConnection();
                
        try
        {
            string sql = string.Format("select * from aluno where usuario = '{0}' and senha = '{1}';", umaPessoa.GetUsuario(), umaPessoa.GetSenha());
            MySqlCommand mySQLcmd = db.CreateCommand();

            mySQLcmd.CommandText = sql;
           
            //execução sem retorno
            MySqlDataReader rsAluno = mySQLcmd.ExecuteReader();

            //se há linhas
            if (rsAluno.HasRows)
                {
                    return 1;
                }

            else
            {
                db.Close();
                db = Connection.getConnection();

                sql = string.Format("select * from funcionario where usuario = '{0}' and senha = '{1}';", umaPessoa.GetUsuario(), umaPessoa.GetSenha());
                mySQLcmd = db.CreateCommand();

                mySQLcmd.CommandText = sql;

                //execução sem retorno
                MySqlDataReader rsFuncionario = mySQLcmd.ExecuteReader();

                //se há linhas
                if (rsFuncionario.HasRows)
                {
                    return 2;
                }

                else
                {
                    return 0;
                }
            }
        }
        catch (MySqlException ex)
        {
            throw new ExcecaoSAG("Erro ao logar um usuário. Código " + ex.ToString());
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
