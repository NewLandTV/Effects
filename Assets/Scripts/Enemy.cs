using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Requirement components
    [SerializeField]
    private MeshRenderer meshRenderer;

    [SerializeField]
    private Color onDamageColor;
    private Color originColor;

    [SerializeField]
    private ObjectManager objectManager;

    // Flags
    private bool onDamage;

    private WaitForSeconds waitTime0_5f;
    private WaitForSeconds waitTime2f;

    private void Awake()
    {
        originColor = meshRenderer.material.color;

        waitTime0_5f = new WaitForSeconds(0.5f);
        waitTime2f = new WaitForSeconds(2f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 9 && !onDamage && !other.CompareTag("AfterEffect"))
        {
            StartCoroutine(OnDamange(other.GetComponent<Collider>(), Random.Range(0, 2) == 0));
        }
    }

    private IEnumerator OnDamange(Collider otherCollider, bool afterDamage)
    {
        onDamage = true;
        otherCollider.enabled = false;
        meshRenderer.material.color = onDamageColor;

        yield return waitTime0_5f;

        meshRenderer.material.color = originColor;
        otherCollider.enabled = true;
        onDamage = false;

        if (afterDamage)
        {
            GameObject nfxEffectObject = objectManager.NFX_EffectObjectPool.Get();

            nfxEffectObject.transform.position = transform.position;

            nfxEffectObject.SetActive(true);

            StartCoroutine(NFX_EffectEnd(nfxEffectObject));
        }
    }

    private IEnumerator NFX_EffectEnd(GameObject nfxEffectObject)
    {
        yield return waitTime2f;

        nfxEffectObject.SetActive(false);
    }
}
