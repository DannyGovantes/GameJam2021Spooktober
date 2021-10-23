using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PickUpitem : MonoBehaviour
{

    private BoxCollider _boxCollider;

    [SerializeField]
    private RecipieItem _recipieItem;
    public RecipieItem RecipieItem => _recipieItem;

    public Color32 _color;

    private void Start()
    {
         if(!TryGetComponent(out _boxCollider))
        {
            Debug.LogWarning("NO HAY BOX COLLDIER");
        }
    }

    public void SetRecipieItem(RecipieItem item)
    {
        _recipieItem = item;
    }

    private void OnValidate()
    {
        if(!TryGetComponent(out _boxCollider))
        {
            Debug.LogWarning("NO HAY BOX COLLDIER");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _color;
        Gizmos.DrawCube(transform.position +_boxCollider.center,_boxCollider.size);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().canAttack = true;
           
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerController>().isGrabing)
        {
            GameManager.Instance.AddItemToInventory(_recipieItem);
            Destroy(gameObject);
            other.gameObject.GetComponent<PlayerController>().canAttack = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().canAttack = false;
        }
    }


}
