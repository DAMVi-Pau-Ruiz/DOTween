
using System;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class Scenario2 : MonoBehaviour, IUseTween
{
    [Header("General")]
    [SerializeField] private Rigidbody cube1;
    [SerializeField] private Rigidbody cube2;

    [Header("Case 1")]
    [SerializeField] private Transform target;
    [SerializeField] private float case1Duration = 1;

    private Vector3 cube1Start;
    private Vector3 cube2Start;
    private Vector3 cube1StartScale;
    private Vector3 cube2StartScale;

    private Sequence case1Sequence;

    private void Start()
    {
        cube1Start = cube1.position;
        cube2Start = cube2.position;

        cube1StartScale = cube1.transform.localScale;
        cube2StartScale = cube2.transform.localScale;

        StartCase1();
    }

    public void PlayAnimation(int caseNumber)
    {
        switch (caseNumber)
        {
            case 1:
                StartCase1();
                break;

            case 2:
                StartCase1();
                break;

            case 3:
                StartCase1();
                break;

            case 4:
                StartCase1();
                break;

            case 5:
                StartCase1();
                break;
        }
    }

    public void StartCase1()
    {
        case1Sequence?.Kill();

        cube1.position = cube1Start;
        cube2.position = cube2Start;

        cube1.linearVelocity = Vector3.zero;
        cube2.linearVelocity = Vector3.zero;

        cube1.transform.localScale = cube1StartScale;
        cube2.transform.localScale = cube2StartScale;

        case1Sequence = DOTween.Sequence();

        // PREPARACION: Cube1 se aplasta y ensancha
        case1Sequence.Append(
            cube1.transform.DOScale(
                new Vector3(
                    cube1StartScale.x * 1.5f,
                    cube1StartScale.y * 0.65f,
                    cube1StartScale.z * 1.5f
                ), 0.2f
            )
        );

        // SALTO: Cube1 salta hacia Cube2
        case1Sequence.Append(
            cube1.DOJump(
                cube2.position + new Vector3(0, 0.75f, 0),
                3, 1, case1Duration
            )
        );

        // ESTIRAMIENTO: secuencia independiente de escala
        Sequence stretchSequence = DOTween.Sequence();

        stretchSequence.Append(
            cube1.transform.DOScale(
                new Vector3(
                    cube1StartScale.x * 0.85f,
                    cube1StartScale.y * 1.4f,
                    cube1StartScale.z * 0.85f
                ), 0.2f
            )
        );

        stretchSequence.Append(
            cube1.transform.DOScale(
                cube1StartScale,
                case1Duration - 0.2f
            )
        );

        case1Sequence.Join(stretchSequence);

        // IMPACTO: Cube1 rebota y Cube2 retrocede
        float bounceDuration = case1Duration / 2;

        case1Sequence.Append(
            cube1.DOJump(cube1Start, 0.5f, 1, bounceDuration)
        );

        case1Sequence.Join(
            cube2.DOMove(
                cube2Start - cube2.transform.forward * 2,
                bounceDuration
            )
        );

        case1Sequence.Join(
            cube2.transform.DOScale(
                new Vector3(
                    cube2StartScale.x * 1.5f,
                    cube2StartScale.y * 0.65f,
                    cube2StartScale.z * 1.5f
                ), 0.2f
            )
        );

        // RECUPERACION: Cube2 vuelve a la normalidad
        case1Sequence.Append(
            cube2.transform.DOScale(cube2StartScale, 0.3f)
        );
    }
}
