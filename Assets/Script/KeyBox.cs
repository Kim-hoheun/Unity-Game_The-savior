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
            player.keyHave = false;  // ���ڸ� �������Ƿ� Ű�� �ٽ� false�� �������
            openCheck = false; // ������Ʈ�� ������ �ѹ��� �����Ű�� ���켭 false�� �������
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
