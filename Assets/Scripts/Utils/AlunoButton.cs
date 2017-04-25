using UnityEngine;

public class AlunoButton : MonoBehaviour {

    public void Clique()
    {
        Camera.main.GetComponent<AlunoView>().Clique(gameObject.name);
    }
}
