using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscilacaoDosBarcos : MonoBehaviour {

    int angle;
    [SerializeField]
    float amplitude;

    [SerializeField]
    CameraController camController;

    bool goingDown;

    [SerializeField]
    GameObject fumaca;

    Vector3 posicaoInicial;

    [SerializeField]
    AudioSource boom;

	// Use this for initialization
	void Start () {
        posicaoInicial = transform.position;
        StartCoroutine(Movement());
	}

    public void ReStart()
    {
        posicaoInicial = transform.position;
        StartCoroutine(Movement());
    }

    IEnumerator Movement()
    {
        while (true)
        {
            angle++;
            transform.position += Vector3.up * Mathf.Sin(angle/2) * amplitude;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Update()
    {
        if (goingDown)
            transform.position -= Vector3.up * Time.deltaTime*0.1f;
    }

    private void OnMouseDown()
    {
        Invoke("Destroindo", 2f);
        GameObject temp = Instantiate(fumaca, transform.position, transform.rotation) as GameObject;
        Destroy(temp, 3f);
        boom.Play();
        goingDown = true;
        camController.Shake();
    }

    void Destroindo()
    {
        goingDown = false;
        transform.position = posicaoInicial;
        gameObject.SetActive(false);
    }
}
