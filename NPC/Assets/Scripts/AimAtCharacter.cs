using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AimAtCharacter : MonoBehaviour
{
    private void Awake()
    {
        RigBuilder rigBuilder = GetComponent<RigBuilder>();

        rigBuilder.enabled = false;
    }

    public void ActivarMultiAimConstraints()
    {
        RigBuilder rigBuilder = GetComponent<RigBuilder>();

        rigBuilder.enabled = true;
    }

    public void DesactivarMultiAimConstraints()
    {
        RigBuilder rigBuilder = GetComponent<RigBuilder>();

        rigBuilder.enabled = false;
    }
}
