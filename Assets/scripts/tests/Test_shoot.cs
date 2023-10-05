// https://docs.unity3d.com/ScriptReference/Physics.Raycast.html
using UnityEngine;

public class Test2_shoot : MonoBehaviour
{
	// 0==target
	// 1==rock
    public GameObject[] prefabs;
    Rigidbody rigidBody;
    GameObject target;

    bool shoot = false;

    // Start is called before the first frame update
    void Start()
    {
        target = Instantiate(prefabs[0], Camera.main.transform.position, prefabs[0].transform.rotation);
        float S = 5f;
        target.transform.localScale = new Vector3(S, S, S);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;

        if (Input.GetMouseButtonUp(0))
        {
            shoot = true;
        }
        
    }

    void FixedUpdate()
    {
        // target
        RaycastHit hit;
        bool hitt = false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 10000.0f))
        {
            target.transform.position = hit.point;
            hitt = true;
        }
        else target.transform.position.Set(10000, 10000, 10000);
        //---------------

        // throw rock
        if (shoot)
        {
            shoot = false;

            var g = Instantiate(prefabs[1], Camera.main.transform.position, prefabs[1].transform.rotation);
            float S = 0.1f;
            g.transform.localScale = new Vector3(S, S, S);

            Vector3 dir;

            if (Cursor.lockState == CursorLockMode.Locked)
                dir = Camera.main.transform.TransformDirection(Vector3.forward);
            else
            {
                if (hitt)
                {
                    dir = target.transform.position - Camera.main.transform.position;
                }
                else
                    dir = Camera.main.transform.TransformDirection(Vector3.forward);

                dir.Normalize();
            }

            rigidBody = g.GetComponent<Rigidbody>();
            rigidBody.position += dir * 1.01f;
            rigidBody.AddForce(dir * 500);
        }
    }
}
