using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    public int selected;
    public Transform[] items;
    public float[] valoresReales;
    public int random, premios;
    public bool turning;
    public float range;
    public float first, last;
    private int num;

    private void Start()
    {
        first = items[0].localPosition.y;
        last = items[items.Length - 1].localPosition.y;
    }
    public IEnumerator Turn()
    {
        num = 0;
        turning = true;
        random = Random.Range(0, items.Length - 1);
        if (random < premios)
        {
            selected = random;
        }
        else if (random < premios * 2)
        {
            selected = (random - premios);
        }
        else
        {
            selected = (random - premios * 2);
        }

        yield return null;

        while (num < 10)
        {
            while (transform.position.y > last)
            {
                transform.position = new Vector3(transform.position.x, (transform.position.y - range), 0);
                yield return new WaitForSeconds(0.025f);
            }

            transform.position = new Vector2(transform.position.x, (first + range));
            num++;
            yield return null;
        }

        transform.position = new Vector2(transform.position.x, last);
        yield return null;

        while (transform.position.y != items[random].localPosition.y)
        {
            if (transform.position.y < (last + range))
            {
                transform.position = new Vector3(transform.position.x, (first + range), 0);
            }
            transform.position = new Vector3(transform.position.x, (transform.position.y - range), 0);
            yield return new WaitForSeconds(0.025f);
        }
        transform.position = new Vector3(transform.position.x, valoresReales[random], 0);

        if (random < premios)
        {
            selected = random;
        }
        else if (random < premios * 2)
        {
            selected = (random - premios);
        }
        else
        {
            selected = (random - premios * 2);
        }

        turning = false;
    }
}
