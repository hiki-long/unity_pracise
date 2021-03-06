using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    // Start is called before the first frame update
    public static DamagePopUp Create(Transform copy, Vector3 offset, int damage, Transform parent)
    {
        Transform damageposition = Instantiate(copy, Vector3.zero, Quaternion.identity);
        DamagePopUp damagePopUp = damageposition.GetComponent<DamagePopUp>();
        damageposition.transform.SetParent(parent);
        damageposition.transform.position = parent.transform.position + offset;
        damagePopUp.Setup(damage);
        return damagePopUp;
    }

    TextMeshProUGUI textMesh;
    private float disappeartime;
    private Color textColor;
    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshProUGUI>();
    }

    public void Setup(int damage)
    {
        textMesh.SetText(damage.ToString());
        textColor = textMesh.color;
        disappeartime = .5f;
    }
    private void Update()
    {
        disappeartime -= Time.deltaTime;
        float moveYSpeed = 100f;
        transform.position += new Vector3(0,moveYSpeed) * Time.deltaTime;
        if(disappeartime < 0)
        {
            float disappearSpeed = 4f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a < 0){
                Destroy(gameObject);
            }
        }

    }
}
