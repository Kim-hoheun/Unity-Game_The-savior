using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxObject : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint; // 미리 이동시킬 객체
    public LayerMask whatStopMovement;
    public bool moveCheck;
    public Transform parentTransform;
    // Start is called before the first frame update
    void Start()
    {
        parentTransform = transform.parent;
        movePoint.parent = parentTransform;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)// 빈객체와 Player객체의 거리가 서로 멀지 않을때만 이동이 가능(둘이 멀어지면 구현시 이상해짐)
        {
            if (moveCheck && Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f) // 좌우 방향키 입력
            {

                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopMovement))// 플레이어가 이동할 공간에 0.2f크기의 원을 그리고 충동할 물체가 있다면 true를 리턴해서 실행안됌
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);                                               // 이동할 공간에 아무것도 없다면 이동할 공간으로 좌표로 값을 더해 준다.
                    moveCheck = false;
                }
                
                moveCheck = false;

            }
            else if (moveCheck && Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f) // 상하 방향키 입력
            {   
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopMovement))// 플레이어가 이동할 공간에 0.2f크기의 원을 그리고 충동할 물체가 있다면 true를 리턴해서 실행안됌
                {    
                     
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);                                                // 이동할 공간에 아무것도 없다면 이동할 공간으로 좌표로 값을 더해 준다.
                    moveCheck = false;
                }
                moveCheck = false;

            }

        }

    }
  

    public void BoxMoveIs()
    {
        moveCheck = true;
    }
    //-> box Oject 구현하기
    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    // 박스 오브젝트는 플레이어 공격을 받으면 플레이어가 공격한 백터 방향으로 한칸 등속도 운동으로 이동할지 말지를 정한다
    // 만약 플레이어가 공격한 백터방향의 공간이 빈칸이라면 이동한다
    // 만약 플레이어가 공격한 백터방향의 공간이 빈칸이 이고 열쇠 오브젝트가 있어도 이동한다
    // 만약 플레이어가 공격한 백터방향의 공간이 맵의 벽이거나, 밖이라면 이동하지 않는다
    // 만약 플레이어가 공격한 백터방향의 공간이 빈칸이 아니라면 이동하지 않는다.

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
}
