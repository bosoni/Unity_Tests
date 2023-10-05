// https://docs.unity3d.com/ScriptReference/Physics.Raycast.html

/*
 * benchmark
 * enter lisää obuja
 * space laittaa lisätyt obut pyörimään
 * L varjot päälle ja pois
 * 
 * */
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_benchmark : MonoBehaviour
{
    public GameObject[] prefabs;
    GameObject target;
    List<GameObject> objs = new List<GameObject>();
    bool rotating = false;

    // Update is called once per frame
    void Update()
    {
        var rm = GameObject.Find("mapScript").GetComponent<RandomMap>();
        if (Input.GetKey(KeyCode.Return))
        {
            for (int cc = 0; cc < 100; )
            {
                float S = 5;
                float x = Random.Range(-S, S);
                float z = Random.Range(-S, S);
                float y = rm.GetY(x, z);
                if (y <= 0) continue; // ei veden alle
                
                var go = rm.AddToMap(x, y, z);

                if (Random.Range(0, 10) < 5)
                    rm.RandomMaterial(go);

                objs.Add(go);
                cc++;
            }
        }

        if (Input.GetKey(KeyCode.L))
        {
            var l = GameObject.Find("Directional Light").GetComponent<Light>();
            if (l.shadows == LightShadows.None)
                l.shadows = LightShadows.Soft;
            else
                l.shadows = LightShadows.None;
        }

        // uudet obut pyörimään
        if (Input.GetKeyUp(KeyCode.Space))
            rotating = !rotating;

        if (rotating)
            foreach (var go in objs)
            {
                go.transform.Rotate(new Vector3(0, Time.deltaTime * 100, 0));
            }


        var txt = GameObject.Find("UI/infoText").GetComponent<Text>();
        string strr = "Object count: " + rm.numOfObjects;
        if (rotating) strr += "\nrotating";
        txt.text = strr;

    }
}
