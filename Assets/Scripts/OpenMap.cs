using UnityEngine;

public class OpenMap : MonoBehaviour
{
    public new Camera camera;

    private RectTransform rectTransform;
    private bool onTrue;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector2(570, 240);
        rectTransform.localScale = new Vector3(1, 1, 1);
    }
    public void onClick()
    {
        if (!onTrue)
        {
            rectTransform.localPosition = new Vector2(425, 100);
            rectTransform.localScale = new Vector3(2, 2, 2);
            camera.orthographicSize = 30;
            onTrue = true;
        }
        else 
        {

            rectTransform.localPosition = new Vector2(570, 240);
            rectTransform.localScale = new Vector3(1, 1, 1);
            camera.orthographicSize = 20;
            onTrue = false;
        }
    }
}
