using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;


public class PlayerController : MonoBehaviour
{

    private PlayerInput _playerInput;
    private Animator _animator;
    private CharacterController _charachertCtrl;
    public bool canAttack;

    private const string actionMapPlayerControls = "Player";
    private const string actionMapMenuControls = "UI";

    private string currentControlScheme;

    public float maxForwardSpeed = 8f;
    public float gravity = 20f;
    public float jumpSpeed = 10f;
    public float minTurnSpeed = 400;
    public float maxTurnSpeed = 1200;
    public float idleTimeOut = 5f;
    public RandomAudioPlayer footstepPlayer;

    private Vector2 _movementValue;
    private bool _isJumpPressed;

    public bool IsJumpPressed
    {
        get => _isJumpPressed;
        set => _isJumpPressed = value;
    }
    private Material _surfaceMaterial;



    #region  Animation State Info
    private AnimatorStateInfo _currentStateInfo;
    private AnimatorStateInfo _nextStateInfo;
    private bool _isAnimatorTransitioning;
    private AnimatorStateInfo _previousCurrentStateInfo;
    private AnimatorStateInfo _previousNextStateInfo;
    private bool _previousIsAnimatorTransitioning;

    #endregion

    private bool _attack;

    #region Private Values

    private bool _previouslyGrounded;
    private Collider[] _overlapResult = new Collider[8];
    private float _idleTimer;
    private bool _inAttack;
    private float _angleDiff;
    private Quaternion _targetRotation;
    private bool _isGrounded;
    private bool _readyToJump;
    private float _verticalSpeed;
    private float _forwardSpeed;
    private float _desiredForwardSpeed;
    private bool _inCombo;
    private bool _pickUp;
    private bool _canPickUp;
    public bool CanPickUp
    {
        get => _canPickUp;
        set => _canPickUp = value;
    }

    #endregion

    #region HASH VALUES
    private readonly int _HashGrounded = Animator.StringToHash("Grounded");
    private readonly int _HashForwardSpeed = Animator.StringToHash("ForwardSpeed");
    private readonly int _HashAngleDeltaRad = Animator.StringToHash("AngleDeltaRad");
    private readonly int _HashInputDetected = Animator.StringToHash("InputDetected");
    private readonly int _HashAirborneVerticalSpeed = Animator.StringToHash("AirborneVerticalSpeed");
    private readonly int _HashMeleeAttack = Animator.StringToHash("MeleeAttack");
    private readonly int _HashStateTime = Animator.StringToHash("StateTime");
    private readonly int _HashTimeoutToIdle = Animator.StringToHash("TimeoutToIdle");
    private readonly int _HashLocomotion = Animator.StringToHash("Locomotion");
    private readonly int _HashHurt = Animator.StringToHash("Hurt");
    private readonly int _HashDeath = Animator.StringToHash("Death");
    private readonly int _HashRespawn = Animator.StringToHash("Respawn");
    private readonly int _HashAirbone = Animator.StringToHash("Airborne");
    private readonly int _HashLanding = Animator.StringToHash("Landing");
    private readonly int _HashGameStarted = Animator.StringToHash("GameStarted");
    private readonly int _HashPickUp = Animator.StringToHash("PickUp");
    private readonly int _HashFootFall = Animator.StringToHash("FootFall");
    private readonly int _HashStandUp = Animator.StringToHash("ActionFinished");
    

    #endregion

    #region  Kinematic Values

    const float k_airboneTurnSpeedProportion = 5.4f;
    const float k_InverseOneEighty = 1f / 180f;
    const float k_jumpAbortSpeed = 10f;
    const float k_StickingGravityProportion = 0.3f;
    const float k_groundedRayDistance = 1f;
    const float k_minEnemyDotCoeff = 0.2f;
    const float k_groundAcceleration = 20f;
    const float k_groundDeceleration = 25f;

    #endregion

    private bool IsMoveInput => !Mathf.Approximately(_movementValue.sqrMagnitude, 0f);

    private void Awake()
    {

        if (!TryGetComponent(out _animator))
        {
            Debug.LogError($"THERE IS NO ANIMATOR");
        }

        if (!TryGetComponent(out _charachertCtrl))
        {
            Debug.LogError($"THERE IS NO CHARACTER CONTROLLER");
        }
        
        if (!TryGetComponent(out _playerInput))
        {
            Debug.LogError($"THERE IS NO PLAYER INPUT");
        }




        currentControlScheme = _playerInput.currentControlScheme;
    }


    public void StandUp()
    {
        _animator.SetBool(_HashStandUp,true);
    }

