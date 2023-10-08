using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEffect : MonoBehaviour
{

    [SerializeField] private SpriteRenderer spriteRender;
    [SerializeField] private float lifeTime;
    [SerializeField] private float speed;
    [SerializeField] private float scaleSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Color col = spriteRender.color;
        col.a = 1.0f;
        spriteRender.color = col;
        StartCoroutine(SurviveFor(lifeTime));
        StartCoroutine(SpawnResize());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
    }

    private IEnumerator SurviveFor (float time)
    {
        float timer = 0f;

        while(timer <= time)
        {
            timer += Time.deltaTime;

            Color col = spriteRender.color;
            col.a = (time - timer) / time;
            spriteRender.color = col;

            yield return null;
        }
        Destroy(this.gameObject);
    }

    private IEnumerator SpawnResize()
    {
        float initScale = transform.localScale.x;
        transform.localScale = Vector3.zero;

        while (transform.localScale.x < initScale)
        {
            transform.localScale += new Vector3(scaleSpeed * Time.deltaTime, scaleSpeed * Time.deltaTime, scaleSpeed * Time.deltaTime);

            yield return null;
        }
        transform.localScale = new Vector3 (initScale, initScale, initScale);
    }
}
