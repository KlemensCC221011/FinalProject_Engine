using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _flipYRotationTime;

    private Coroutine _turnCoroutine;

    private PlayerController _playerController;
    private bool _isFacingRight = true;

    private void Awake()
    {
        _playerController = _playerTransform.GetComponent<PlayerController>();
        _isFacingRight = _playerController.facingRight;
    }

    private void Update()
    {
        transform.position = _playerTransform.position;
    }

    public void CallTurn()
    {
        _turnCoroutine = StartCoroutine(FlipYLerp());
    }

    private IEnumerator FlipYLerp()
    {
        float startRotation = transform.localEulerAngles.y;
        float endRotation = DetermineEndRotation();
        float yRotation = 0f;

        float elapsedTime = 0f;
        while(elapsedTime < _flipYRotationTime)
        {
            elapsedTime += Time.deltaTime;

            yRotation = Mathf.Lerp(startRotation, endRotation, (elapsedTime/_flipYRotationTime));
            transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }

        yield return null;
    }

    private float DetermineEndRotation()
    {
        _isFacingRight = !_isFacingRight;
        if(!_isFacingRight )
        {
            return 180f;
        }
        else
        {
            return 0f;
        }
    }
}
