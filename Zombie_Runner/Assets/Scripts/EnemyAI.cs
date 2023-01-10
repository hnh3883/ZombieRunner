using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float chaseRange = 5f; // 이 범위안에 들어가면 적이 쫓아옴
    [SerializeField] float turnSpeed = 5f; // 적이 고개돌리는 속도

    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity; // 타겟과의 거리  (일단은 무한대로 설정)(엄청 큰값)
    bool isProvoked = false;
    EnemyHealth health;
    Transform target;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
        target = FindObjectOfType<PlayerHealth>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (health.IsDead())
        {
            enabled = false;  // 죽으면 멈춤
            navMeshAgent.enabled = false;
        }

        distanceToTarget = Vector3.Distance(target.position, transform.position); // 적과의 거리 = 타겟위치와 자신위치의 차이

        if (isProvoked)
        {
            EngageTarget();
        }
        else if(distanceToTarget <= chaseRange) // 타겟과의 거리가  chaseRange보다 작아지면 (= 적 범위안에 들어오면)
        {
            isProvoked = true;
        }
    }

    public void OnDamageTaken() // 공격받으면 적이 화나서 쫓아옴
    {
        isProvoked = true;
    }

    private void EngageTarget()
    {
        FaceTarget();

        // 거리가 stoppingDistance만큼 되면 멈추겠다 (stoppingDistance가 0이면 절대안멈춤)(계속 나를 밀어냄)
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("attack", false);

        // 가까이가면 애니메이션 무브 트리거를 발동?? (enemy가 움직)
        GetComponent<Animator>().SetTrigger("move");

        // 적이 쫓아온다
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("attack", true);
    }

    private void FaceTarget() // 타겟을 바라보는 메소드
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        Debug.Log("kkk");
    }

    // 기즈모가 선택되면 메소드 발동
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; // 기즈모의 색깔 설정
        Gizmos.DrawWireSphere(transform.position, chaseRange);  
    }
}
