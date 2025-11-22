using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Player : TransformObject
{
    // Requirement components
    private Camera mainCamera;
    [SerializeField]
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    private GameObject destinationObject;
    [SerializeField]
    private ObjectManager objectManager;

    // Flags
    private bool isAttack;
    private bool isMove;
    private bool usedSkill;

    private Vector3 attackPoint;

    private WaitForSeconds waitTime2f;

    private void Awake()
    {
        InitializeTransformObject();

        mainCamera = Camera.main;

        navMeshAgent.updateRotation = false;

        waitTime2f = new WaitForSeconds(2f);
    }

    private IEnumerator Start()
    {
        while (true)
        {
            ClickAttack();
            ClickMove();
            LookMoveDirection();
            UseSkill();

            yield return null;
        }
    }

    private void ClickAttack()
    {
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, 1 << 10))
        {
            attackPoint = hit.point;

            Attack();
        }
    }

    private void ClickMove()
    {
        if (Input.GetMouseButton(1) && Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, 1 << 10))
        {
            destinationObject.transform.position = hit.point;

            destinationObject.SetActive(true);
            navMeshAgent.SetDestination(hit.point);

            isMove = true;
        }
    }

    private void Attack()
    {
        isAttack = true;

        GameObject hitEffectObject = Random.Range(0, 2) == 0 ? objectManager.HitEffectObjectPool.Get() : objectManager.HitEffect_BlueObjectPoool.Get();

        hitEffectObject.transform.position = attackPoint + Vector3.up;

        hitEffectObject.SetActive(true);

        StartCoroutine(AttackEnd(hitEffectObject));
    }

    private IEnumerator AttackEnd(GameObject hitEffectObject)
    {
        yield return waitTime2f;

        hitEffectObject.SetActive(false);

        isAttack = false;
    }

    private void LookMoveDirection()
    {
        if (isMove)
        {
            if (navMeshAgent.velocity.sqrMagnitude == 0f)
            {
                destinationObject.SetActive(false);

                isMove = false;

                return;
            }

            Vector3 direction = new Vector3(navMeshAgent.steeringTarget.x, transform.position.y, navMeshAgent.steeringTarget.z) - transform.position;

            transform.forward = direction;
        }
    }

    private void UseSkill()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !usedSkill)
        {
            usedSkill = true;

            GameObject skillEffectObject = objectManager.SkillEffectObjectPool.Get();

            skillEffectObject.transform.SetPositionAndRotation(transform.position + transform.forward, transform.rotation);

            skillEffectObject.SetActive(true);

            StartCoroutine(SkillEnd(skillEffectObject));
        }
    }

    private IEnumerator SkillEnd(GameObject skillEffectObject)
    {
        yield return waitTime2f;

        skillEffectObject.SetActive(false);

        usedSkill = false;
    }
}
