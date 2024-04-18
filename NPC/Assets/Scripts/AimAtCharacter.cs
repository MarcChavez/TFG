using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AimAtCharacter : MonoBehaviour
{
    private void Awake()
    {
        RigBuilder rigBuilder = GetComponent<RigBuilder>();

        rigBuilder.enabled = false;

        FindObjectOfType<PlayerInteract>().onEnterRange.AddListener(ActivarMultiAimConstraints);

        FindObjectOfType<PlayerInteract>().onExitRange.AddListener(DesactivarMultiAimConstraints);
    }

    private void ActivarMultiAimConstraints()
    {
        RigBuilder rigBuilder = GetComponent<RigBuilder>();

        rigBuilder.enabled = true;
    }

    private void DesactivarMultiAimConstraints()
    {
        RigBuilder rigBuilder = GetComponent<RigBuilder>();

        rigBuilder.enabled = false;
    }
}
