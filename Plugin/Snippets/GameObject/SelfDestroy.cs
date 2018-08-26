using UnityEngine;

public sealed class SelfDestroy : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
