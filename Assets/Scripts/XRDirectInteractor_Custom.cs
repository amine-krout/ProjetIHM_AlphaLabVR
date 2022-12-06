using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRDirectInteractor_Custom : XRDirectInteractor
{

    protected new void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);
        IXRInteractable interactable;
        interactionManager.TryGetInteractableForCollider(col, out interactable);
        attachTransform.SetPositionAndRotation(
            interactable.transform.position,
            interactable.transform.rotation);
    }
}
