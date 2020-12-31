using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallControllerScript : MonoBehaviour
{
    Transform tr1, tr2, cam;
    float speed = 3f;
    Vector2 startPosition = new Vector2(0, 10);
    public int health = 3;
    GameObject[] hearts;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Transform>();
        wallsStartPosition();
        // получаем первую стену
        tr1 = getRandomWall();
        hearts = GameObject.FindGameObjectsWithTag("hearts");
    }

    void Update()
    {
        heartsRender();
        if (tr1 != null)
        {
            // если позиция первой стены ниже позиции камеры и нет второй стены
            if (tr1.position.y <= cam.position.y && tr2 == null)
            {
                // получаем вторую стену
                tr2 = getRandomWall();
            }
        }

        if (tr2 != null)
        {
            // если позиция второй стены ниже позиции камеры
            if (tr2.position.y <= cam.position.y)
            {
                // возвращаем первую стену на начальную позицию
                tr1.position = startPosition;

                // меняем местами ссылки на стены
                tr1 = tr2;
                // обнуляем вторую стену
                tr2 = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            stopGame();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // если получена первая стена, двигаем стену
        if (tr1 != null) moveWall(tr1);

        // если получена вторая стена, двигаем стену
        if (tr2 != null) moveWall(tr2);
    }

    private Transform getRandomWall()
    {
        Transform b;
        do {
            b = GameObject.Find("wall" + Random.Range(1, 5)).GetComponent<Transform>();
        }
        while (b == tr1 || b == tr1);

        return b;
    }

    private void moveWall(Transform tr) {
        tr.Translate(Vector2.down.normalized * speed * Time.fixedDeltaTime);
    }

    private void wallsStartPosition() {
        List<Transform> walls = new List<Transform>();
        GameObject.Find("walls").GetComponentsInChildren<Transform>(walls);
        walls.Sort((x, y) => string.Compare(y.name, x.name));

        foreach (Transform t in walls)
        {
            t.transform.position = startPosition;
        }
    }

    private void heartsRender()
    {
        int c = 1;
        foreach(GameObject g in hearts)
        {
            g.GetComponent<SpriteRenderer>().enabled = false;
            if(c <= health) g.GetComponent<SpriteRenderer>().enabled = true;
            c++;
        }
    }

    private void stopGame() {
        tr1 = null;
        tr2 = null;
        wallsStartPosition();
    }
}
