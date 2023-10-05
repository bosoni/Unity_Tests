using UnityEngine;

public class WaterOffset : MonoBehaviour
{
    // Scroll main texture based on time
    float scrollSpeed = 0.05f;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        rend.material.SetTextureOffset("_BumpMap", new Vector2(offset, 0));
    }
}
