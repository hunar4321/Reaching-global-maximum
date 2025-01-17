using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupOfAgents : MonoBehaviour
{
    Rect popRect;
    Rect dedRect;
    GUIStyle style;
    int population;
    int dead = 0;
    int previousAlive = 0;
    public GameObject agent;
    public int num = 300;
    public int xmin = 900;
    public int xmax = 1000;
    public int zmin = 124;
    public int zmax = 340;
    public int ymin = 15;
    public int ymax = 100;

    float color_range = 0.1f;

    //private GameObject[] agents;
    private void Awake()
    {
        Application.targetFrameRate = 30;
        //agents = new GameObject[num];
 
    }


    void Start()
    {
        popRect = new Rect(20, 20, 400, 100);
        //dedRect = new Rect(20, 40, 400, 100);
        style = new GUIStyle();
        style.fontSize = 18;
        StartCoroutine(CalculatePopulation());
        previousAlive = num;
        for (int i = 0; i < num; ++i)
        {
            
            int x = Random.Range(xmin, xmax);
            int y = Random.Range(ymin, ymax);
            int z = Random.Range(zmin, zmax);
            var agents = Instantiate(agent, new Vector3(x, y, z), Quaternion.identity);

            Color cc = agents.GetComponent<Renderer>().material.color;

            int toss = Random.Range(0, 2);
            Color new_color;

            //color_range = 0.2f;
            //float rcc = cc.r + Random.Range(-color_range, color_range);
            //float gcc = cc.g + Random.Range(-color_range, color_range);
            //rcc = Mathf.Clamp(rcc, 0, 1);
            //gcc = Mathf.Clamp(gcc, 0, 1);
            //if (toss == 0) { new_color = new Color(rcc, gcc, 0.7f, 0); }
            //else { new_color = new Color(cc.r, cc.g, 0.4f, 0); }

            if (toss == 0)
            {
                new_color = new Color(0f, 0f, 0.8f, 0); // liberal
            }
            else
            {
                new_color = new Color(0.8f, 0f, 0.0f, 0); // conservative
            }

            agents.GetComponent<Renderer>().material.SetColor("_Color", new_color);

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        //if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            var Players = GameObject.FindGameObjectsWithTag("Player");
            var numAlive = Players.Length;
            //dead += (previousAlive - numAlive);
           
            //Debug.Log("Alive:" + numAlive);

            //var tobeBorn = num - numAlive;
            int tobeBorn = 10; // new born every time spacebar is pressed
            //previousAlive = numAlive + 50; ;

            if (numAlive < (num - tobeBorn))
            {

                for (int i = 0; i < tobeBorn; i++)
                {

                    // randomly choose on of the alive ones to have a child (a better way is to chose the one with longest life)
                    int ind = Random.Range(0, numAlive);
                    // determine the characters of the parent
                    Color cc = Players[ind].GetComponent<Renderer>().material.color;
                    Vector3 pos = Players[ind].transform.position;

                    float x = pos.x; // Random.Range(xmin, xmax);
                    float y = pos.y; // Random.Range(ymin, ymax);
                    float z = pos.z; // Random.Range(zmin, zmax);
                    var agents = Instantiate(agent, new Vector3(x, y, z), Quaternion.identity);

                    int toss = Random.Range(0, 2);
                    Color new_color;

                    //color_range = 0.2f;
                    //float rcc = cc.r + Random.Range(-color_range, color_range);
                    //float gcc = cc.g + Random.Range(-color_range, color_range);
                    //rcc = Mathf.Clamp(rcc, 0, 1);
                    //gcc = Mathf.Clamp(gcc, 0, 1);
                    //if (toss == 0) { new_color = new Color(rcc, gcc, 0.7f, 0); }
                    //else { new_color = new Color(cc.r, cc.g, 0.4f, 0); }

                    if (toss == 0)
                    {
                        new_color = new Color(0f, 0f, 0.8f, 0); // liberal
                    }
                    else
                    {
                        new_color = new Color(0.8f, 0f, 0.0f, 0); // conservative
                    }

                    agents.GetComponent<Renderer>().material.SetColor("_Color", new_color);

                    //Debug.Log("New Born:" + tobeBorn);
                    //Debug.Log("rcc: " + rcc + "gcc: " + gcc);
                }

            }
          
        }

    }

    private IEnumerator CalculatePopulation()
    {
        while (true)
        {
            population = GameObject.FindGameObjectsWithTag("Player").Length;
            yield return new WaitForSeconds(2);
        }
    }

    private void OnGUI()
    {
        GUI.Label(popRect, "Population:" + population, style);
        //GUI.Label(dedRect, "Dead:" + dead, style);
    }

}
