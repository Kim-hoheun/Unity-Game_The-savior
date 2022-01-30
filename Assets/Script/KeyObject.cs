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
        if (keyDistroy) // �÷��̾ Ű�� ������ Ű�� �ı��ϱ� �ؾ��Ѵ�. ���� obtainKey()���� �÷��̾��� Ű���� Ʈ��� ���ְ� keyObject�� �ı��� true�� ���ش�
        {
            Invoke("KeyDistroy", 1.0f);
            keyDistroy = false; // ������Ʈ���� �ѹ��� �����ϰ� �ϱ� ���ؼ� �ٽ� false�� �ٲپ���
        }
    }
    public void obtainKey()
    {
        player.keyHave = true; // �÷��̾��� Ű ȹ�� ���θ� true�� �ٲپ���
        keyDistroy = true;        // Ű�� �ν��� �Ǳ� ������ true�� ��ȯ
    }
    public void KeyDistroy()
    {
        Destroy(gameObject);
    }
}
