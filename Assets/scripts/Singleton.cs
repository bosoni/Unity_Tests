using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton instance;

    public enum MovingMode { Axis, Forward }

    [HideInInspector]
    public MovingMode movingMode = MovingMode.Axis;

    [HideInInspector]
    public int score;

    private void Awake()
    {
        CreateSingleton();
    }

    void CreateSingleton()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyUp(KeyCode.F12))
        {
            movingMode++;
            if (movingMode == MovingMode.Forward)
                movingMode = MovingMode.Axis;
        }

        if (Input.GetKey(KeyCode.Tab))
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;


    }
}
