using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DAOFactory {

    // Aqui entram as implementa��es dos m�todos abstratos 
    // especificados em DAOFactory.

    /*public CursoDAO getCursoDAO()
    {
        return new CursoDAO();
    }

    public DisciplinaDAO getDisciplinaDAO()
    {
        return new DisciplinaDAO();
    }*/

    public AlunoDAO getAlunoDAO()
    {
        return new AlunoDAO();
    }

    /*public ProfessorDAO getProfessorDAO()
    {
        return new ProfessorDAO();
    }

    public TurmaDAO getTurmaDAO()
    {
        return new TurmaDAO();
    }

    public AvaliacaoDAO getAvaliacaoDAO()
    {
        return new AvaliacaoDAO();
    }

    public SalaDAO getSalaDAO()
    {
        return new SalaDAO();
    }*/
}
