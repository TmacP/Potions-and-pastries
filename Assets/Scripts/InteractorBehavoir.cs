using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorBehavoir : MonoBehaviour
{
    [SerializeField] Vector3 InteractionOffset = new Vector3();
    [SerializeField] float InteractRadius = 5.0f;
    [SerializeField] private LayerMask _InteractableMask;

    private readonly Collider[] _Colliders = new Collider[4];
    [SerializeField] private int CollidersCount;

    private void Update()
    {
        CollidersCount = Physics.OverlapSphereNonAlloc(InteractionOffset + transform.position, InteractRadius, _Colliders, _InteractableMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(InteractionOffset + transform.position, InteractRadius);
    }

    public bool TryInteract()
    {
        Debug.Log("Interaction Attempt");
        if (CollidersCount > 0)
        {
            IInteractable Interactable = _Colliders[0].GetComponent<IInteractable>();
            Interactable.TryInteract(this);
        }
        return false;
    }
}
