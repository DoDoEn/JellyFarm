using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    public int move_delay;  // 다음 이동까지의 딜레이 시간
    public int move_time;   // 이동 시간

    float speed_x;  // x축 방향 이동 속도
    float speed_y;  // y축 방향 이동 속도
    bool isWandering;
    bool isWalking;

    SpriteRenderer sprite;
    Animator anim;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        isWandering = false;
        isWalking = false;
    }

    void FixedUpdate()
    {
        if(!isWandering)
            StartCoroutine(Wander());  // 코루틴 실행
        if(isWalking)
            Move();
    }

    void Move()
    {
        if(speed_x != 0)
            sprite.flipX = speed_x < 0;

        transform.Translate(speed_x, speed_y, speed_y);  // 젤리 이동
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Bottom") || collision.gameObject.name.Contains("Top"))
            speed_y = -speed_y;
        else if (collision.gameObject.name.Contains("Left") || collision.gameObject.name.Contains("Right"))
            speed_x = -speed_x;
    }

    IEnumerator Wander()
    {
        move_delay = 6;
        move_time = 3;
        
        // Translate로 이동할 시 Object가 텔레포트 하는 것을 방지하기 위해 Time.deltaTime을 곱해줌
        speed_x = Random.Range(-0.8f, 0.8f) * Time.deltaTime;
        speed_y = Random.Range(-0.8f, 0.8f) * Time.deltaTime;

        isWandering = true;

        yield return new WaitForSeconds(move_delay);

        isWalking = true;
        anim.SetBool("isWalk", true);	// 이동 애니메이션 실행

        yield return new WaitForSeconds(move_time);
        
        isWalking = false;
        anim.SetBool("isWalk", false);	// 이동 애니메이션 종료

        isWandering = false;
    }
}
