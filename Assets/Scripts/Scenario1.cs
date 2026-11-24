using System;
using DG.Tweening;
using UnityEngine;

public class Scenario1 : MonoBehaviour
{
    [SerializeField] private Rigidbody cube1RB;
    [SerializeField] private Transform target;
    [SerializeField] private float jumpDuration;

    private Transform cubeTransform;

    private Vector3 startPosition;
    private Vector3 startScale;
    private Quaternion startRotation;

    private Sequence jumpSequence;
    private Tween jumpTween;

    private void Start()
    {
        cubeTransform = cube1RB.transform;

        startPosition = cube1RB.position;
        startScale = cubeTransform.localScale;
        startRotation = cubeTransform.rotation;

        StartJump();
    }

    private void StartJump()
    {
        jumpSequence = DOTween.Sequence();

        jumpSequence.Append(cubeTransform.DORotate(new Vector3(-30, 0, 0), 0.3f));
        jumpSequence.Join(cubeTransform.DOScale(startScale * 0.7f, 0.3f));
        jumpSequence.Append(cube1RB.DOJump(target.position, 3, 1, jumpDuration));
        jumpSequence.Join(cubeTransform.DORotate(Vector3.zero, jumpDuration));
        
        Sequence scaleSequence = DOTween.Sequence();
        
        scaleSequence.Append(cubeTransform.DOScale(startScale * 1.3f, 0.1f));
        scaleSequence.Append(cubeTransform.DOScale(startScale, 0.3f));

        jumpSequence.Join(scaleSequence);
    }

    public void RestartJump()
    {
        jumpSequence?.Kill();
        jumpTween?.Kill();

        cube1RB.position = startPosition;
        cubeTransform.localScale = startScale;
        cubeTransform.rotation = startRotation;

        cube1RB.linearVelocity = Vector3.zero;
        cube1RB.angularVelocity = Vector3.zero;

        StartJump();
    }
}