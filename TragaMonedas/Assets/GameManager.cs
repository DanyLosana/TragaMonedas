using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Row[] rows;
    public int[] premiosNoJackpot;
    private bool check, clicked;
    public Transform handle;
    public Image ganador;
    public Sprite[] ganadores;
    public Sprite perdedor, dosPar;
    public AudioSource AS;
    public AudioClip machine, price;
    public int jackpotSlot;
    private void Start()
    {
        ganador.gameObject.SetActive(false);
        check = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!rows[0].turning && !rows[1].turning && !rows[2].turning && !check)
        {
            StartCoroutine(CheckScore());
        }

        if (Input.GetMouseButtonDown(0) && !clicked)
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if(hit.collider.gameObject == handle.gameObject)
                {
                    clicked = true;
                }                
            }
        }
        if(Input.GetMouseButtonUp(0) && clicked)
        {
            clicked = false;
            StartCoroutine(PullHandle());
        }
    }

    private IEnumerator CheckScore()
    {
        check = true;
        AS.Stop();
        AS.PlayOneShot(price);
        yield return new WaitForSeconds(1f);
        if(rows[0].selected == rows[1].selected && rows[1].selected == rows[2].selected)
        {
            ganador.sprite = ganadores[rows[0].selected];
            if(rows[0].selected == jackpotSlot)
            {
                print("jackpot");
            }
            else
            {
                print("ganador no jackpot");
            }
        }
        else if (rows[0].selected == rows[1].selected || rows[1].selected == rows[2].selected || rows[0].selected == rows[2].selected)
        {
            ganador.sprite = dosPar;
        }
        else
        {
            ganador.sprite = perdedor;
            print("perdedor");
        }
        ganador.gameObject.SetActive(true);
    }

    public void Turn()
    {
        AS.loop = true;
        AS.clip = machine;
        AS.Play();
        check = false;
        if (!rows[0].turning && !rows[1].turning && !rows[2].turning)
        {
            StartCoroutine(rows[0].Turn());
            StartCoroutine(rows[1].Turn());
            StartCoroutine(rows[2].Turn());
            check = false;
        }
        int rand = Random.Range(0, 100);
        if (rand < 71)
        {
            if (rows[0].selected == rows[1].selected && rows[1].selected == rows[2].selected)
            {
                print("cambio");
                if(rows[0].random > rows[0].items.Length - 2)
                {
                    rows[0].random = 0;
                }
                else
                {
                    rows[0].random++;
                }
            }
        }
        else if (rand < 91 && rand > 70)
        {
            int r = Random.Range(0, premiosNoJackpot.Length - 1);
            rows[0].random = rows[premiosNoJackpot[r]].random;
            rows[1].random = rows[premiosNoJackpot[r]].random;
            rows[2].random = rows[premiosNoJackpot[r]].random;
        }
        else if (rand > 90)
        {
            rows[0].random = jackpotSlot;
            rows[1].random = jackpotSlot;
            rows[2].random = jackpotSlot;
        }
    }

    private IEnumerator PullHandle()
    {
        for(int i=0; i < 15; i += 5)
        {
            handle.Rotate(0f, 0f, i);
            yield return new WaitForSeconds(0.1f);
        }

        Turn();
        for (int i = 0; i < 15; i += 5)
        {
            handle.Rotate(0f, 0f, -i);
            yield return new WaitForSeconds(0.1f);
        }

    }
}
