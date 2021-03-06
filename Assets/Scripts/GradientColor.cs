using UnityEngine;
using UnityEngine.UI;

public class GradientColor : MonoBehaviour
{
    public Gradient Color;
    private Image Image;

    // Start is called before the first frame update
    void Start()
    {
        Image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        // Image.color = Color.Evaluate(Mathf.Abs(Mathf.Cos(Time.time)));
    }
}