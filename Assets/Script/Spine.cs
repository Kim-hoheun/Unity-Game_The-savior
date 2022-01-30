using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spine : MonoBehaviour
{
    public BoxCollider2D boxCollider2D;
    public Animator anim;
    public float firstTime;
    public float outTime;
    public float inTime;
    // Start is called before the first frame update

    // 1초 후에 콜라이더 온 하고 애니메이션
    // 1초 후에 콜라이더 오프 하고 애니메이션
    void Start()
    {
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        Invoke("onSpine", firstTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onSpine()
    {
        anim.SetBool("isActive", true);
        boxCollider2D.enabled = true;
        Invoke("offSpine", outTime);
    }
    public void offSpine()
    {
        anim.SetBool("isActive", false);
        boxCollider2D.enabled = false;
        Invoke("onSpine", inTime);
    }
}
