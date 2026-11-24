using DG.Tweening;
using UnityEngine;

public class ButtonScenario1 : MonoBehaviour, IInteractable, IHighlightable
{
    [SerializeField] private GameObject scenario;
    [SerializeField] private MeshRenderer meshRenderer;

    [Header("Outline")]
    [SerializeField] private int outlineMaterialIndex = 1;
    [SerializeField] private float outlineWidth = 0.02f;

    [Header("Button Animation")]
    [SerializeField] private float shrinkScale = 0.9f;
    [SerializeField] private float animationDuration = 0.1f;

    private MaterialPropertyBlock propertyBlock;
    private Vector3 originalScale;
    private Tween buttonTween;

    private void Awake()
    {
        propertyBlock = new MaterialPropertyBlock();

        originalScale = transform.localScale;

        ShowHighlight(false);
    }

    public void Interact()
    {
        buttonTween?.Kill();
        transform.localScale = originalScale;

        buttonTween = transform.DOScale(
            originalScale * shrinkScale,
            animationDuration
        ).SetLoops(2, LoopType.Yoyo);


        IUseTween tween = scenario.GetComponent<IUseTween>();

        if (tween != null)
        {
            tween.PlayAnimation(0);
        }
    }

    public void ShowHighlight(bool state)
    {
        Debug.Log("HOLA MUNDO");
        meshRenderer.GetPropertyBlock(propertyBlock, outlineMaterialIndex);

        propertyBlock.SetFloat("_OutlineSize", state ? outlineWidth : 0f);

        meshRenderer.SetPropertyBlock(propertyBlock, outlineMaterialIndex);
    }
}