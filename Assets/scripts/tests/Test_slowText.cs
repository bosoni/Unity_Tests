using UnityEngine;
using UnityEngine.UI;

public class Test_SlowText : MonoBehaviour
{
    string txt = "Unity 2023 testing...\nVittulan jorma sano moro!\n\n....\n...\n..\n.\nHoi sie!\n\n-\n--\n---\n----\n-----\n----\n---\n--\n-\n\n.";
    float pos = 0;

    void Update()
    {
        pos += Time.deltaTime * 10;
        if (pos >= txt.Length)
            pos = 0;

        GetComponent<Text>().text = txt.Substring(0, (int)pos);
    }

    public void SetText(string newText)
    {
        txt = newText;
        pos = 0;
    }

}
