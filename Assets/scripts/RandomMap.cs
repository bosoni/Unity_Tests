// https://docs.unity3d.com/ScriptReference/Collider.Raycast.html
// http://gyanendushekhar.com/2018/09/16/change-material-and-its-properties-at-runtime-unity-tutorial/

using UnityEngine;

public class RandomMap : MonoBehaviour
{
    public GameObject[] prefabs;
    public int createObjects = 100;
    public float scaleMul = 1;
    public int numOfObjects = 0;
    public bool randomMaterial = false;

    Collider coll;

    void Start()
    {
        coll = GameObject.Find("ground1/Plane-col").GetComponent<Collider>();

        for (numOfObjects = 0; numOfObjects < createObjects;)
        {
            float S = 5;
            float x = Random.Range(-S, S);
            float z = Random.Range(-S, S);
            float y = GetY(x, z);
            if (y <= 0) continue; // ei veden alle

            AddToMap(x, y, z);
        }
    }

    public bool IsOK()
    {
        return numOfObjects == createObjects;
    }

    public GameObject AddToMap(float x, float y, float z)
    {
        var pos = new Vector3(x, y, z);

        var rnd = Random.Range(0, prefabs.Length);

        var g = Instantiate(prefabs[rnd], pos, prefabs[rnd].transform.rotation);

        g.transform.localScale = new Vector3(g.transform.localScale.x * scaleMul,
            g.transform.localScale.y * scaleMul,
            g.transform.localScale.z * scaleMul);

        numOfObjects++;

        if (randomMaterial) RandomMaterial(g);

        return g;
    }

    public float GetY(float x, float z)
    {
        RaycastHit hit;
        Ray ray = new Ray(new Vector3(x, 1000, z), Vector3.down);
        if (coll.Raycast(ray, out hit, 10000))
        {
            return hit.point.y;
        }
        return 0;
    }

    public void RandomMaterial(GameObject g)
    {
        if (g.transform.childCount == 0) return;
        MeshRenderer m;
        if (g.transform.GetChild(0).TryGetComponent<MeshRenderer>(out m))
        {
            for (int q = 0; q < m.materials.Length; q++)
            {
                m.materials[q].color = new Color(Random.Range(0f, 1), Random.Range(0f, 1), Random.Range(0f, 1));

                //m.materials[q].mainTexture = newTex; // TODO
            }
        }
    }
}