    private void FixedUpdate()
    {

        Cache();
        _animator.SetFloat(_HashStateTime, Mathf.Repeat(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime, 1f));
        _animator.ResetTrigger(_HashMeleeAttack);

        if (_attack && canAttack)
        {
            _animator.SetTrigger(_HashMeleeAttack);
        }

        
        MovementX();
        MovementY();
        SetRotation();

        if (IsOrientationUpdated() && IsMoveInput)
        {
            UpdateOrientation();
        }
        PlayAudio();

        Idle();

        _previouslyGrounded = _isGrounded;
    }

    public void StartCharacter()
    {
        _animator.SetBool(_HashGameStarted,true);
    }

    public void CallPickUpSetAction(int hashAnimation, int action )
    {
        _animator.SetInteger(hashAnimation,action);
    }

    public void PlayAudio()
    {
        float footFallCurve = _animator.GetFloat(_HashFootFall);
        if (footFallCurve > 0.01f && !footstepPlayer.playing && footstepPlayer.canPlay)
        {
            footstepPlayer.playing = true;
            footstepPlayer.canPlay = false;
            footstepPlayer.PlayRandomClip(_surfaceMaterial, _forwardSpeed < 4 ? 0 : 1);
        }
        else if (footstepPlayer.playing )
        {
            footstepPlayer.playing = false;
        }
        else if (footFallCurve < 0.01f && !footstepPlayer.canPlay)
        {
            footstepPlayer.canPlay = true;
        }
    }

    private void Cache()
    {
        _previousCurrentStateInfo = _currentStateInfo;
        _previousNextStateInfo = _nextStateInfo;
        _previousIsAnimatorTransitioning = _isAnimatorTransitioning;

        _currentStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        _nextStateInfo = _animator.GetNextAnimatorStateInfo(0);
        _isAnimatorTransitioning = _animator.IsInTransition(0);

    }


    private void Idle()
    {
        bool inputDetected = IsMoveInput || _isJumpPressed || _attack;

        if (_isGrounded && !inputDetected)
        {
            _idleTimer += Time.deltaTime;

            if (_idleTimer >= idleTimeOut)
            {
                _idleTimer = 0f;
                _animator.SetTrigger(_HashTimeoutToIdle);
            }
        }
        else
        {
            _idleTimer = 0f;
            _animator.ResetTrigger(_HashTimeoutToIdle);
        }

        _animator.SetBool(_HashInputDetected, inputDetected);
        
    }


    private void MovementX()
    {
        Vector2 moveInput = _movementValue;

        if (moveInput.sqrMagnitude > 1f)
        {
            moveInput.Normalize();
        }

        _desiredForwardSpeed = moveInput.magnitude * maxForwardSpeed;

        float acceleration = IsMoveInput ? k_groundAcceleration : k_groundDeceleration;

        _forwardSpeed = Mathf.MoveTowards(_forwardSpeed, _desiredForwardSpeed, acceleration * Time.deltaTime);

        _animator.SetFloat(_HashForwardSpeed, _forwardSpeed);

    }

    private void MovementY()
    {
        if (!_isJumpPressed && _isGrounded)
        {
            _readyToJump = true;
        }

        if (_isGrounded)
        {
            _verticalSpeed = -gravity * k_StickingGravityProportion;

            if (_readyToJump && !_inCombo && _isJumpPressed)
            {
                _verticalSpeed = jumpSpeed;
                _isGrounded = false;
                _readyToJump = false;
            }
        }
        else
        {
            if (!_isJumpPressed && _verticalSpeed > 0.0f)
            {
                _verticalSpeed -= k_jumpAbortSpeed * Time.deltaTime;
            }

            if (Mathf.Approximately(_verticalSpeed, 0f))
            {
                _verticalSpeed = 0f;
            }

            _verticalSpeed -= gravity * Time.deltaTime;
        }

    }

    private void OnAnimatorMove()
    {
        Vector3 movement;

        if (_isGrounded)
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position + Vector3.up * k_groundedRayDistance * 0.5f, -Vector3.up);
            if (Physics.Raycast(ray, out hit, k_groundedRayDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                movement = Vector3.ProjectOnPlane(_animator.deltaPosition, hit.normal);
                
                Renderer groundRenderer = hit.collider.GetComponent<Renderer>();
                _surfaceMaterial = groundRenderer ? groundRenderer.sharedMaterial : null;

            }
            else
            {
                movement = _animator.deltaPosition;
                _surfaceMaterial = null;
            }

        }
        else
        {
            movement = _forwardSpeed * transform.forward * Time.deltaTime;
        }

        _charachertCtrl.transform.rotation *= _animator.deltaRotation;
        movement += _verticalSpeed * Vector3.up * Time.deltaTime;

