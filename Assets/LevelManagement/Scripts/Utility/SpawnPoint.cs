using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SpawnPoint : MonoBehaviour
{

    public Color32 gizmoColor;
    private BoxCollider _boxCollider;

    public void OnValidate()
    {
        if(!TryGetComponent(out _boxCollider))
        {
            Debug.LogWarning("NO HAY BOX COLLIDER");
        }
    }


    public void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawCube(transform.position + _boxCollider.center,_boxCollider.size);

    }
}
