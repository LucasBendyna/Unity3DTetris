using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddScore : MonoBehaviour
{
    // Delay between the deletion of text containing the value of the score to be added.
    float delay = 2f;

    /*
    * Display the text containing the value of the score to be added before deleting it.
    */
    void Update()
    {
        StartCoroutine(FadeOutRoutine());
        Destroy(gameObject, delay);
    }

    /*
    * Edit text containing the value of the score to be added.
    * 
    * Parameters :
    *   int value - The value to display.
    */
    public void EditValue(int value)
    {
        GetComponent<Text>().text = "+ " + value;
    }

    /*
    * Display a text containing the value of the score to be added before deleting it.
    * 
    * Return :
    *   IEnumerator - Allow the use of a coroutine.
    */
    IEnumerator FadeOutRoutine()
    {
        Text text = GetComponent<Text>();
        Color originalColor = text.color;
        for (float t = 0.01f; t < delay; t += Time.deltaTime)
        {
            text.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / delay));
            yield return null;
        }
    }
}
