using UnityEngine;
using Gavin.FSM;
using UnityEngine.AI;

public class Monster : MonoBehaviour,IAIObj
{
    private  NavMeshAgent agent;
    public Transform objTransform => this.transform;

    public Vector3 nowPos => throw new System.NotImplementedException();

    public Vector3 targetObjPos => throw new System.NotImplementedException();

    public float atkRange => throw new System.NotImplementedException();

    public Vector3 bornPos => throw new System.NotImplementedException();

    public void Atk()
    {
        throw new System.NotImplementedException();
    }

    public void ChangeAction(E_Action action)
    {
        throw new System.NotImplementedException();
    }

    public bool IsLeavingStationedArea()
    {
        throw new System.NotImplementedException();
    }

    public void Move(Vector3 dirOrPos)
    {
        throw new System.NotImplementedException();
    }

    public void StopMove()
    {
        
    }



}
