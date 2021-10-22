using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
        public float Speed = 0.1f;
    [SerializeField]private GameObject _player;

    public float m_XAxis => transform.rotation.x;


    private void Start()
        {
            if (!_player)
            {
                _player = FindObjectOfType<PlayerController>().gameObject;
            }
        }


        private void LateUpdate()
        {
            Vector3 pos = _player.transform.position + _offset;
            Vector3 desiredPosition = Vector3.Lerp(transform.position, pos, Speed);
            transform.position = desiredPosition;
        }
    
}
