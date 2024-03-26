using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public Transform bird;
    private bool sawPlayer = false;
    public Animator birdAnimator;

    public Vector3 startPosition;
    public Vector3 endPosition;
    public float duration = 10f;
    public float elapsed;


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            //print("Bird saw player!!");
            this.sawPlayer = true;
            this.birdAnimator.SetTrigger("sawPlayer");
        }

    }

    private void setColor()
    {
        float R = Random.Range(0.0f, 1.0f);
        float G = Random.Range(0.0f, 1.0f);
        float B = Random.Range(0.0f, 1.0f);
        this.bird.transform.Find("Head").GetComponent<SpriteRenderer>().color = new Color(R, G, B);
        this.bird.transform.Find("Neck").GetComponent<SpriteRenderer>().color = new Color(R, G, B);
        this.bird.transform.Find("Body").GetComponent<SpriteRenderer>().color = new Color(R, G, B);
        this.bird.transform.Find("Tail").GetComponent<SpriteRenderer>().color = new Color(R, G, B);
        this.bird.transform.Find("F_Wing").GetComponent<SpriteRenderer>().color = new Color(R, G, B);
        this.bird.transform.Find("B_Wing").GetComponent<SpriteRenderer>().color = new Color(R, G, B);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.birdAnimator = transform.parent.GetComponent<Animator>();
        this.birdAnimator.speed = Random.Range(0.5f, 1.5f);
        this.bird = transform.parent;
        this.startPosition = bird.position;
        if (bird.localScale.x >= 0)
        {
            this.endPosition = new Vector3(bird.position.x - Random.Range(100.0f, 150.0f), 100.0f, 0.0f);
        }
        else if (bird.localScale.x <= 0)
        {
            this.endPosition = new Vector3(bird.position.x + Random.Range(100.0f, 150.0f), 100.0f, 0.0f);
        }
        setColor();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.sawPlayer == true)
        {
            this.elapsed += Time.deltaTime;
            float totalTime = elapsed / duration;
            bird.position = Vector3.Lerp(bird.position, this.endPosition, totalTime);
            if (this.elapsed >= 1)
            {
                bird.gameObject.SetActive(false);
                Destroy(bird.gameObject);
            }
        }
        
    }
}
