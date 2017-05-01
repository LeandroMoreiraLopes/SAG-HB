using UnityEngine;

public class AlunoButton : MonoBehaviour {

    public void Clique()
    {
        gameObject.GetComponentInParent<AlunoView>().Clique(gameObject.name);
    }
}
