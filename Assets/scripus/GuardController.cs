using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GuardController : MonoBehaviour
{
    private NavMeshAgent agent;
    private const float
        State_IDLE = 0,
        State_Walk = 1;
    private float _currentState;
    private Animator anim;
    private Transform target;
    private float _pedSpeed;
    private RaycastHit hit;
    private Ray ray;

    [SerializeField] private float maxDistance;
    [SerializeField] private Transform point, exitPoint;
    private GameObject body;
    

    void Start()
    {
        anim=GetComponentInChildren<Animator>();
        body = anim.gameObject;
        agent = GetComponent<NavMeshAgent>();
        target = point;
        agent.destination = target.position;
        _currentState = 1;
    }
    private void Update()
    {
        anim.SetFloat("speed", agent.velocity.magnitude);
        switch (_currentState)
        {
            case State_IDLE:
                ray = new Ray(body.transform.position,
                              body.transform.forward);
                Debug.DrawRay(body.transform.position,
                              body.transform.forward*maxDistance,
                              Color.red);

                if (Physics.Raycast(ray, out hit, maxDistance))
                {
                    if (hit.transform.gameObject.CompareTag("Ped"))
                    {
                        target = hit.transform;
                        _pedSpeed = target.GetComponentInParent<NavMeshAgent>().speed;
                        agent.destination = target.position;
                        anim.SetBool("guardPoint",false);
                        _currentState = State_Walk;
                    }
                }
                break;

            case State_Walk:
                agent.destination = target.position;
                float dist = agent.remainingDistance;
                if (dist <= 0.8f)
                {
                    if (target.gameObject.CompareTag("Ped") )
                        
                    {
          
                        hit.transform.GetComponentInParent<NavMeshAgent>().speed = 0;
                        StartCoroutine(Search());
                    }
                    else
                    {
                        anim.SetBool("guardPoint",true);
                        transform.rotation=point.transform.rotation;
                        _currentState = State_IDLE;
                    }
                }
                break;

            default:
                break;
        }

        IEnumerator Search()
        {
            yield return new WaitForSeconds(3f);
            if (target.gameObject.CompareTag("Ped"))
            {
                hit.transform.GetComponentInParent<NavMeshAgent>().speed = _pedSpeed;
                bool isBad = target.gameObject.GetComponentInParent<CharContr>().badBoy;
                if (isBad)
                {
                    hit.transform.GetComponentInParent<NavMeshAgent>().destination = exitPoint.position;
                    target.gameObject.GetComponentInParent<CharContr>().BadBoy(this.gameObject);
                }
                else
                {
                    target = point;
                    agent.destination = target.position;
                    _currentState = State_Walk;
                }
            }
        }

    } 
    public void BadBoyOut()
        {
        agent.destination = point.position;
        target = point;
        _currentState = State_Walk;
        }

}