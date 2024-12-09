using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private static readonly int Death = Animator.StringToHash("Death");

    [Header("敌人血量")]
    public int hp;


    private NavMeshAgent agent;

    private Transform target;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GameOver)
            return;

        if (agent.enabled)
            agent.SetDestination(target.position);
    }

    public void Damage(int damageHp = 40)
    {
        hp -= damageHp;
        if (hp <= 0)
        {
            hp = 0;
            animator.SetTrigger(Death);
            agent.enabled = false;
        }
    }

    void Attack()
    {
        target.GetComponent<PlayerHealth>().Damage(5);
    }

    void StartSinking()
    {
        GetComponent<CapsuleCollider>().isTrigger = true;

        Destroy(this.gameObject, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InvokeRepeating(nameof(Attack), 0.5f, 1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CancelInvoke(nameof(Attack));
        }
    }
}
