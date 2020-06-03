using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    //throw new System.NotImplementedException();
    public void OnPointerDown(PointerEventData evenData)
    {
        if (gameObject.name == "Right")
        {
            GameObject.Find("player").GetComponent<Motion>().moveInput = 1;
        }
        if (gameObject.name == "Left")
        {
            GameObject.Find("player").GetComponent<Motion>().moveInput = -1;
        }
        if (gameObject.name == "Up")
        {
            GameObject.Find("player").GetComponent<Motion>().jump();
        }
    }

    // Update is called once per frame
    public void OnPointerUp(PointerEventData evenData)
    {
        GameObject.Find("player").GetComponent<Motion>().moveInput = 0;
    }
}
