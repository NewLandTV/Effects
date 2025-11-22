using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField]
    private ObjectPool hitEffectObjectPool;
    [SerializeField]
    private ObjectPool hitEffect_BlueObjectPoool;
    [SerializeField]
    private ObjectPool skillEffectObjectPool;
    [SerializeField]
    private ObjectPool nfxEffectObjectPool;

    public ObjectPool HitEffectObjectPool => hitEffectObjectPool;
    public ObjectPool HitEffect_BlueObjectPoool => hitEffect_BlueObjectPoool;
    public ObjectPool SkillEffectObjectPool => skillEffectObjectPool;
    public ObjectPool NFX_EffectObjectPool => nfxEffectObjectPool;

    private void Awake()
    {
        hitEffectObjectPool.MakeInstance(20);
        hitEffect_BlueObjectPoool.MakeInstance(20);
        skillEffectObjectPool.MakeInstance(10);
        nfxEffectObjectPool.MakeInstance(50);
    }
}