        _charachertCtrl.Move(movement);
        _isGrounded = _charachertCtrl.isGrounded;

        if (!_isGrounded)
        {
            _animator.SetFloat(_HashAirborneVerticalSpeed, _verticalSpeed);
        }

        _animator.SetBool(_HashGrounded, _isGrounded);

    }

    private void SetRotation()
    {
        Vector2 moveInput = _movementValue;
        Vector3 localMovementDirection = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

        //TODO AXIS DE CAMERA (X)
        Vector3 forward = Quaternion.Euler(0f, 45f, 0f) * Vector3.forward;
        forward.y = 0f;
        forward.Normalize();

        Quaternion targetRoation;

        if (Mathf.Approximately(Vector3.Dot(localMovementDirection, Vector3.forward), -1.0f))
        {
            targetRoation = Quaternion.LookRotation(-forward);
        }
        else
        {
            Quaternion cameraToInputOffset = Quaternion.FromToRotation(Vector3.forward, localMovementDirection);
            targetRoation = Quaternion.LookRotation(cameraToInputOffset * forward);
        }

        Vector3 resultingForward = targetRoation * Vector3.forward;

        
        
        float angleCurrent = Mathf.Atan2(transform.forward.x, transform.forward.z) * Mathf.Rad2Deg;
        float targetAngle = Mathf.Atan2(resultingForward.x, resultingForward.z) * Mathf.Rad2Deg;
        _angleDiff = Mathf.DeltaAngle(angleCurrent, targetAngle);
        _targetRotation = targetRoation;

    }


    private bool IsOrientationUpdated()
    {
        bool locomotion = !_isAnimatorTransitioning && _currentStateInfo.shortNameHash == _HashLocomotion || _nextStateInfo.shortNameHash == _HashLocomotion;
        bool airborne = !_isAnimatorTransitioning && _currentStateInfo.shortNameHash == _HashAirbone || _nextStateInfo.shortNameHash == _HashAirbone;
        bool landing = !_isAnimatorTransitioning && _currentStateInfo.shortNameHash == _HashLanding || _nextStateInfo.shortNameHash == _HashLanding;

        return locomotion || airborne || landing || _inCombo && !_inAttack;


    }

    private void SetUpPickUp()
    {
        _animator.SetTrigger(_HashPickUp);
    }

    private void UpdateOrientation()
    {
        _animator.SetFloat(_HashAngleDeltaRad, _angleDiff * Mathf.Deg2Rad);

        Vector3 localInput = new Vector3(_movementValue.x, 0f, _movementValue.y);
        float groundedTurnSpeed = Mathf.Lerp(maxTurnSpeed, minTurnSpeed, _forwardSpeed / _desiredForwardSpeed);
        float actualTurnSpeed = _isGrounded ? groundedTurnSpeed : Vector3.Angle(transform.forward, localInput) * k_InverseOneEighty
        * k_airboneTurnSpeedProportion * groundedTurnSpeed;

        _targetRotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, actualTurnSpeed * Time.deltaTime);

        transform.rotation = _targetRotation;


    }


    public void Respawn()
    {
            Debug.Log("<b>Respawning</b>");
    }

    public void RespawnFinished()
    {
            Debug.Log("<b> Respawn finished </b>");
    }

    public void GetMovement(InputAction.CallbackContext context)
    {
        Vector2 inputMovement = context.ReadValue<Vector2>();
        _movementValue = inputMovement;

    }

    public void GetJumpAction(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.performed;
    }

    public void GetAttackAction(InputAction.CallbackContext context)
    {
        _attack = context.performed;
    }

    public void GetPickUpAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if ( _canPickUp)
            {
                SetUpPickUp();
            }
        }
        
    }

    public void OnTogglePause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            PauseMenu.Open();
            GameManager.Instance.SwitchScheme(true);
        }
    }
    public void OnControlsChanged()
    {
        if (_playerInput.currentControlScheme != currentControlScheme)
        {
            currentControlScheme = _playerInput.currentControlScheme;
            RemoveAllBindingsOverride();
        }
    }
    public void OnDeviceLost()
    {
        print($"CONTROL LOST");
    }
    public void OnDeviceRegained()
    {
        print($"CONTROL REGAINED");
    }

    private void RemoveAllBindingsOverride()
    {
        InputActionRebindingExtensions.RemoveAllBindingOverrides(_playerInput.currentActionMap);
    }

    public void EnableGameplayControls()
    {
        _playerInput.SwitchCurrentActionMap(actionMapPlayerControls);
    }

    public void EnableMenuControls()
    {
        _playerInput.SwitchCurrentActionMap(actionMapMenuControls);
    }

}

