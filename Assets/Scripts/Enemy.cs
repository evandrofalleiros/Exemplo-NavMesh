using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour{
    private NavMeshAgent _agent;
    private State _state;
    private GameObject _target;
    
    [Header("Enemy Properties")]
    [SerializeField] private float _patrollingSpeed = 1f;
    [SerializeField] private float _followingSpeed = 3f;

    void Start(){
        this._agent = GetComponent<NavMeshAgent>();
        this._agent.updateRotation = false;
        this._agent.updateUpAxis = false;

        this._state = State.Patroling;
    }

    void Update(){
        switch (_state){
            case State.Following: 
                Follow(_target);
                break;
            default: 
                Patrol();
                break;
        } 
    }

    private void Patrol(){
        this._agent.speed = _patrollingSpeed;

        if(!this._agent.pathPending && !this._agent.hasPath){
            this._agent.SetDestination(Random.insideUnitCircle.normalized * 3);
        }        
    }

    private void Follow(GameObject target){
        this._agent.speed = _followingSpeed;

        this._agent.SetDestination(target.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other != null){
            if (other.gameObject.CompareTag("Player")){
                this._state = State.Following;
                this._target = other.gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other != null){
            if (other.gameObject.CompareTag("Player")){
                this._state = State.Patroling;
            }
        }
    }


    enum State { Patroling, Following }
    
}
