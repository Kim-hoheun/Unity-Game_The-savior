using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObject : MonoBehaviour
{
    public Player player;
    public bool keyDistroy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (keyDistroy) // 플레이어가 키를 얻으면 키를 파괴하기 해야한다. 따라서 obtainKey()에서 플레이어의 키값을 트루로 해주고 keyObject의 파괴도 true로 해준다
        {
            Invoke("KeyDistroy", 1.0f);
            keyDistroy = false; // 업데이트에서 한번만 실행하게 하기 위해서 다시 false로 바꾸어줌
        }
    }
    public void obtainKey()
    {
        player.keyHave = true; // 플레이어의 키 획득 여부를 true로 바꾸어줌
        keyDistroy = true;        // 키를 부숴도 되기 때문에 true로 변환
    }
    public void KeyDistroy()
    {
        Destroy(gameObject);
    }
}
