using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public Rigidbody2D hook;
    public float releaseTime = .15f;
    public float maxDragDistance = 2f;

    public GameObject nextBall;

    private bool isPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed == true)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector3.Distance(mousePos, hook.position) > maxDragDistance)
            {
                rb.position = hook.position + (mousePos - hook.position).normalized * maxDragDistance;
            }
            else
            {
                rb.position = mousePos;
            }
        }
    }

    private void OnMouseDown()
    {
        isPressed = true;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnMouseUp()
    {
        isPressed = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        StartCoroutine(Release());
    }

    private IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime);

        GetComponent<SpringJoint2D>().enabled = false;
        this.enabled = false;

        yield return new WaitForSeconds(2f);

        if (nextBall != null)
        {
            nextBall.SetActive(true);
        }
        else
        {
            Enemy.EnemiesAlive = 0; // Otherwise the static variable is not reinitialised at Loadscene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
