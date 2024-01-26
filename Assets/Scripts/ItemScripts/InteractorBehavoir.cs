using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class InteractorBehavoir : MonoBehaviour
{
    [SerializeField] Vector3 InteractionOffset = new Vector3();
    [SerializeField] float InteractRadius = 5.0f;
    [SerializeField] private LayerMask _InteractableMask;
    [SerializeField] private bool bDrawDebug = false;

    private readonly Collider[] _Colliders = new Collider[4];
    [SerializeField] private int CollidersCount;

    private void Update()
    {
        CollidersCount = Physics.OverlapSphereNonAlloc(InteractionOffset + transform.position, InteractRadius, _Colliders, _InteractableMask);
    }

    private void OnDrawGizmos()
    {
        if(bDrawDebug)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(InteractionOffset + transform.position, InteractRadius);
        }
    }

    public bool TryInteract()
    {
        if (CollidersCount > 0)
        {
            IInteractable Interactable = _Colliders[0].GetComponent<IInteractable>();
            return Interactable.TryInteract(this);
        }
        return false;
    }
}
