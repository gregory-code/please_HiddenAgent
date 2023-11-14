using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, MovementInterface, IBTTaskInterface, ITeamInterface
{
    [SerializeField] ValueGuage healthBarPrefab;
    [SerializeField] Transform healthBarAttachTransform;
    HealthComponet healthComponet;
    [SerializeField] int teamID = 2;
    [SerializeField] DamageComponent damageComponent;

    Animator animator;
    Vector3 previousLocation;
    Vector3 velocity;

    MovementComponent movementComponent;

    NavMeshAgent agent;

    ValueGuage healthBar;

    private void Awake()
    {
        healthComponet = GetComponent<HealthComponet>();
        healthComponet.onTakenDamage += TookDamage;
        healthComponet.onHealthEmpty += StartDealth;
        healthComponet.onHealthChanged += HealthChanged;

        movementComponent = GetComponent<MovementComponent>();
        animator = GetComponent<Animator>();

        damageComponent.SetTeamInterface(this);

        agent = GetComponent<NavMeshAgent>();

        healthBar = Instantiate(healthBarPrefab, FindObjectOfType<Canvas>().transform);
        UIAttachComponent attachmentComp = healthBar.AddComponent<UIAttachComponent>();
        attachmentComp.SetupAttachment(healthBarAttachTransform);
    }

    public int GetTeamID() { return teamID; }

    private void HealthChanged(float currentHealth, float delta, float maxHealth)
    {
        healthBar.SetValue(currentHealth, maxHealth);
    }

    private void StartDealth(float delta, float maxHealth)
    {
        animator.SetTrigger("death");
        Destroy(healthBar.gameObject);
        GetComponent<AIController>().StopAILogic();
    }

    public void DeathAnimFinished()
    {
        Destroy(gameObject);
    }

    private void TookDamage(float currentHealth, float delta, float maxHealth, GameObject instigator)
    {
        Debug.Log($"Took Damage {delta}, now health is: {currentHealth}/{maxHealth}");
    }

    // Start is called before the first frame update
    void Start()
    {
        previousLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateVelocity();
    }

    private void CalculateVelocity()
    {
        velocity = (transform.position - previousLocation) / Time.deltaTime;
        previousLocation = transform.position;
        animator.SetFloat("speed", velocity.magnitude);
    }

    public void RotateTowards(Vector3 direction)
    {
        movementComponent.RotateTowards(direction);
    }

    public void RotateTowards(GameObject target)
    {
        movementComponent.RotateTowards(target.transform.position - transform.position);
    }

    public void AttackTarget(GameObject target)
    {
        animator.SetTrigger("attack");
    }

    public void AttackPoint()
    {
        damageComponent.DoDamage();
    }

    public float GetMoveSpeed()
    {
        return agent.speed;
    }

    public void SetMoveSpeed(float speed)
    {
        agent.speed = speed;
    }
}
