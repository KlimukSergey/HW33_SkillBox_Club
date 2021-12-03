using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CharContr : MonoBehaviour
{
    private NavMeshAgent agent;
    Targets targets;

    private const float
         State_WALK = 1,
         State_Dance = 2,
         State_Exit = 3;

    private float _currentState;
    private GameObject target;
    private Animator anim;
    private bool music;
    private GameObject obj;
    private GameObject guard;
    [SerializeField] public bool badBoy;


    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        obj = GameObject.Find("GAMEMANAGER");
        targets = obj.GetComponent<Targets>();
        agent = GetComponent<NavMeshAgent>();
        target = targets.ChangeTarget();
        agent.destination = target.transform.position;
        _currentState = 1;
    }

    void Update()
    {
        switch (_currentState)
        {

            case State_WALK:
                if (anim.GetBool("jump")) anim.SetBool("jump", false);
                float dist = agent.remainingDistance;
                if (dist <= 0)
                {
                    if (target.CompareTag("DanceTarget"))
                    {
                        _currentState = State_Dance;
                        StartCoroutine(Wait());
                    }
                    else _currentState = 0;
                    StartCoroutine(Wait());
                }
                break;

            case State_Dance:
                music = obj.GetComponent<GameManager>().music;
                if (music)
                    anim.SetBool("jump", true);  
                break;

            case State_Exit:
                if (agent.remainingDistance <= 0)
                {
                    guard.GetComponent<GuardController>().BadBoyOut();
                    gameObject.SetActive(false);
                    //  Destroy(gameObject);
                }
                break;

            default:
                break;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(Random.Range(8f, 16f));
        targets.AddTarget(target);
        target = targets.ChangeTarget();
        agent.destination = target.transform.position;
        _currentState = 1;
    }
    public void Dance()
    {
        StartCoroutine(Wait());
    }
    public void BadBoy(GameObject @object)
    {
        guard = @object;
        _currentState = State_Exit;
    }
}
