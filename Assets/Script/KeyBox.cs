using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBox : MonoBehaviour
{
    public Player player;
    public bool openCheck;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (openCheck)
        {
            Invoke("KeyBoxDistroy", 1.0f);
            player.keyHave = false;  // 상자를 열었으므로 키를 다시 false로 만들어줌
            openCheck = false; // 업데이트기 때문에 한번만 실행시키기 위헤서 false로 만들어줌
        }
    }

    public void KeyBoxOpen()
    {
        openCheck = true;
    }
    public void KeyBoxDistroy()
    {
        Destroy(gameObject);
    }
}
