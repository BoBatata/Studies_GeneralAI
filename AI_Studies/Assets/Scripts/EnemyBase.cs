using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public enum States
{
    Idle,
    Patrol,
}

public class EnemyBase : MonoBehaviour
{
    private Rigidbody _rb;
    private Collider _col;
    private Transform _trans;
    private NavMeshAgent _agent;

    private float _elapsedTime = 0;
    [SerializeField] private float timeToWait = 1f;
    private Vector3 _targetPos;
    
    [SerializeField] private States state;
    [SerializeField] private float speed;
    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<Collider>();
        _trans = GetComponent<Transform>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.destination = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
    }

    private void Update()
    {
        _agent.speed = speed;
        
        switch (state)
        {
            case States.Idle:
                HandleIdle();
                break;
            case States.Patrol:
                HandlePatrol();
                break;
        }
    }

    private void HandleIdle()
    {
        print("idle");
        _agent.destination = _trans.position;
    }

    private void HandlePatrol()
    {
        print("patrol");
        print("Tempo percorrido: " + _elapsedTime + " Tempo esperado: " + timeToWait);
        if (_elapsedTime >= timeToWait)
        {
            _agent.destination = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
            _elapsedTime = 0;
        }
        else
        {
            _elapsedTime  += Time.deltaTime;
        }
    }
}
